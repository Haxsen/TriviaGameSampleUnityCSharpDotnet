using UnityEngine;

using System;

using MVVM.ViewModel;

using Sirenix.OdinInspector;

namespace MVVM.DataBinding
{
	public abstract class BaseContextContainer : MonoBehaviour, IContextContainer
	{
		public event Action OnContextChange;


		protected IViewModelContext _context;


		#region Properties

		public IContextContainer ParentContainer { get; private set; }

		public IViewModelContext Context => _context;


#if UNITY_EDITOR
		[ShowInInspector, ReadOnly, FoldoutGroup("Runtime")] MonoBehaviour ParentContainerComponent => ParentContainer as MonoBehaviour;
		[ShowInInspector, ReadOnly, FoldoutGroup("Runtime")] string ContextTypeName => _context?.GetType().Name;
#endif

		#endregion Properties


		public virtual void SetParentContainer(IContextContainer parentContainer)
		{
			ParentContainer = parentContainer;
		}

		public virtual void SetContext(IViewModelContext context)
		{
			if (_context == context)
			{
				return;
			}
			_context = context;
			OnContextChange?.Invoke();
			UpdateContextsRecursively(transform);
		}


		protected void UpdateContextsRecursively(Transform t)
		{
			UpdateBindings(t);
			foreach (Transform child in t)
			{
				var container = child.GetComponent<IContextContainer>();
				if (container != null)
				{
					container.SetParentContainer(this);
				}
				else
				{
					UpdateContextsRecursively(child);
				}
			}
		}


		void UpdateBindings(Transform t)
		{
			var bindings = t.GetComponents<IBinding>();
			foreach (var binding in bindings)
			{
				binding.SetContextContainer(this);
			}
		}
	}
}
