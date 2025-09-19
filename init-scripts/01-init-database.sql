-- Database initialization script for Houseiana
-- This script runs when PostgreSQL container starts for the first time

-- Create database if it doesn't exist (handled by POSTGRES_DB environment variable)
-- CREATE DATABASE IF NOT EXISTS houseiana;

-- Connect to the houseiana database
\c houseiana;

-- Enable UUID extension for generating GUIDs
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Create indexes for better performance (these will be created by EF migrations)
-- This script is mainly for any additional setup needed

-- Set timezone
SET timezone = 'UTC';

-- Create a function to update the updated_at timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ language 'plpgsql';

-- Log the successful initialization
INSERT INTO information_schema.sql_features (feature_id, feature_name, sub_feature_id, sub_feature_name, is_supported, comments)
VALUES ('HOUSEIANA_INIT', 'Houseiana Database Initialized', '1', 'Initial Setup', 'YES', 'Database ready for Entity Framework migrations')
ON CONFLICT DO NOTHING;