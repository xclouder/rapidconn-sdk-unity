using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogHelper {

	public static void Debug(string msg)
	{
		UnityEngine.Debug.Log (msg);
	}

	public static void DebugFmt(string format, params object[] args)
	{
		UnityEngine.Debug.LogFormat (format, args);
	}

	public static void Error(string msg)
	{
		UnityEngine.Debug.LogError (msg);
	}

	public static void ErrorFmt(string format, params object[] args)
	{
		UnityEngine.Debug.LogErrorFormat (format, args);
	}

}
