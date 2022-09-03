using UnityEngine.EventSystems;

using MVVM.DataBinding;

namespace MVVM.Bindings
{
	public class ClickBinding : ActionBinding, IPointerClickHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			Execute();
		}
	}
}
