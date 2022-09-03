using UnityEngine;
using UnityEngine.UI;

using MVVM.DataBinding;

namespace MVVM.Bindings
{
	[RequireComponent(typeof(Toggle))]
	public class ToggleValueChangeBinding : ActionBinding
	{
		[SerializeField, HideInInspector] Toggle _toggle;

		protected override object[] CallParams => new object[] { _toggle.isOn };


		#region MonoCallbacks

		void OnValidate()
		{
			if (_toggle == null)
			{
				_toggle = GetComponent<Toggle>();
			}
		}

		void OnDestroy()
		{
			_toggle.onValueChanged.RemoveListener(ChangeValue);
		}

		#endregion MonoCallbacks


		protected override void OnBind()
		{
			_toggle.onValueChanged.AddListener(ChangeValue);
		}


		void ChangeValue(bool value)
		{
			Execute();
		}
	}
}
