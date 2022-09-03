using UnityEngine;

using MVVM.DataBinding;
using MVVM.ViewModel;
using View;
using ViewModel;

namespace MvvmExample
{
	public class TestRoot : MonoBehaviour
	{
		void Awake()
		{
			var container = GetComponent<ContextContainer>();
			var context = new TestViewModel();

			var injector = new Injector();
			var logger = new UnityLogger();
			var lifecycle = gameObject.AddComponent<UnityLifecycle>();
			
			injector.Provide<ILog>(logger);
			injector.Provide<ILifecycle>(lifecycle);
			injector.Provide<ICoroutineProvider>(lifecycle);

			context.Inject(injector);
			container.SetContext(context);
		}
	}
}
