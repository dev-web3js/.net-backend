using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Houseiana.Data;
using Houseiana.Models;

namespace Houseiana.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly HouseianaDbContext _context;

    public ChatHub(HouseianaDbContext context)
    {
        _context = context;
    }

    public async Task JoinConversation(string conversationId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
    }

    public async Task LeaveConversation(string conversationId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);
    }

    public async Task SendMessage(string conversationId, string content)
    {
        var userId = Context.User?.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
            return;

        // Verify user is part of conversation
        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == conversationId && 
                (c.Participant1Id == userId || c.Participant2Id == userId));

        if (conversation == null)
            return;

        var receiverId = conversation.Participant1Id == userId 
            ? conversation.Participant2Id 
            : conversation.Participant1Id;

        var message = new Message
        {
            Id = Guid.NewGuid().ToString(),
            ConversationId = conversationId,
            SenderId = userId,
            ReceiverId = receiverId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        _context.Messages.Add(message);
        
        // Update conversation
        conversation.LastMessageId = message.Id;
        conversation.LastMessageAt = DateTime.UtcNow;
        
        if (conversation.Participant1Id == userId)
            conversation.UnreadCount2++;
        else
            conversation.UnreadCount1++;

        await _context.SaveChangesAsync();

        // Send to conversation group
        await Clients.Group(conversationId).SendAsync("ReceiveMessage", new
        {
            id = message.Id,
            conversationId = message.ConversationId,
            senderId = message.SenderId,
            content = message.Content,
            createdAt = message.CreatedAt
        });
    }

    public async Task MarkAsRead(string conversationId)
    {
        var userId = Context.User?.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
            return;

        var conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == conversationId && 
                (c.Participant1Id == userId || c.Participant2Id == userId));

        if (conversation == null)
            return;

        // Reset unread count for the user
        if (conversation.Participant1Id == userId)
            conversation.UnreadCount1 = 0;
        else
            conversation.UnreadCount2 = 0;

        // Mark messages as read
        var unreadMessages = await _context.Messages
            .Where(m => m.ConversationId == conversationId && 
                   m.ReceiverId == userId && !m.IsRead)
            .ToListAsync();

        foreach (var message in unreadMessages)
        {
            message.IsRead = true;
            message.ReadAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        // Notify other participant
        await Clients.Group(conversationId).SendAsync("MessagesRead", conversationId);
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirst("userId")?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.User?.FindFirst("userId")?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
        }
        await base.OnDisconnectedAsync(exception);
    }
}