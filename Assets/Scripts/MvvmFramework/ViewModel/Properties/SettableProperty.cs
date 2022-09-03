using System;
using System.Collections.Generic;

namespace MVVM.ViewModel
{
	public class SettableProperty<T> : IBindableProperty<T>
	{
		public event Action OnChange;

		public event Action<T> OnChangeWithValue;


		protected T _value;


		public SettableProperty(T value = default(T))
		{
			_value = value;
		}

		public virtual T GetValue()
		{
			return _value;
		}

		public object GetBoxedValue()
		{
			return GetValue();
		}

		public virtual void Set(T value)
		{
			var isChanged = !EqualityComparer<T>.Default.Equals(value, _value);
			if (isChanged)
			{
				_value = value;
				NotifyChanged();
			}
		}

		public static implicit operator T(SettableProperty<T> property)
		{
			var value = property.GetValue();
			return value;
		}

		protected void NotifyChanged()
		{
			OnChange?.Invoke();
			OnChangeWithValue?.Invoke(_value);
		}
	}
}
