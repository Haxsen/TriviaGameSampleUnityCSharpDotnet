using MVVM.ViewModel;

using System;

namespace MVVM.DataBinding
{
	public interface IContextContainer
	{
		public event Action OnContextChange;

		public IContextContainer ParentContainer { get; }

		public IViewModelContext Context { get; }

		public void SetParentContainer(IContextContainer parentContainer);
	}
}
