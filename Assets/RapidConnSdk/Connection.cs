using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;

namespace RapidConn
{
	
	public class Connection : IConnection {

		private class SocketContext
		{
			public Socket socket;
			public bool isClosed;
		}

		private enum State
		{
			NotConnected,
			Connecting,
			Connected
		}

		private State m_state;

		private SocketContext m_socketCtx;
		private Thread m_netThread;
		private readonly IFramePacker m_framePacker;

		public Connection(IFramePacker framePacker)
		{
			m_sendQueue = new Queue<Frame> ();
			m_receiveQueue = new Queue<Frame> ();
			m_framePacker = framePacker;

			m_threadSendQueue = new Queue<Frame> ();
			m_threadReceiveQueue = new Queue<Frame> ();

			m_sendBuf = new byte[1024 * 512];
		}
			
		public void Connect(string ip, int port)
		{
			if (m_state == State.NotConnected) {

				if (m_socketCtx != null) {
					LogHelper.Error ("socket is not null");

					m_socketCtx.socket.Close ();
					m_socketCtx.isClosed = true;
					m_socketCtx = null;
				}

				var socket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				IPAddress address = IPAddress.Parse(ip);
				IPEndPoint endPoint = new IPEndPoint(address, port);

				try {
					socket.Connect (endPoint);
				}
				catch (Exception e) {
					LogHelper.ErrorFmt ("exception occured during connect server:{0}, port {1}, ex:{2}", ip, port, e);
					return;
				}

				if (socket.Connected) {
					m_netThread = new Thread (NetThreadWorking);
					m_netThread.IsBackground = true;
					m_netThread.Priority = ThreadPriority.Normal;

					m_socketCtx = new SocketContext ();
					m_socketCtx.socket = socket;
					m_socketCtx.isClosed = false;

					m_netThread.Start (m_socketCtx);
				} else {
					LogHelper.Error ("socket not connected");
				}


			} else {
				LogHelper.Error ("already connected.");
			}
		}

		private void NetThreadWorking(object stateObj)
		{
			var ctx = stateObj as SocketContext;
			var socket = ctx.socket;

			while (!ctx.isClosed) {

				if (socket.Connected) {

					try {
						//receive
						DoReceiveThreaded(socket);
					}
					catch(Exception e) {
						LogHelper.Error ("socket receive exception:" + e);
					}

					try
					{
						//send
						DoSendThreaded(socket);
					}
					catch(Exception e) {
						LogHelper.Error ("socket send exception:" + e);
					}

				} else {
					LogHelper.Error ("socket not connected");
				}
			}

		}

		private object m_lockForSend = new object();
		private object m_lockForReceive = new object();

		private Queue<Frame> m_threadSendQueue;
		private byte[] m_sendBuf;
		private void DoSendThreaded(Socket socket)
		{
			if (m_sendQueue.Count > 0) {

				lock (m_lockForSend) {

					while (m_sendQueue.Count > 0) {

						m_threadSendQueue.Enqueue (m_sendQueue.Dequeue ());

					}
				}

				while (m_threadSendQueue.Count > 0)
				{
					var frame = m_threadSendQueue.Dequeue ();

					int size = 0;
					m_framePacker.Pack (frame, ref m_sendBuf, out size);

					if (size > 0) {

						socket.Send (m_sendBuf, size, SocketFlags.None);

					} else {
						LogHelper.Error ("pack frame get size:0");
					}
				}

			}
		}

		private Queue<Frame> m_threadReceiveQueue;
		private void DoReceiveThreaded(Socket socket)
		{
			
		}

		private void Close()
		{
			
		}

		private Queue<Frame> m_sendQueue;
		private Queue<Frame> m_receiveQueue;
		public void Send(IFrame frame)
		{
			
		}

	}

}
