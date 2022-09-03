using UnityEngine;

using MVVM.ViewModel;

namespace MVVM.DataBinding.BooleanBinding
{
	public abstract class PropertyComparator<T> : IPropertyComparator
	{
		protected T GetValue(IBindableProperty property)
		{
			var typedProperty = property as IBindableProperty<T>;
			if (typedProperty == null)
			{
				Debug.LogError($"{GetType().Name}.{nameof(GetValue)}. Property has invalid type. Expected: {nameof(IBindableProperty<T>)}, Provided: {property?.GetType().Name}");
				return default(T);
			}
			var value = typedProperty.GetValue();
			return value;
		}

		public abstract bool GetResult(IBindableProperty property);
	}
}
