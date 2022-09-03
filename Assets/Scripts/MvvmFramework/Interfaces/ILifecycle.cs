using System;

namespace ViewModel
{
	public interface ILifecycle
	{
		public event Action OnUpdate;
		public event Action OnFixedUpdate;

		public float Time { get; }
		public float DeltaTime { get; }
	}
}
