using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);

            try
            {
                socket.Bind(iPEndPoint);

                socket.Listen(5);

                Console.WriteLine("da");

                while (true)
                {
                    Socket sock = socket.Accept();

                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];

                    do
                    {
                        bytes = sock.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (sock.Available > 0);

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                    //string message = "ты гей";
                    //data = Encoding.Unicode.GetBytes(message);
                    sock.Send(data);
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
                //sock.Shutdown(SocketShutdown.Both);
                //sock.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
