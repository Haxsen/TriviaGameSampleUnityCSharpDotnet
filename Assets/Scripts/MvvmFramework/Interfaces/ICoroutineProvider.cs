using System.Collections;

namespace MVVM.ViewModel
{
	public interface ICoroutine
	{
		bool Active { get; }

		void Stop();
	}

	public interface ICoroutineProvider
	{
		ICoroutine StartCoroutine(IEnumerator enumerator);
	}
}
