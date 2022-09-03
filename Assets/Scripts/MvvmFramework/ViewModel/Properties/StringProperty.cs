namespace MVVM.ViewModel
{
	public class StringProperty : SettableProperty<string>
	{
		public StringProperty(string value = default(string))
			: base(value)
		{
		}
	}
}
