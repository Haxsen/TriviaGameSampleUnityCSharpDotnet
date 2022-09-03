using UnityEngine;
using UnityEngine.UI;

using MVVM.DataBinding.BooleanBinding;

namespace MVVM.Bindings
{
	[RequireComponent(typeof(Toggle))]
	public class ToggleValueBinding : BooleanBinding
	{
		[SerializeField, HideInInspector] Toggle _toggle;


		void OnValidate()
		{
			if (_toggle == null)
			{
				_toggle = GetComponent<Toggle>();
			}
		}


		protected override void SetValue(bool value)
		{
			_toggle.isOn = value;
		}
	}
}
