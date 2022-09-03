using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;

using MVVM.DataBinding;

using Sirenix.OdinInspector;

namespace Bindings
{
	public class LoadingProgressBinding : FloatBinding
	{
		[Header("Components")]
		[SerializeField] Slider _slider;
		[SerializeField] TextMeshProUGUI _label;

		[Header("Initialization")]
		[SerializeField] float _progressSetDuration = 0.3f;
		[SerializeField] bool _resetOnEnable = true;

		[ShowInInspector, ReadOnly, FoldoutGroup("Runtime")] Tweener _tween;


		protected override void OnEnable()
		{
			if (_resetOnEnable)
			{
				UpdateUI(0f);
			}
			base.OnEnable();
		}

		protected override void SetValue(float progressValue)
		{
			_tween = DOTween.To(() => _slider.value, UpdateUI, progressValue, _progressSetDuration);
		}


		void UpdateUI(float progress)
		{
			var roundedProgressBarValue = Mathf.RoundToInt(progress * 100);
			_label.text = $"{roundedProgressBarValue} %";
			_slider.value = progress;
		}
	}
}
