using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net.Sockets;

namespace Microbrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient wantedserver = new TcpClient("www.uni-regensburg.de", 80); // IP adress, PORT number
            //prepare HTTP for the page of choice 
            byte[] wantedpage = Encoding.ASCII.GetBytes("GET /index.html HTTP/1.0"); //send to the network through bytes

            //Connect to server: three-way handshake 
            NetworkStream stream = wantedserver.GetStream(); //GetStream method

            //Send request
            stream.Write(wantedpage, 0, wantedpage.Length); //get request
            byte[] newline = Encoding.ASCII.GetBytes("\r\n"); //return newline
            stream.Write(newline, 0, newline.Length);
            stream.Write(newline, 0, newline.Length); //new empty line

            //Read the answer
            byte[] bytes = new byte[wantedserver.ReceiveBufferSize]; //byte array
            int mycount = 0;
            string data = "";
            do
            {
                mycount = stream.Read(bytes, 0, bytes.Length); //bytes, offset, size
                data = Encoding.ASCII.GetString(bytes, 0, mycount);

                //Display
                Console.WriteLine(data);


            } while (mycount > 0); //means that we go through the code at least once

            //Finished
            stream.Close();
            wantedserver.Close();
            Console.ReadLine();
        }
    }
}
