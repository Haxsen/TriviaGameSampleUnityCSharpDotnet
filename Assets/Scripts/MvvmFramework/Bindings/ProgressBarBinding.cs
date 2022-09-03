using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

using MVVM.DataBinding;

using Sirenix.OdinInspector;

public class ProgressBarBinding : FloatBinding
{
	[Header("Components")]
	[SerializeField, Required] Slider _slider;

	[Header("Initialization")]
	[SerializeField] float _fillAnimationDuration = 0.4f;

	Tweener _sliderAnimation;

	#region MonoCallbacks

	void Reset()
	{
		if (!_slider)
		{
			_slider = GetComponent<Slider>();
		}
	}

	#endregion MonoCallbacks

	protected override void SetValue(float value)
	{
		if (value == 0f)
		{
			_slider.value = value;

		}
		else
		{
			if (_sliderAnimation != null && _sliderAnimation.IsPlaying())
			{
				_sliderAnimation.Kill();
			}
			_sliderAnimation = _slider.DOValue(value, _fillAnimationDuration)
				.SetEase(Ease.Linear)
				.Play();
		}
	}
}
