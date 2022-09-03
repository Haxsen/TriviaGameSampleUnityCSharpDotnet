using MVVM.ViewModel;

namespace MVVM.DataBinding
{
	public abstract class IntBinding : SinglePropertyBinding
	{
		protected override void ProcessChange()
		{
			if (Property is IBindableProperty<int> intProperty)
			{
				var value = intProperty.GetValue();
				SetValue(value);
			}
			else if (Property is IBindableProperty<bool> boolProperty)
			{
				var value = boolProperty.GetValue();
				SetValue(value ? 1 : 0);
			}
		}

		protected abstract void SetValue(int value);
	}
}
