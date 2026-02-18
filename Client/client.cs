using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    public static void Main(string[] args)
    {
        Socket client = ConnectToServer();
        if (client == null)
        {
            Console.WriteLine("Failed to connect to server.");
            return;
        }

        ListenToServer(client);
        Disconnect(client);
    }

    private static Socket ConnectToServer()
    {
        try
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(IPAddress.Loopback, 11000);
            Console.WriteLine("Connected to server: " + client.RemoteEndPoint?.ToString());
            return client;
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: " + e.ToString());
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.ToString());
            return null;
        }
    }

    private static void ListenToServer(Socket client)
    {
        try
        {
            Console.WriteLine("You can now type messages to send to the server. Type 'exit' to quit.");
            byte[] buffer = new byte[1024];
            while (true)
            {
                string? line = Console.ReadLine();
                if (line == null) break;
                if (line.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                byte[] data = Encoding.ASCII.GetBytes(line);
                client.Send(data);

                int bytesRead = client.Receive(buffer);
                if (bytesRead > 0)
                {
                    string received = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Server: " + received);
                }
                else
                {
                    Console.WriteLine("Server closed the connection.");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during communication: " + ex.ToString());
        }
    }

    private static void Disconnect(Socket socket)
    {
        if (socket == null) return;
        try
        {
            if (socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
            }
        }
        catch { }
        finally
        {
            socket.Close();
            Console.WriteLine("Disconnected.");
        }
    }
}
