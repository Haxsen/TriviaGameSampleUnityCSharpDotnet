using UnityEngine;

using System;

using MVVM.ViewModel;

using Sirenix.OdinInspector;

namespace MVVM.DataBinding.BooleanBinding
{
	[Serializable]
	public class FloatComparator : PropertyComparator<float>
	{
		public enum Mode
		{
			EqualsTo,
			LessThan,
			GreaterThan,
			LessOrEqualsTo,
			GreaterOrEqualsTo,
			Between
		}


		[SerializeField] Mode _mode;

		[SerializeField, HideIf(nameof(_mode), Mode.Between)] float _compareWith;

		[SerializeField, ShowIf(nameof(_mode), Mode.Between)] float _firstValue;
		[SerializeField, ShowIf(nameof(_mode), Mode.Between)] float _secondValue;


		public override bool GetResult(IBindableProperty property)
		{
			var value = GetValue(property);
			var result = Compare(value);
			return result;
		}


		bool Compare(float value)
		{
			var result = _mode switch
			{
				Mode.EqualsTo => Math.Abs(value - _compareWith) < 0.0001,
				Mode.LessThan => value < _compareWith,
				Mode.GreaterThan => value > _compareWith,
				Mode.LessOrEqualsTo => value <= _compareWith,
				Mode.GreaterOrEqualsTo => value >= _compareWith,
				Mode.Between => value >= _firstValue && value <= _secondValue,
				_ => throw new ArgumentOutOfRangeException()
			};

			return result;
		}
	}
}
