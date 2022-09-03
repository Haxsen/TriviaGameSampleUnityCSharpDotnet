using UnityEngine;

using System;

using MVVM.ViewModel;

using Sirenix.OdinInspector;

namespace MVVM.DataBinding.BooleanBinding
{
	[Serializable]
	public class IntComparator : PropertyComparator<int>
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

		[SerializeField, HideIf(nameof(_mode), Mode.Between)] int _compareWith;

		[SerializeField, ShowIf(nameof(_mode), Mode.Between)] int _firstValue;
		[SerializeField, ShowIf(nameof(_mode), Mode.Between)] int _secondValue;


		public override bool GetResult(IBindableProperty property)
		{
			var value = GetValue(property);
			var result = Compare(value);
			return result;
		}


		bool Compare(int value)
		{
			var result = _mode switch
			{
				Mode.EqualsTo => value == _compareWith,
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
