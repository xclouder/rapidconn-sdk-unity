using System.Collections;
using System.Collections.Generic;

namespace RapidConn
{
	public delegate void OnConnected();

	public delegate void OnDisconnected();

	public delegate void OnReceiveData (IFrame frame);

	public interface IConnection {

		void Connect(string ip, int port);

		void Send(IFrame frame);
	}
}
