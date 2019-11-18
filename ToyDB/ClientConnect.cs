using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace ClientConnect
{
    class Client
    {
        // Instantiate socket object
        private Socket Sender { get; set; }

        // Check whether or not the client has connected to the server
        private bool connected = false;


        /**
         * Sends a query to the server.
         * The query is encoded as a byte and sent through the socket to the connected server port.
         * 
         * @param String query - the query to be parsed
         * 
         * @return void
        **/
        public void SendQuery(String query)
        {
            // Check if socket connection has been established
            if (connected == true)
            {
                try
                {
                    byte[] queryMessage = Encoding.ASCII.GetBytes(query);
                    Sender.Send(queryMessage);
                }

                // Management of Socket's Exceptions 
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show("ArgumentNullException : " + ane.ToString());
                }

                catch (SocketException se)
                {
                    MessageBox.Show("SocketException : " + se.ToString());
                }

                catch (ObjectDisposedException ode)
                {
                    MessageBox.Show("ObjectDisposedException : " + ode.ToString());
                }

                catch (Exception e)
                {
                    MessageBox.Show("Unexpected exception : " + e.ToString());
                }
            }

            else
            {
                MessageBox.Show("Not connected to database server.");
            }
        }


        /**
         * Receive results from the server
         * The resuls are received using the method Receive() which returns the number of bytes
         * The message is then converted to a string
         * 
         * @return void
        */
        public void ReceiveResults()
        {
            // Check if socket connection has been established
            if (connected == true)
            {
                try
                {
                    byte[] resultsMessage = new byte[1024];
                    int byteRecv = Sender.Receive(resultsMessage);

                    MessageBox.Show("Query results -> " + Encoding.ASCII.GetString(resultsMessage, 0, byteRecv));
                }

                // Management of Socket's Exceptions 
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show("ArgumentNullException : " + ane.ToString());
                }

                catch (SocketException se)
                {
                    MessageBox.Show("SocketException : " + se.ToString());
                }

                catch (ObjectDisposedException ode)
                {
                    MessageBox.Show("ObjectDisposedException : " + ode.ToString());
                }

                catch (System.Security.SecurityException ssse)
                {
                    MessageBox.Show("SecurityException : " + ssse.ToString());
                }

                catch (Exception e)
                {
                    MessageBox.Show("Unexpected exception : " + e.ToString());
                }
            }

            else
            {
                MessageBox.Show("Not connected to database server.");
            }
        }


        /**
         * Open a socket to connect client to server on the specified port
         * 
         * @param int port - the port to connect to
         * 
         * @return void
        */
        public void ConnectSocket(int port)
        {
            try
            {
                // Establish the remote endpoint for the socket
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, port);

                // Creation of TCP/IP Socket using the Socket Class Constructor 
                Sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Sender.Connect(localEndPoint);

                connected = true;
                //MessageBox.Show("Socket connected to " + Sender.RemoteEndPoint.ToString());
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());            
            }
        }

        /**
         * Close the socket and end the connection
         * 
         * @return void
        **/
        public void CloseSocket()
        {
            Sender.Shutdown(SocketShutdown.Both);
            Sender.Close();
        }
    }
}
