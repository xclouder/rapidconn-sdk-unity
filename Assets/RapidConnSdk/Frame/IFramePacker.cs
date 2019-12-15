using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidConn
{
	public interface IFramePacker {

		void Pack(IFrame frame, ref byte[] buffer, out int size);
		IFrame UnPack(byte[] buffer, int size);

	}
}