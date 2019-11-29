using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

namespace RapidConn
{
	
	public class Connection {

		public delegate void OnConnected();

		public delegate void OnDisconnected();

		public delegate void OnReceiveData (Frame frame);

		public void Connect(string ip, int port)
		{
			TcpClient c = new TcpClient (ip, port);

		}

		public void Send(byte[] buffer, int size)
		{

		}

	}

}
