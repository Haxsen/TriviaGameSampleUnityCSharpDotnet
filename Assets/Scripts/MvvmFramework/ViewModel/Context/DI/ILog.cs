namespace MVVM.ViewModel
{
	public interface ILog
	{
		void Info(string message);

		void Warning(string message);

		void Error(string message);
	}
}
