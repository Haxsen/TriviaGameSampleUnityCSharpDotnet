using UnityEngine;
using UnityEngine.UI;

using System.Collections;

using MVVM.DataBinding;

using Sirenix.OdinInspector;

[RequireComponent(typeof(ScrollRect))]
public class VerticalNormalizedScrollPositionBinding : FloatBinding
{
	[Header("Components")] 
	[ShowInInspector, ReadOnly] ScrollRect _scrollRect;

	#region Mono Callbacks

	void Awake()
	{
		_scrollRect = GetComponent<ScrollRect>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		StartCoroutine(UpdateValueIE());
	}
	
	#endregion
	
	protected override void SetValue(float verticalNormalizedPosition)
	{
		_scrollRect.verticalNormalizedPosition = verticalNormalizedPosition;
	}
	
	IEnumerator UpdateValueIE()
	{
		yield return null;
		ProcessChange();
	}
}
