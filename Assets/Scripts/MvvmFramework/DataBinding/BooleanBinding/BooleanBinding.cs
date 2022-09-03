using UnityEngine;

using System;

using Sirenix.OdinInspector;
using UnityEngine.Serialization;


namespace MVVM.DataBinding.BooleanBinding
{
	public abstract class BooleanBinding : SinglePropertyBinding
	{
		public enum PropertyType
		{
			Boolean,
			String,
			Int,
			Float,
			Collection
		}


		[SerializeField] PropertyType _propertyType;
		[SerializeField, LabelText("Invert")] bool _isInverted;

		[SerializeField, ShowIf("_propertyType", PropertyType.Boolean)] BooleanComparator _booleanComparator = new BooleanComparator();
		[SerializeField, ShowIf("_propertyType", PropertyType.String)] StringComparator _stringComparator = new StringComparator();
		[SerializeField, ShowIf("_propertyType", PropertyType.Int)] IntComparator _intComparator = new IntComparator();
		[SerializeField, ShowIf("_propertyType", PropertyType.Float)] FloatComparator _floatComparator = new FloatComparator();
		[SerializeField, ShowIf("_propertyType", PropertyType.Collection)] CollectionLengthComparator _collectionLengthComparator = new CollectionLengthComparator();


		protected override void ProcessChange()
		{
			IPropertyComparator comparator = _propertyType switch
			{
				PropertyType.Boolean => _booleanComparator,
				PropertyType.String => _stringComparator,
				PropertyType.Int => _intComparator,
				PropertyType.Float => _floatComparator,
				PropertyType.Collection => _collectionLengthComparator,
				_ => throw new ArgumentOutOfRangeException()
			};

			var result = comparator.GetResult(Property);
			result = result != _isInverted;
			SetValue(result);
		}

		protected abstract void SetValue(bool value);
	}
}
