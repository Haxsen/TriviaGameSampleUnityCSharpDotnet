using UnityEngine;
using UnityEngine.Events;

using MVVM.DataBinding;

using Sirenix.OdinInspector;


namespace Bindings
{
	public class TriggerBinding : SinglePropertyBinding
	{
		[SerializeField, Required] UnityEvent _action;

		[SerializeField] bool _shouldProcessOnBind = false;
		[SerializeField] bool _shouldSkipProcessingOnEnable = true;


		#region Properties

		protected override bool ShouldProcessOnBind => _shouldProcessOnBind;

		protected override bool ShouldSkipProcessingOnEnable => _shouldSkipProcessingOnEnable;

		#endregion


		protected override void ProcessChange()
		{
			_action?.Invoke();
		}
	}
}
