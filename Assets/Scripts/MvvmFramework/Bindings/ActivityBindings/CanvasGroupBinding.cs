using UnityEngine;

using MVVM.DataBinding.BooleanBinding;

namespace MVVM.Bindings
{
	[RequireComponent(typeof(CanvasGroup))]
	public class CanvasGroupBinding : BooleanBinding
	{
		[SerializeField, HideInInspector] CanvasGroup _canvasGroup;


		void OnValidate()
		{
			if (_canvasGroup == null)
			{
				_canvasGroup = GetComponent<CanvasGroup>();
			}
		}


		protected override void SetValue(bool isTrue)
		{
			if (isTrue)
			{
				_canvasGroup.alpha = 1;
				_canvasGroup.interactable = true;
			}
			else
			{
				_canvasGroup.alpha = 0;
				_canvasGroup.interactable = false;
			}
		}
	}
}
