using MVVM.DataBinding;

namespace Bindings
{
	public class ExternalCallActionBinding : ActionBinding
	{
		object[] _callParams;

		protected override object[] CallParams => _callParams;


		public new void Execute(params object[] args)
		{
			_callParams = args;
			base.Execute();
		}

		public new void Execute()
		{
			_callParams = null;
			base.Execute();
		}
	}
}
