using System.Collections.Generic;
using UnityEngine;

using MVVM.DataBinding;

namespace MVVM.Bindings
{
	public class GameObjectsSwitchBinding : IntBinding
	{
		[SerializeField] List<GameObject> _targets;


		protected override bool ProcessIfDisabled => false;


		protected override void SetValue(int value)
		{
			var itemToActivate = _targets[value];
			if (itemToActivate)
			{
				itemToActivate.SetActive(true);
			}
			foreach (var go in _targets)
			{
				if (go && go != itemToActivate)
				{
					go.SetActive(false);
				}
			}
		}
	}
}
