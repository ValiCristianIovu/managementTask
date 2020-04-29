﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace managementTask
{
    public class Client
    {

        TcpClient client = null;
        public NetworkStream stream { get; set; }
        public readonly string id = GenerateID();
    
        public void Start(String serverIP)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Connect(serverIP);
            }).Start();
            Console.ReadLine();
        }
       public void Connect(String serverIP)
        {
            try
            {
                Int32 port = 13000;
                client = new TcpClient(serverIP, port);             
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.Read();
        }
      
        public void CloseConnection()
        {
            stream.Close();
            client.Close();
        }
        public static string GenerateID()
        {
            Random random = new Random();
            return random.Next(999999).ToString();
        }

         public Packet ReadObject()
        {
            IFormatter formatter = new BinaryFormatter();
            Packet packet = (Packet)formatter.Deserialize(client.GetStream()); 
            return packet;
        }

        public void WriteObject(Packet packet)
        {
            if (packet != null)
            {
                IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream
                formatter.Serialize(client.GetStream(), packet);
            }
        }
    }
}
