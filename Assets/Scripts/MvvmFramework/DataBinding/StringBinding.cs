using MVVM.ViewModel;

namespace MVVM.DataBinding
{
	public abstract class StringBinding : SinglePropertyBinding
	{
		protected override void ProcessChange()
		{
			var value = Property switch
			{
				IBindableProperty<string> stringProp => stringProp.GetValue(),
				IBindableProperty<int> intProp => intProp.GetValue().ToString(),
				IBindableProperty<float> floatProp => floatProp.GetValue().ToString(),
				IBindableProperty<bool> boolProp => boolProp.GetValue().ToString(),
				_ => Property.GetBoxedValue().ToString()
			};

			SetValue(value);
		}

		protected abstract void SetValue(string value);
	}
}
