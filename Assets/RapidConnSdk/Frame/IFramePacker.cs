using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidConn
{
	public interface IFramePacker {

		void Pack(Frame frame, ref byte[] buffer, out int size);
		Frame UnPack(byte[] buffer, int size);

	}
}