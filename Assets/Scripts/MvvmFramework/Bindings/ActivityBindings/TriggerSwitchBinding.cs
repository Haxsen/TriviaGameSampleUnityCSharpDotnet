using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

using MVVM.DataBinding;

namespace MVVM.Bindings
{
	public class TriggerSwitchBinding : IntBinding
	{
		[SerializeField] List<UnityEvent> _actions;


		protected override bool ProcessIfDisabled => false;


		protected override void SetValue(int value)
		{
			var action = _actions[value];
			action?.Invoke();
		}
	}
}
