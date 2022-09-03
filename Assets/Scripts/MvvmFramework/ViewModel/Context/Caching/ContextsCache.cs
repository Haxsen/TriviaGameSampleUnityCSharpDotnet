using System.Reflection;
using System.Runtime.CompilerServices;

namespace MVVM.ViewModel
{
	public class ContextsCache : PropertiesCache<IViewModelContext>
	{
		protected override bool IsRequiredField(FieldInfo info)
		{
			var isContextType = typeof(IViewModelContext).IsAssignableFrom(info.FieldType);
			var isGenerated = info.GetCustomAttribute(typeof(CompilerGeneratedAttribute)) != null;
			return isContextType && !isGenerated;
		}
	}
}
