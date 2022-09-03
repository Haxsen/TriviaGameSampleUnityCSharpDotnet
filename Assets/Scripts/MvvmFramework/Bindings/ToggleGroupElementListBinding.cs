using UnityEngine;
using UnityEngine.UI;

using MVVM.DataBinding;

namespace MVVM.Bindings
{
	[RequireComponent(typeof(ToggleGroup))]
	public class ToggleGroupElementListBinding : ListBinding
	{
		[SerializeField, HideInInspector] ToggleGroup _toggleGroup;


		void OnValidate()
		{
			if(!_toggleGroup)
			{
				_toggleGroup = GetComponent<ToggleGroup>();
			}
		}


		protected override ContextContainer GetNewInstance(ContextContainer original, IContextContainer container)
		{
			var newInstance = base.GetNewInstance(original, container);

			var toggle = newInstance.GetComponent<Toggle>();
			toggle.group = _toggleGroup;

			return newInstance;
		}
	}
}
