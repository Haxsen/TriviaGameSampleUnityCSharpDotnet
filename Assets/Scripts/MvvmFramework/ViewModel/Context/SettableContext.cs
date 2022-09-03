namespace MVVM.ViewModel
{
	public abstract class SettableContext<T> : ViewModelContext, ISettable<T>
	{
		T _data;


		public T Data => _data;


		public void SetData(T data)
		{
			_data = data;
			Set(data);
		}

		protected abstract void Set(T data);
	}
}
