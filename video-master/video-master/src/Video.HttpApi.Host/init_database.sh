#!/bin/bash

# Database initialization script for Video Management System
# This script creates the SQLite database and initializes it with the required schema and data

echo "Initializing Video Management System database..."

# Get the directory where this script is located
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
DB_PATH="$SCRIPT_DIR/video.db"
SQL_SCRIPT="$SCRIPT_DIR/init_database.sql"

# Check if sqlite3 is available
if ! command -v sqlite3 &> /dev/null; then
    echo "Error: sqlite3 is not installed or not in PATH"
    echo "Please install sqlite3 to continue"
    exit 1
fi

# Check if the SQL script exists
if [ ! -f "$SQL_SCRIPT" ]; then
    echo "Error: init_database.sql not found in $SCRIPT_DIR"
    exit 1
fi

# Create the database
echo "Creating database at: $DB_PATH"
sqlite3 "$DB_PATH" < "$SQL_SCRIPT"

if [ $? -eq 0 ]; then
    echo "Database initialized successfully!"
    echo "Tables created:"
    sqlite3 "$DB_PATH" ".tables"
    echo ""
    echo "Admin user created:"
    echo "Username: admin"
    echo "Password: admin"
    echo ""
    echo "Database location: $DB_PATH"
else
    echo "Error: Failed to initialize database"
    exit 1
fi