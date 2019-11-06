using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace RapidConn
{
	public class SimpleFramePacker : IFramePacker {

		private const int cHeadSize = 12;	//[len, cmdid, seq, payload]

		public SimpleFramePacker()
		{
			m_tempWriteBuff = new byte[Frame.cMaxFrameSize];
			m_tempReadBuff = new byte[Frame.cMaxFrameSize];

			m_writeStream = new MemoryStream (m_tempWriteBuff);
			m_writer = new BinaryWriter (m_writeStream);

			m_packSeq = 0;
		}

		private int m_packSeq;
		private BinaryWriter m_writer;
		private MemoryStream m_writeStream;
		private byte[] m_tempWriteBuff;
		private byte[] m_tempReadBuff;


		public void Pack (Frame frame, ref byte[] buffer, out int size)
		{
			m_writeStream.Seek (0, SeekOrigin.Begin);

			var frameLen = cHeadSize + frame.PayloadSize;

			m_writer.Write (frameLen);
			m_writer.Write (frame.CmdId);
			m_writer.Write (m_packSeq);

			var headLen = m_writeStream.Position;
			size = frameLen;

			System.Buffer.BlockCopy(m_tempWriteBuff, 0, buffer, 0, cHeadSize);
			System.Buffer.BlockCopy(frame.Payload, 0, buffer, cHeadSize, frame.PayloadSize);

			m_packSeq++;
		}

		public Frame UnPack (byte[] buffer, int size)
		{
			//todo optmize gc

			Frame f = new Frame ();

			//todo

			return f;
		}

	}
}