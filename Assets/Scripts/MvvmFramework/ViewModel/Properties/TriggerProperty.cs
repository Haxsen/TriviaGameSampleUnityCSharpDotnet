namespace MVVM.ViewModel
{
	public class TriggerProperty : BooleanProperty
	{
		public void Set()
		{
			NotifyChanged();
		}
	}

	public class TriggerProperty<T> : SettableProperty<T>
	{
		public override void Set(T value)
		{
			_value = value;
			NotifyChanged();
		}
	}
}
