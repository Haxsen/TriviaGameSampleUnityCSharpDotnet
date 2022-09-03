using UnityEngine;

using MVVM.DataBinding.BooleanBinding;

using Sirenix.OdinInspector;

namespace MVVM.Bindings
{
	public class GameObjectActivityBinding : BooleanBinding
	{
		[SerializeField, Required] GameObject _target;


		protected override bool ProcessIfDisabled => true;


		#region MonoCallbacks

		void Reset()
		{
			if (!_target)
			{
				_target = gameObject;
			}
		}

		#endregion MonoCallbacks


		protected override void SetValue(bool value)
		{
			_target.SetActive(value);
		}
	}
}
