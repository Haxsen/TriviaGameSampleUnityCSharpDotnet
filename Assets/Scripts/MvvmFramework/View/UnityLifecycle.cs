using UnityEngine;

using ViewModel;

using System;
using System.Collections;

namespace MVVM.ViewModel
{
	public class UnityLifecycle : MonoBehaviour, ILifecycle, ICoroutineProvider
	{
		public event Action OnUpdate;
		public event Action OnFixedUpdate;

		#region Properties

		public float Time => UnityEngine.Time.time;
		public float DeltaTime => UnityEngine.Time.deltaTime;

		#endregion Properties

		#region MonoCallbacks

		void Update()
		{
			OnUpdate?.Invoke();
		}

		void FixedUpdate()
		{
			OnFixedUpdate?.Invoke();
		}

		#endregion MonoCallbacks

		ICoroutine ICoroutineProvider.StartCoroutine(IEnumerator enumerator)
		{
			var wrapper = new CoroutineWrapper(this, enumerator);
			return wrapper;
		}
	}
}
