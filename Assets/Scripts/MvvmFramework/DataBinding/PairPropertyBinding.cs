using MVVM.DataBinding;
using MVVM.ViewModel;

public abstract class PairPropertyBinding<T, V> : SinglePropertyBinding
{
	protected override void ProcessChange()
	{
		if (Property is IBindableProperty<(T, V)> property)
		{
			var value = property.GetValue();
			SetValue(value);
		}
	}

	protected abstract void SetValue((T, V) value);
}
