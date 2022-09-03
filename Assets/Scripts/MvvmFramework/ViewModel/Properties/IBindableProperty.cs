using System;

namespace MVVM.ViewModel
{
	public interface IBindableProperty
	{
		event Action OnChange;

		object GetBoxedValue();
	}

	public interface IBindableProperty<out T> : IBindableProperty
	{
		T GetValue();
	}
}
