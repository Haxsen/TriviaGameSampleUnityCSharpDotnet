using UnityEngine;

using MVVM.ViewModel;

namespace View
{
	public class UnityLogger : ILog
	{
		public void Info(string message)
		{
			Debug.Log(message);
		}

		public void Warning(string message)
		{
			Debug.LogWarning(message);
		}

		public void Error(string message)
		{
			Debug.LogError(message);
		}
	}
}
