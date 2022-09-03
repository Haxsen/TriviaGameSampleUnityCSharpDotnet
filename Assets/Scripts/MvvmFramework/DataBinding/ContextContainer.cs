using MVVM.ViewModel;

namespace MVVM.DataBinding
{
	public class ContextContainer : BaseContextContainer
	{
		#region MonoCallbacks

		void OnEnable()
		{
			_context?.Enable();
		}

		void OnDisable()
		{
			_context?.Disable();
		}

		void OnDestroy()
		{
			_context?.Destroy();
		}

		#endregion

		public override void SetContext(IViewModelContext context)
		{
			if (Context != null)
			{
				Context.Disable();
				Context.Destroy();
			}
			base.SetContext(context);
			context.Enable();
		}
	}
}
