using System.Reflection;
using System.Runtime.CompilerServices;

namespace MVVM.ViewModel
{
	public class ContextListsCache : PropertiesCache<ContextsList>
	{
		protected override bool IsRequiredField(FieldInfo info)
		{
			var isContextType = typeof(ContextsList).IsAssignableFrom(info.FieldType);
			var isGenerated = info.GetCustomAttribute(typeof(CompilerGeneratedAttribute)) != null;
			return isContextType && !isGenerated;
		}
	}
}
