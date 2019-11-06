using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidConn
{
	public class RapidConn {

		public RapidConn()
		{
			m_state = State.None;
		}


		public enum State
		{
			None,
			Connecting,
			Connected,
			Binded,
			Disconnecting,
		}

		private State m_state;
		public State GetState()
		{
			return m_state;
		}

		public delegate void OnConnectedSuccess();
		public delegate void OnError(int errorCode);
		public delegate void OnBind();
		public delegate void OnUnBind ();

		public void StartConnect(int connCode)
		{

		}

		public void Disconnect()
		{

		}

	}
}
