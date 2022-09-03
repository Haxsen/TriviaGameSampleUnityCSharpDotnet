using UnityEngine.UI;

using MVVM.DataBinding.BooleanBinding;

using Sirenix.OdinInspector;

namespace Bindings
{
	public class InteractivityBinding : BooleanBinding
	{
		[ShowInInspector, ReadOnly] Selectable _target;


		Selectable Target => _target ??= GetComponent<Selectable>();


		protected override void SetValue(bool value)
		{
			Target.interactable = value;
		}
	}
}
