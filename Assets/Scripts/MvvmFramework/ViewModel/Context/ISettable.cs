namespace MVVM.ViewModel
{
	public interface ISettable<in T>
	{
		void SetData(T data);
	}
}
