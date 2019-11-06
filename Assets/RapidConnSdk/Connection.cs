using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidConn
{
	
	public class Connection {

		public delegate void OnConnected();

		public delegate void OnDisconnected();

		public delegate void OnReceiveData (Frame frame);

		public void Connect(string ip, int port)
		{
			
		}

		public void Send(byte[] buffer, int size)
		{

		}

	}

}
