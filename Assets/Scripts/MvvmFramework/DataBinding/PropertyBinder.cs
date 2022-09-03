using UnityEngine;

using System;

using MVVM.ViewModel;

using Sirenix.OdinInspector;
using Object = UnityEngine.Object;


namespace MVVM.DataBinding
{
	[Serializable]
	public class PropertyBinder : IBinding
	{
		[SerializeField, Required] protected string _path;

		protected IViewModelContext _context;
		protected IBindableProperty _property;
		protected BindingPath _bindingPath;
		protected IContextContainer _container;

		protected Object _currentObject;

#if UNITY_EDITOR
		[ShowInInspector, ReadOnly, FoldoutGroup("Runtime")] MonoBehaviour _contextContainer;
		[ShowInInspector, ReadOnly, FoldoutGroup("Runtime")] string _contextType;
#endif


		#region Properties

		public bool IsBound => _property != null;

		public IBindableProperty Property => _property;

		public string Path
		{
			get => _path;
			set => _path = value;
		}

		protected BindingPath BindingPath => _bindingPath ??= new BindingPath(_path);

		#endregion


		public void SetObjectForLog(Object currentObject)
		{
			_currentObject = currentObject;
		}

		public void SetContextContainer(IContextContainer container)
		{
			UpdateContextByPath(container);
			if (_context != null)
			{
				UpdateProperty();
			}
			else
			{
				Debug.LogError($"{nameof(PropertyBinder)}.{nameof(SetContextContainer)}. No context found by path: {Path}", _currentObject);
			}
		}

		public void SubscribeOnProperty(Action onChange)
		{
			if (_property != null)
			{
				_property.OnChange += onChange;
			}
		}

		public void UnsubscribeFromProperty(Action onChange)
		{
			if (_property != null)
			{
				_property.OnChange -= onChange;
			}
		}

		void UpdateContextByPath(IContextContainer container)
		{
			var skippedContainersCount = BindingPath.SkippedContainers;
			var foundContainer = container.FindContainer(skippedContainersCount);
			_container = foundContainer;
#if UNITY_EDITOR
			_contextContainer = foundContainer as MonoBehaviour;
#endif
			if (foundContainer == null)
			{
				Debug.LogError($"{nameof(SinglePropertyBinding)}.{nameof(UpdateContextByPath)}. No container found. Skipped count: {skippedContainersCount}", _currentObject);
#if UNITY_EDITOR
				_contextType = string.Empty;
#endif
				return;
			}

			var segments = BindingPath.WithoutLastSegment().Segments;
			_context = foundContainer.Context.FindContext(segments);
#if UNITY_EDITOR
			_contextType = _context?.GetType().Name;
#endif
		}

		IBindableProperty FindProperty(IViewModelContext context, BindingPath bindingPath)
		{
			var segments = bindingPath.Segments;
			var lastSegment = segments[segments.Count - 1];
			var propertyName = lastSegment.PathPart;
			context.TryGetProperty(propertyName, out var property);
			return property;
		}

		void UpdateProperty()
		{
			_property = FindProperty(_context, BindingPath);
			if (_property == null)
			{
				Debug.LogError($"{nameof(PropertyBinder)}.{nameof(UpdateProperty)}. No property found by path: {Path}", _currentObject);
			}
		}
	}
}
