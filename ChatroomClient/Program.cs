using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatroomClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 35;
            Console.WindowWidth = 50;
            Console.SetWindowPosition(0, 0);
            Client chat = new ChatroomClient.Client();
            chat.RunClient();
        }
    }
}
