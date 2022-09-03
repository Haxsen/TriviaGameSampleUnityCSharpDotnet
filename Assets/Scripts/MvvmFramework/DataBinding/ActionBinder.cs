using UnityEngine;
using Object = UnityEngine.Object;

using System;

using MVVM.ViewModel;

using Sirenix.OdinInspector;

namespace MVVM.DataBinding
{
	[Serializable]
	public class ActionBinder : IBinding
	{
		[SerializeField, Required] string _path;

		BindingPath _bindingPath;
		IViewModelContext _context;
		Action<object[]> _action;

		Object _currentObject;

#if UNITY_EDITOR
		[ShowInInspector, ReadOnly, FoldoutGroup("Runtime")]
		MonoBehaviour _contextContainer;

		[ShowInInspector, ReadOnly, FoldoutGroup("Runtime")]
		string _contextType;
#endif


		#region Properties

		public bool IsBound => _action != null;

		public Action<object[]> Action => _action;

		public string Path
		{
			get => _path;
			set => _path = value;
		}

		#endregion


		public void SetObjectForLog(Object currentObject)
		{
			_currentObject = currentObject;
		}

		public void SetContextContainer(IContextContainer container)
		{
			UpdateContextByPath(container);
			if (_context == null)
			{
				Debug.LogError($"{nameof(ActionBinder)}.{nameof(SetContextContainer)}. No context found by path: {_path}", _currentObject);
				return;
			}

			UpdateAction();
		}

		public void Execute(object[] callParams)
		{
			Action.Invoke(callParams);
		}


		void UpdateContextByPath(IContextContainer container)
		{
			_bindingPath = new BindingPath(_path);
			var skippedContainersCount = _bindingPath.SkippedContainers;
			var foundContainer = container.FindContainer(skippedContainersCount);
#if UNITY_EDITOR
			_contextContainer = foundContainer as MonoBehaviour;
#endif
			if (foundContainer == null)
			{
				Debug.LogError(
					$"{nameof(ActionBinder)}.{nameof(UpdateContextByPath)}. No container found. Skipped count: {skippedContainersCount}", _currentObject);
#if UNITY_EDITOR
				_contextType = string.Empty;
#endif
				return;
			}

			var segments = _bindingPath.WithoutLastSegment().Segments;
			_context = foundContainer.Context.FindContext(segments);
#if UNITY_EDITOR
			_contextType = _context.GetType().Name;
#endif
		}

		void UpdateAction()
		{
			var segments = _bindingPath.Segments;
			var lastSegment = segments[segments.Count - 1];
			var propertyName = lastSegment.PathPart;
			_context.TryGetMethod(propertyName, out _action);
			if (_action == null)
			{
				Debug.LogError($"{nameof(ActionBinder)}.{nameof(UpdateAction)}. Method not found. Path: {_path}", _currentObject);
				return;
			}
		}
	}
}
