using UnityEngine;

using System.Collections;

namespace MVVM.ViewModel
{
	public class CoroutineWrapper : ICoroutine
	{
		readonly MonoBehaviour _component;


		public bool Active { get; private set; }

		Coroutine Coroutine { get; }


		public CoroutineWrapper(MonoBehaviour component, IEnumerator enumerator)
			: this(component, component.StartCoroutine(enumerator))
		{
		}

		public CoroutineWrapper(MonoBehaviour component, Coroutine coroutine)
		{
			_component = component;
			Coroutine = coroutine;
			Active = true;
		}


		public void Stop()
		{
			_component.StopCoroutine(Coroutine);
			Active = false;
		}
	}
}
