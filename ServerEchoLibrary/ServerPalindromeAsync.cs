using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;



namespace ServerPalindromeLibrary

{

    public class ServerPalindromeAPM : ServerPalindrome

    {

        byte[] loginMessage;
        byte[] passwordMessage;
        byte[] welcomeMessage;
        byte[] refuseMessage;
        string login;
        string password;

        public delegate void TransmissionDataDelegate(NetworkStream stream);

        public ServerPalindromeAPM(IPAddress IP, int port) : base(IP, port)

        {
            this.loginMessage = new ASCIIEncoding().GetBytes("\rPodaj login: ");
            this.passwordMessage = new ASCIIEncoding().GetBytes("\rPodaj haslo: ");
            this.welcomeMessage = new ASCIIEncoding().GetBytes("\rZalogowano!");
            this.refuseMessage = new ASCIIEncoding().GetBytes("\rNiepoprawne dane logowania!\r\n");
        }


        protected void readLoginFile()
        {
            System.IO.StreamReader file = new StreamReader(@"login.txt");
            login = file.ReadLine();
            password = file.ReadLine();
        }

        protected override void AcceptClient()
        {
            while (true)
            {
                TcpClient tcpClient = TcpListener.AcceptTcpClient();
                Stream = tcpClient.GetStream();
                TransmissionDataDelegate transmissionDelegate = new TransmissionDataDelegate(BeginDataTransmission);
                //callback style
                transmissionDelegate.BeginInvoke(Stream, TransmissionCallback, tcpClient);
                // async result style
                //IAsyncResult result = transmissionDelegate.BeginInvoke(Stream, null, null);
                ////operacje......
                //while (!result.IsCompleted) ;
                ////sprzątanie
            }

        }



        private void TransmissionCallback(IAsyncResult ar)

        {
            // sprzątanie
        }

        protected override void BeginDataTransmission(NetworkStream stream)
        {
            readLoginFile();
            byte[] buffer = new byte[Buffer_size];
            char[] trim = { (char)0x0 };

            while (true)
            {
                try

                {
                    stream.Write(loginMessage, 0, loginMessage.Length);
                    int dlugosc = stream.Read(buffer, 0, buffer.Length);
                    if (Encoding.ASCII.GetString(buffer, 0, dlugosc) == "\r\n")
                    {
                        stream.Read(buffer, 0, buffer.Length);
                    }
                    string login = Encoding.ASCII.GetString(buffer).Trim(trim);
                    Array.Clear(buffer, 0, buffer.Length);

                    stream.Write(passwordMessage, 0, passwordMessage.Length);
                    dlugosc = stream.Read(buffer, 0, buffer.Length);
                    if (Encoding.ASCII.GetString(buffer, 0, dlugosc) == "\r\n")
                    {
                        stream.Read(buffer, 0, buffer.Length);
                    }
                    string password = Encoding.ASCII.GetString(buffer).Trim(trim);
                    Array.Clear(buffer, 0, buffer.Length);

                    if (login == this.login && password == this.password)
                    {
                       

                        {
                            //stream.Write(welcomeMessage, 0, welcomeMessage.Length);

                            var inputMessage = new ASCIIEncoding().GetBytes("\rZalogowano. Podaj slowo ktore chcesz sprawdzic, czy jest palindromem: \n\r");

                            stream.Write(inputMessage, 0, inputMessage.Length);
                            int dlugoscWiadomosc = stream.Read(buffer, 0, buffer.Length);
                            if (Encoding.ASCII.GetString(buffer, 0, dlugoscWiadomosc) == "\r\n")
                            {
                                stream.Read(buffer, 0, buffer.Length);
                            }
                            string input = Encoding.ASCII.GetString(buffer).Trim(trim);
                            Array.Clear(buffer, 0, buffer.Length);

                            bool palindrome = Palindrome.IsPalindrome(input);

                            if (palindrome == true)
                            {
                                var trueMessage = new ASCIIEncoding().GetBytes("\rBrawo, to slowo jest palindromem\n\n");
                                stream.Write(trueMessage, 0, trueMessage.Length);
                            }
                            else
                            {
                                var falseMessage = new ASCIIEncoding().GetBytes("\rPrzykro mi, to slowo nie jest palindromem\n\n");
                                stream.Write(falseMessage, 0, falseMessage.Length);
                             
                            }            
                        }
                    }
                    else
                    {
                        stream.Write(refuseMessage, 0, refuseMessage.Length);
                    }
                }

                catch (IOException e)
                {
                    break;
                }

            }

        }

        public override void Start()

        {
            StartListening();
            //transmission starts within the accept function
            AcceptClient();

        }

        static byte[] stringToBytes(string input)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            return bytes;
        }

        


    }

}