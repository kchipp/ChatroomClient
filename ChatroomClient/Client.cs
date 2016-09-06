using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Chat = System.Net;




namespace ChatroomClient
{
    class Client
    {
        NetworkStream server = default(NetworkStream);
        TcpClient client = new TcpClient();
        //BST

        private void GetMessage()
        {
            while (true)
            {
                byte[] inStream = new byte[1024];
                server.Read(inStream, 0, inStream.Length);
                string message = Encoding.ASCII.GetString(inStream);
                message = message.Substring(0, message.IndexOf("\0"));
                Console.WriteLine(message);
            }

        }

        public void RunClient()
        {

            try
            {
                client.Connect("localhost", 5253);
                Console.WriteLine("Welcome to The Chatroom!");
                Console.WriteLine("You have connected to the server.\nPlease enter your name...");
                string userName = Console.ReadLine();
                Console.WriteLine("Is the name you entered correct? Y/N");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "y":
                        byte[] stream = System.Text.Encoding.ASCII.GetBytes(userName);
                        server = client.GetStream();
                        server.Write(stream, 0, stream.Length);
                        server.Flush();
                        break;
                    case "n":
                        Console.WriteLine("Please re-enter your name");
                        RunClient();
                        break;
                    default:
                        Console.WriteLine("An error has occured.  Please disconnect and try again.");
                        break;
                }
                while (client.Connected)
                {
                    Thread messageReceived = new Thread(GetMessage);
                    messageReceived.Start();
                    server = client.GetStream();
                    string message = Console.ReadLine();

                    byte[] messageSent = Encoding.ASCII.GetBytes(message);
                    server.Write(messageSent, 0, messageSent.Length);
                    server.Flush();

                    if (message.ToLower() == "esc")
                    {
                        Environment.Exit(0);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
            }
        }
    }//class
}//namespace


