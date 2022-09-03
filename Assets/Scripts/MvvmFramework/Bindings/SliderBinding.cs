using UnityEngine;
using UnityEngine.UI;

using System;

using MVVM.DataBinding;

using Sirenix.OdinInspector;

namespace Bindings
{
	public class SliderBinding : FloatBinding
	{
		enum Property
		{
			Value,
			MinValue,
			MaxValue
		}

		[Header("Components")]
		[SerializeField, Required] Slider _slider;

		[Header("Initialization")]
		[SerializeField] Property _property;


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
			switch (_property)
			{
				case Property.Value:
					_slider.value = value;
					break;
				case Property.MinValue:
					_slider.minValue = value;
					break;
				case Property.MaxValue:
					_slider.maxValue = value;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
