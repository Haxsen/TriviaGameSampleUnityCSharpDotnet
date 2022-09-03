using MVVM.ViewModel;

namespace MVVM.DataBinding
{
	public abstract class FloatBinding : SinglePropertyBinding
	{
		protected override void ProcessChange()
		{
			if (Property is IBindableProperty<float> floatProp)
			{
				var value = floatProp.GetValue();
				SetValue(value);
			}
		}

		protected abstract void SetValue(float value);
	}
}
