using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

using MVVM.DataBinding;
using MVVM.ViewModel;

using Sirenix.OdinInspector;

namespace MVVM.Bindings
{
	public class ListBinding : SinglePropertyBinding
	{
		[Header("Components")]
		[SerializeField] ContextContainer _template;
		
		[Header("Initialization")]
		[SerializeField] bool _rebuildLayout = false;

		
		[ShowInInspector, FoldoutGroup("Runtime")] readonly List<ContextContainer> _instances = new List<ContextContainer>();
		[ShowInInspector, FoldoutGroup("Runtime")] readonly Stack<ContextContainer> _cache = new Stack<ContextContainer>();
		
		#region Properties

		public virtual int ElementsCount => _instances.Count;

		#endregion

		
		public virtual GameObject GetInstanceByIndex(int index)
		{
			if (_instances.Count == 0 || index >= _instances.Count)
			{
				Debug.LogError($"<b>{nameof(ListBinding)}.{nameof(GetInstanceByIndex)} Index is out of range</b>");
				return null;
			}

			var obj = _instances[index].gameObject;
			return obj;
		}


		protected override void ProcessChange()
		{
			if (Property is ContextsList list)
			{
				var items = list.GetValue();
				Fill(items);
			}
			else
			{
				Debug.LogError($"Found property is not {nameof(ContextsList)}. Provided property type: {Property.GetType().Name}");
			}
		}

		protected virtual ContextContainer GetNewInstance(ContextContainer original, IContextContainer container)
		{
			var instance = Instantiate(original, transform);
			instance.SetParentContainer(container);
			return instance;
		}


		void Fill(List<ViewModelContext> items)
		{
			foreach (var container in _instances)
			{
				_cache.Push(container);
			}
			_instances.Clear();

			for (var i = 0; i < items.Count; i++)
			{
				var itemContext = items[i];
				var instance = GetTemplateInstance();
				instance.transform.SetSiblingIndex(i);
				instance.SetContext(itemContext);
				_instances.Add(instance);
			}

			foreach (var container in _cache)
			{
				container.gameObject.SetActive(false);
			}

			if (_rebuildLayout)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
			}
		}

		ContextContainer GetTemplateInstance()
		{
			var instance = _cache.Count > 0 ?
				GetCachedInstance(_cache) :
				GetNewInstance(_template, Container);

			return instance;
		}

		ContextContainer GetCachedInstance(Stack<ContextContainer> cache)
		{
			var instance = cache.Pop();
			instance.gameObject.SetActive(true);
			return instance;
		}
	}
}
