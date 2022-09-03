namespace MVVM.ViewModel
{
	public class BooleanProperty : SettableProperty<bool>
	{
		public BooleanProperty(bool value = default(bool))
			: base(value)
		{
		}
	}
}
