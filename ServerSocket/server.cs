    using System.Net;
    using System.Net.Sockets;
    using System.Collections;
    using System.IO;

    class Server
    {
        public ArrayList clientsList = new ArrayList();
        public static void Main(string[] args)
        {
            Socket socket = StartServer();
            Socket client = AcceptConnection(socket);
            ListenToClient(client);
            Disconnect(socket);
        }
        private static Socket StartServer()
        {
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
            Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                ServerSocket.Bind(localEndPoint);
                ServerSocket.Listen(10);

                return ServerSocket;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        private static Socket AcceptConnection(Socket ServerSocket)
        {
            Console.WriteLine("Waiting for a connection...");
            Socket clientSocket = ServerSocket.Accept();
            Console.WriteLine("Client connected: " + clientSocket.RemoteEndPoint.ToString());
            return clientSocket;
        }
        private static void ListenToClient(Socket client)
        {
            byte[] buffer = new byte[1024];
            int bytesRead;
            while (true)
            {
                try
                {
                    bytesRead = client.Receive(buffer);
                    if (bytesRead > 0)
                    {
                        string receivedData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("Received: " + receivedData);
                        String UpperData = receivedData.ToUpper();
                        log(UpperData);
                        byte[] encodeString = System.Text.Encoding.ASCII.GetBytes(UpperData);

                        client.Send(encodeString, bytesRead, SocketFlags.None);
                    }
                    else
                    {
                        Console.WriteLine("Client disconnected: " + client.RemoteEndPoint.ToString());
                        client.Close();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    client.Close();
                    break;
                }
            }

        }

        private static void Disconnect(Socket socket) { socket.Close(); }

    private static void log(String message)
    {

        string logDirectory = Environment.GetEnvironmentVariable("LOG_DIR") ?? Directory.GetCurrentDirectory();

        Directory.CreateDirectory(logDirectory);

        string fileName = DateTime.Now.ToString("ddMMyyyy") + "_Logs.txt";
        string fullPath = Path.Combine(logDirectory, fileName);

        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath, true))
            {
                file.WriteLine(DateTime.Now.ToString() + ": " + message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur de log : " + ex.Message);
        }
    }

}