using UnityEngine;

using MVVM.ViewModel;

using Sirenix.OdinInspector;

namespace MVVM.DataBinding
{
	public abstract class SinglePropertyBinding : MonoBehaviour, IBinding
	{
		[SerializeField, HideInInspector] PropertyBinder _binder = new PropertyBinder();

		bool _shouldProcessAfterEnable;


		#region Properties

		protected virtual bool ProcessIfDisabled => false;

		protected virtual bool ShouldProcessOnBind => true;

		protected virtual bool ShouldSkipProcessingOnEnable => false;

		protected IBindableProperty Property => _binder.Property;

		protected IContextContainer Container { get; private set; }


		[ShowInInspector, Required]
		protected string Path
		{
			get => _binder.Path;
			set => _binder.Path = value;
		}

		bool CanProcess => ProcessIfDisabled || gameObject.activeInHierarchy && enabled;

		#endregion


		#region MonoCallbacks

		protected virtual void OnEnable()
		{
			if (_shouldProcessAfterEnable && !ShouldSkipProcessingOnEnable)
			{
				_shouldProcessAfterEnable = false;
				ProcessChange();
			}
		}

		#endregion MonoCallbacks


		public virtual void SetContextContainer(IContextContainer container)
		{
			if (Container == container)
			{
				return;
			}

			if (Container != null)
			{
				Container.OnContextChange -= Bind;
			}

			Container = container;
			Bind();
			container.OnContextChange += Bind;
			OnBind();
		}


		protected virtual void OnBind()
		{
		}


		protected abstract void ProcessChange();


		protected virtual void Bind()
		{
			// unsubscribe from old property if it exists
			UnsubscribeFromProperty();
			_binder.SetObjectForLog(this);
			_binder.SetContextContainer(Container);
			SubscribeToProperty();

			if (ShouldProcessOnBind)
			{
				ProcessChangeInternal();
			}
		}

		void ProcessChangeInternal()
		{
			if (CanProcess)
			{
				ProcessChange();
			}
			_shouldProcessAfterEnable = !CanProcess;
		}

		void SubscribeToProperty()
		{
			_binder.SubscribeOnProperty(ProcessChangeInternal);
		}

		void UnsubscribeFromProperty()
		{
			_binder.UnsubscribeFromProperty(ProcessChangeInternal);
		}
	}
}
