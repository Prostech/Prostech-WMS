using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using System;

namespace Prostech.WMS.API.Models
{
    public class SocketHelper
    {
        private static Socket _handler;
        private readonly ILogger<SocketHelper> _logger;

        public SocketHelper(ILogger<SocketHelper> logger)
        {
            _logger = logger;
        }

        public static void SetHandler(Socket socket)
        {
            _handler = socket;
        }

        public static async Task<Socket> OpenSocket(string ipAddress, int port)
        {
            try
            {
                IPAddress serverIpAddress = IPAddress.Parse(ipAddress);
                IPEndPoint serverEndPoint = new IPEndPoint(serverIpAddress, port);

                Socket listener = new Socket(serverIpAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Bind the socket to the server endpoint
                listener.Bind(serverEndPoint);

                // Start listening for incoming connections
                listener.Listen(5);

                // Accept the connection and create a new socket for communication
                _handler = await listener.AcceptAsync();

                return _handler;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<string> SendAsync(byte[] data, int bufferSize, ILogger logger)
        {
            try
            {
                logger.LogInformation("Sending data...");
                await _handler.SendAsync(data, SocketFlags.None);
                logger.LogInformation("Data sent successfully.");
                string receivedMessage = "";
                while (true)
                {
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead = await _handler.ReceiveAsync(buffer, SocketFlags.None);
                    receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    logger.LogInformation("Received message " + receivedMessage);
                    if (receivedMessage == "bye")
                        break;
                }


                return receivedMessage;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public static void CloseSocket()
        {
            try
            {
                if (_handler != null && _handler.Connected)
                {
                    _handler.Shutdown(SocketShutdown.Both);
                    _handler.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
