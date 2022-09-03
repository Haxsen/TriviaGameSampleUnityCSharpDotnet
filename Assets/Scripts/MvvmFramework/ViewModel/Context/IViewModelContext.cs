using System;
using System.Collections.Generic;

namespace MVVM.ViewModel
{
	public interface IViewModelContext
	{
		Dictionary<string, IBindableProperty> GetProperties();
		
		Dictionary<string, IViewModelContext> GetContexts();
		
		Dictionary<string, ContextsList> GetCollections();
		
		Dictionary<string, Action<object[]>> GetMethods();

		bool TryGetProperty(string name, out IBindableProperty property);

		bool TryGetMethod(string name, out Action<object[]> action);

		bool TryGetContext(string name, out IViewModelContext context);

		bool TryGetCollection(string name, out ContextsList context);

		void Enable();

		void Disable();

		void Destroy();
	}
}
