using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace udpClient1
{
    public class udpClient1
    {
        private const int port = 777;
        private static readonly UdpClient recUdpClient = new UdpClient(port);
        private static IPEndPoint ipEndPoint;
        private static byte[] receiveBytes = new byte[0];

        public static void Main()
        {
            try
            {
                Console.WriteLine("Ждем файл");
                receiveBytes = recUdpClient.Receive(ref ipEndPoint);
                using (var fileStream = new FileStream("temp.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    fileStream.Write(receiveBytes, 0, receiveBytes.Length);
                    Process.Start(fileStream.Name);
                }
                recUdpClient.Close();
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                recUdpClient.Close();
                Console.Read();
            }
        }
    }
}