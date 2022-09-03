using UnityEngine;

using MVVM.DataBinding.BooleanBinding;

using Sirenix.OdinInspector;

namespace MVVM.Bindings
{
	[RequireComponent(typeof(CanvasGroup))]
	public class CanvasGroupInteractableBinding : BooleanBinding
	{
		[Header("Runtime")]
		[ShowInInspector, ReadOnly] CanvasGroup _canvasGroup;
		
		#region Mono Callbacks

		void Awake()
		{
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		#endregion

		protected override void SetValue(bool value)
		{
			_canvasGroup.interactable = value;
		}
	}	
}
