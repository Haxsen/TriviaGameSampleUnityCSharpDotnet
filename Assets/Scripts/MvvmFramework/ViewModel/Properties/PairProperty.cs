using MVVM.ViewModel;

namespace ViewModel
{
	public class PairProperty<T, V> : SettableProperty<(T value1, V value2)>
	{
		public PairProperty((T, V) value = default) : base(value)
		{
		}
	}
}
