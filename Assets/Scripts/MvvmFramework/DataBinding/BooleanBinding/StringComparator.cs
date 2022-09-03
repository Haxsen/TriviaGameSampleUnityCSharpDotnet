using UnityEngine;

using System;

using MVVM.ViewModel;

using Sirenix.OdinInspector;


namespace MVVM.DataBinding.BooleanBinding
{
	[Serializable]
	public class StringComparator : PropertyComparator<string>
	{
		public enum CompareMode
		{
			HasValue,
			EqualsTo,
			Contains
		}

		[SerializeField] CompareMode _mode;
		[SerializeField, HideIf(nameof(_mode), CompareMode.HasValue)] string _compareWith;


		public override bool GetResult(IBindableProperty property)
		{
			var value = GetValue(property);
			var result = Compare(_mode, value, _compareWith);
			return result;
		}


		bool Compare(CompareMode mode, string value, string compareWith)
		{
			var result = mode switch
			{
				CompareMode.HasValue => !string.IsNullOrWhiteSpace(value),
				CompareMode.EqualsTo => value == compareWith,
				CompareMode.Contains => value.Contains(compareWith),
				_ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
			};

			return result;
		}
	}
}
