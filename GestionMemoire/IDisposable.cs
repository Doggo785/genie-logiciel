using System;
namespace GestionMemoire;

public class DatabaseConnection : IDisposable
{
    private bool isOpen = false; // State of the connection

    public void Dispose()
    {
        Close();
    }

    // Constructor that simulates opening the database connection.
    public DatabaseConnection()
    {
        Console.WriteLine("Database connection opened.");
        isOpen = true;
    }

    // Method to simulate a database operation.
    public void ExecuteQuery(string query)
    {
        // Check if the connection is open.
        if (!isOpen)
            throw new InvalidOperationException("The connection is closed.");

        Console.WriteLine($"Executing query: {query}");
    }

    // Method to close the connection.
    public void Close()
    {
        // Close the connection if it is open.
        if (isOpen)
        {
            Console.WriteLine("Database connection closed.");
            isOpen = false;
        }
    }
}