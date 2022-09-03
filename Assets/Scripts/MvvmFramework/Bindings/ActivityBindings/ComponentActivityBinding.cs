using UnityEngine;

using MVVM.DataBinding.BooleanBinding;

using Sirenix.OdinInspector;

namespace MVVM.Bindings
{
	public class ComponentActivityBinding : BooleanBinding
	{
		[SerializeField, Required] MonoBehaviour _target;


		protected override void SetValue(bool value)
		{
			_target.enabled = value;
		}
	}
}
