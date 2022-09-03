using System.Reflection;
using System.Runtime.CompilerServices;

namespace MVVM.ViewModel
{
	public class BindablePropertiesCache : PropertiesCache<IBindableProperty>
	{
		protected override bool IsRequiredField(FieldInfo info)
		{
			var isBindableProperty = typeof(IBindableProperty).IsAssignableFrom(info.FieldType);
			var isGenerated = info.GetCustomAttribute(typeof(CompilerGeneratedAttribute)) != null;
			return isBindableProperty && !isGenerated;
		}
	}
}
