namespace MVVM.ViewModel
{
	public class IntProperty : SettableProperty<int>
	{
		public IntProperty(int value = default(int))
			: base(value)
		{
		}
	}
}
