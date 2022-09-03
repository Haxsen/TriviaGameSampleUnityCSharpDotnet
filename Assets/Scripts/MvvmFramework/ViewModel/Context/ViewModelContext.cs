using System;
using System.Collections.Generic;

namespace MVVM.ViewModel
{
	public class ViewModelContext : IViewModelContext, IInjectionConsumer
	{
		protected readonly BindablePropertiesCache _propertiesCache = new BindablePropertiesCache();
		protected readonly ContextsCache _contextsCache = new ContextsCache();
		protected readonly ContextListsCache _contextListsCache = new ContextListsCache();
		protected readonly MethodsCache _methodsCache = new MethodsCache();

		bool _enabled;

		#region Properties

		protected Injector Injector { get; private set; }

		protected ILog Log => Injector.Get<ILog>();

		#endregion


		public ViewModelContext()
		{
			var type = GetType();
			_propertiesCache.SetType(this, type);
			_contextsCache.SetType(this, type);
			_contextListsCache.SetType(this, type);
			_methodsCache.SetType(this, type);
		}


		public Dictionary<string, IBindableProperty> GetProperties() => _propertiesCache.InstanceMembersByName;
		
		public Dictionary<string, IViewModelContext> GetContexts() => _contextsCache.InstanceMembersByName;
		
		public Dictionary<string, ContextsList> GetCollections() => _contextListsCache.InstanceMembersByName;
		
		public Dictionary<string, Action<object[]>> GetMethods() => _methodsCache.InstanceMembersByName;

		public void Inject(Injector injector)
		{
			Injector = injector;
			foreach (var context in _contextsCache.InstanceMembers)
			{
				if (context is IInjectionConsumer ctx)
				{
					ctx.Inject(injector);
				}
			}

			foreach (var context in _contextListsCache.InstanceMembers)
			{
				context.Inject(injector);
			}

			_methodsCache.SetLogger(Log);

			Init();
		}

		public bool TryGetProperty(string name, out IBindableProperty property)
		{
			var found = _propertiesCache.TryGetInstanceMember(name, out property);
			return found;
		}

		public bool TryGetContext(string name, out IViewModelContext context)
		{
			var found = _contextsCache.TryGetInstanceMember(name, out context);
			return found;
		}

		public bool TryGetCollection(string name, out ContextsList collection)
		{
			var found = _contextListsCache.TryGetInstanceMember(name, out collection);
			return found;
		}

		public bool TryGetMethod(string name, out Action<object[]> action)
		{
			var found = _methodsCache.TryGetInstanceMember(name, out action);
			return found;
		}

		public void Enable()
		{
			if (_enabled)
			{
				return;
			}

			_enabled = true;

			foreach (var context in _contextsCache.InstanceMembers)
			{
				context.Enable();
			}

			OnEnable();
		}

		public void Disable()
		{
			if (!_enabled)
			{
				return;
			}

			_enabled = false;

			foreach (var context in _contextsCache.InstanceMembers)
			{
				context.Disable();
			}

			OnDisable();
		}

		public void Destroy()
		{
			foreach (var context in _contextsCache.InstanceMembers)
			{
				context.Destroy();
			}

			OnDestroy();
		}


		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnDestroy()
		{
		}

		protected virtual void Init()
		{
		}
	}
}
