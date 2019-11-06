using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidConn
{
	public class Frame {

		public const int cMaxFrameSize = 1024 * 5;

		public int CmdId;
		public int Seq;
		public int PayloadSize;
		public byte[] Payload;

	}

}
