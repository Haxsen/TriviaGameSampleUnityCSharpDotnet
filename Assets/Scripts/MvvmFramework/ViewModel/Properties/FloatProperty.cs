namespace MVVM.ViewModel
{
	public class FloatProperty : SettableProperty<float>
	{
		public FloatProperty(float value = default(float))
			: base(value)
		{
		}
	}
}
