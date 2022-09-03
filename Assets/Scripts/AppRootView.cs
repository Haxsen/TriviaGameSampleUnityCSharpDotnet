using UnityEngine;

using MVVM.ViewModel;
using ViewModel;

using Sirenix.OdinInspector;

using View;

/// <summary>
/// Provided dependencies to injector
/// </summary>
public class AppRootView : MonoBehaviour
{
	[SerializeField, Required] UnityLifecycle _lifecycle;
	
	#region Mono Callbacks

	void Awake()
	{
		var injector = new Injector();

		ProvideLogger(injector);
		ProvideUnityLifecycle(injector);
	}

	#endregion Mono Callbacks

	void ProvideUnityLifecycle(Injector injector)
	{
		injector.Provide<ILifecycle>(_lifecycle);
		injector.Provide<ICoroutineProvider>(_lifecycle);
	}

	void ProvideLogger(Injector injector)
	{
		var logger = new UnityLogger();
		injector.Provide<ILog>(logger);
	}
}
