using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ClientConnect
{
    class Client
    {
        public void ExecuteClient()
        {
            try
            {
                // Establish the remote endpoint for the socket.
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

                // Creation TCP/IP Socket using Socket Class Costructor 
                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    // Connect Socket to the remote endpoint using method Connect() 
                    sender.Connect(localEndPoint);

                    // We print EndPoint information that we are connected 
                    MessageBox.Show("Socket connected to -> " + sender.RemoteEndPoint.ToString());

                    // Creation of message that we will send to Server 
                    byte[] messageSent = Encoding.ASCII.GetBytes("Test Clientassds<EOF>");
                    int byteSent = sender.Send(messageSent);

                    // Data buffer 
                    byte[] messageReceived = new byte[1024];

                    // We receive the message using the method Receive(). This method returns number of bytes 
                    // received, that we'll use to convert them to string 
                    int byteRecv = sender.Receive(messageReceived);
                    MessageBox.Show("Message from Server -> " + Encoding.ASCII.GetString(messageReceived, 0, byteRecv));

                    // Close Socket using the method Close() 
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }

                // Manage of Socket's Exceptions 
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {
                    MessageBox.Show("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    MessageBox.Show("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
