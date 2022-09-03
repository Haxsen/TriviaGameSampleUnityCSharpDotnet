using UnityEngine;

using System;

using Sirenix.OdinInspector;

namespace MVVM.DataBinding
{
	public abstract class ActionBinding : MonoBehaviour, IBinding
	{
		static readonly object[] EmptyParamsList = Array.Empty<object>();

		[SerializeField, HideInInspector] ActionBinder _binder = new ActionBinder();

		IContextContainer _container;


		#region Properties

		protected virtual object[] CallParams => EmptyParamsList;


		[ShowInInspector]
		string Path
		{
			get => _binder.Path;
			set => _binder.Path = value;
		}

		#endregion Properties

		public void SetContextContainer(IContextContainer container)
		{
			if (_container == container)
			{
				return;
			}

			if (_container != null)
			{
				_container.OnContextChange -= Bind;
			}

			_container = container;
			_binder.SetObjectForLog(this);
			Bind();
			container.OnContextChange += Bind;
			OnBind();
		}

		void Bind()
		{
			_binder.SetContextContainer(_container);
			if (!_binder.IsBound)
			{
				Debug.LogError($"{nameof(ActionBinding)}.{nameof(SetContextContainer)}. No context found by path: {Path}", gameObject);
			}
		}


		protected virtual void OnBind()
		{
		}

		protected void Execute()
		{
			if (!_binder.IsBound)
			{
				Debug.LogError($"{nameof(ActionBinding)}.{nameof(Execute)}. Try to call empty action. Path: {Path}", gameObject);
				return;
			}

			_binder.Execute(CallParams);
		}
	}
}
