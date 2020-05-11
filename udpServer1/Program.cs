using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Practical1Server
{
    public class UdpFileServer
    {
        private static IPAddress ipAddress;
        private const int port = 777;
        private static UdpClient sender = new UdpClient();
        private static IPEndPoint ipEndPoint;

        private static FileStream fileStream;

        public static void Main()
        {
            try
            {
                Console.Write("IP ");
                ipAddress = IPAddress.Parse(Console.ReadLine());
                ipEndPoint = new IPEndPoint(ipAddress, port);
                Console.Write("путь ");
                fileStream = new FileStream(Console.ReadLine(), FileMode.Open, FileAccess.Read);
                SendFile();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void SendFile()
        {
            var bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);

            Console.WriteLine("размер " + fileStream.Length + " байт, отправляем");
            try
            {
                sender.Send(bytes, bytes.Length, ipEndPoint);
                fileStream.Close();
                sender.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                fileStream.Close();
                sender.Close();
            }
            Console.WriteLine("отправлено");
            Console.Read();
        }
    }
}