using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using MVVM.DataBinding;

using Object = System.Object;

using Sirenix.OdinInspector;

[RequireComponent(typeof(ScrollRect))]
public class VerticalNormalizedScrollPositionUpdateBinding : ActionBinding, IEndDragHandler
{
	[Header("Components")]
	[ShowInInspector, ReadOnly] ScrollRect _scrollRect;

	[Header("Runtime")] 
	[ShowInInspector, ReadOnly] float _verticalNormalizedScrollPosition = 1;
	
	#region Properties
	
	protected override object[] CallParams => new Object[] { _verticalNormalizedScrollPosition };

	#endregion
	
	#region Mono Callbacks

	void Awake()
	{
		_scrollRect = GetComponent<ScrollRect>();
	}

	#endregion
	
	public void OnEndDrag(PointerEventData eventData)
	{
		_verticalNormalizedScrollPosition = _scrollRect.verticalNormalizedPosition;
		Execute();
	}
}
