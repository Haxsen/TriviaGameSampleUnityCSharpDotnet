using System;

namespace MVVM.ViewModel
{
	public class EnumProperty<T> : IntProperty where T:struct, Enum, IConvertible
	{
		public new virtual T GetValue()
		{
			return (T)(object)base.GetValue();
		}

		public virtual void Set(T value)
		{
			var intValue = (int) (object) value;
			base.Set(intValue);
		}

		public static implicit operator T(EnumProperty<T> property)
		{
			var value = property.GetValue();
			return value;
		}

	}
}
