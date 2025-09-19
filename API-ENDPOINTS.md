# Houseiana API Endpoints - CRUD Operations

## Authentication Endpoints
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - User login
- `POST /api/auth/logout` - User logout
- `GET /api/auth/profile` - Get user profile
- `PUT /api/auth/profile` - Update user profile
- `POST /api/auth/become-host` - Become a host
- `POST /api/auth/refresh-token` - Refresh JWT token
- `POST /api/auth/validate` - Validate user token

## 🏠 Listings CRUD
### Public Endpoints (No Auth Required)
- `GET /api/listings` - Get all listings (paginated)
- `GET /api/listings/{id}` - Get listing by ID
- `POST /api/listings/search` - Search listings

### Host-Only Endpoints (Auth Required)
- `POST /api/listings` - Create new listing *(Host only)*
- `PUT /api/listings/{id}` - Update listing *(Host only)*
- `DELETE /api/listings/{id}` - Delete listing *(Host only)*
- `GET /api/listings/host/{hostId}` - Get host's listings

## 📅 Bookings CRUD
All booking endpoints require authentication.

- `GET /api/bookings` - Get user's bookings (paginated)
- `GET /api/bookings/{id}` - Get booking by ID
- `POST /api/bookings` - Create new booking
- `PUT /api/bookings/{id}` - Update booking
- `POST /api/bookings/{id}/cancel` - Cancel booking
- `GET /api/bookings/host` - Get host's bookings *(Host only)*
- `GET /api/bookings/guest` - Get guest's bookings

## 👥 Users CRUD
Authentication required for all endpoints.

- `GET /api/users` - Get all users *(Admin only)*
- `GET /api/users/{id}` - Get user by ID *(Self or Admin)*
- `PUT /api/users/{id}` - Update user *(Self or Admin)*
- `DELETE /api/users/{id}` - Delete user *(Self or Admin)*
- `POST /api/users/{id}/verify-email` - Verify email *(Self only)*
- `POST /api/users/{id}/verify-phone` - Verify phone *(Self only)*

## 🔔 Notifications CRUD
Authentication required for all endpoints.

- `GET /api/notifications` - Get user's notifications
- `POST /api/notifications` - Create notification *(Admin only)*
- `PUT /api/notifications/{id}/read` - Mark as read
- `DELETE /api/notifications/{id}` - Delete notification
- `POST /api/notifications/email` - Send email notification *(Admin only)*
- `POST /api/notifications/push` - Send push notification *(Admin only)*

## 🔧 System Endpoints
- `GET /api` - API health check
- `GET /api/health` - System health status

## Authentication & Authorization

### JWT Token Required
All endpoints except public listing views require JWT token in header:
```
Authorization: Bearer {your-jwt-token}
```

### Role-Based Access
- **Guest**: Can view listings, create bookings, manage own profile
- **Host**: All guest permissions + create/manage listings, view host bookings
- **Admin**: Full access to all endpoints

### Permission Levels
- 🌍 **Public**: No authentication required
- 🔒 **Authenticated**: Valid JWT token required
- 👤 **Self-Only**: User can only access their own data
- 🏠 **Host-Only**: Must have host privileges
- 👑 **Admin-Only**: Admin role required

## Request/Response Examples

### Create Listing (Host)
```http
POST /api/listings
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Beautiful Beach House",
  "description": "Amazing oceanfront property",
  "address": "123 Beach Ave",
  "nightlyPrice": 150.00,
  "maxGuests": 6
}
```

### Create Booking
```http
POST /api/bookings
Authorization: Bearer {token}
Content-Type: application/json

{
  "listingId": "listing-uuid",
  "checkIn": "2024-01-15",
  "checkOut": "2024-01-20",
  "guests": 4
}
```

### Search Listings
```http
POST /api/listings/search
Content-Type: application/json

{
  "location": "beach",
  "checkIn": "2024-01-15",
  "checkOut": "2024-01-20",
  "guests": 4,
  "maxPrice": 200
}
```

## Error Responses
All endpoints return consistent error format:
```json
{
  "message": "Error description",
  "error": "Detailed error information"
}
```

## Status Codes
- `200` - Success
- `201` - Created
- `400` - Bad Request
- `401` - Unauthorized
- `403` - Forbidden
- `404` - Not Found
- `500` - Server Error

## Next Steps
1. Connect to your Azure PostgreSQL database
2. Run `dotnet ef database update` to create tables
3. Start the API with `dotnet run`
4. Test endpoints using Swagger UI at `/api`