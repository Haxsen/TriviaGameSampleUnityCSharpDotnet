using System;

using MVVM.ViewModel;


namespace MVVM.DataBinding.BooleanBinding
{
	[Serializable]
	public class BooleanComparator : PropertyComparator<bool>
	{
		public override bool GetResult(IBindableProperty property)
		{
			var value = GetValue(property);
			return value;
		}
	}
}
