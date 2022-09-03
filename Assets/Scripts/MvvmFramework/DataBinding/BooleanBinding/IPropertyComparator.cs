using MVVM.ViewModel;

namespace MVVM.DataBinding.BooleanBinding
{
	public interface IPropertyComparator
	{
		bool GetResult(IBindableProperty property);
	}
}
