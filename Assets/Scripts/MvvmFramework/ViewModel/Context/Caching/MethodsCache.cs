using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MVVM.ViewModel
{
	public class MethodsCache : MembersInfoCache<MethodInfo, Action<object[]>>
	{
		protected override void FillInstanceMembers()
		{
			var methods = GetCache();
			foreach (var field in methods)
			{
				var name = field.Key;
				void MethodCall(object[] args)
				{
					if (!HasBindingAttribute(field.Value))
					{
						_logger.Warning($"<b>{field.Value.DeclaringType.Name}.{field.Value.Name}</b> has no BindingAction attribute.");
					}
					field.Value.Invoke(_context, args);
				}
				_instanceMembersByName[name] = MethodCall;
			}
		}

		protected override void FillTypeMembers(Type type, Dictionary<string, MethodInfo> members)
		{
			var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (var info in methods)
			{
				// check if it's property getter/setter
				if (!info.IsSpecialName && info.DeclaringType != typeof(ViewModelContext) && info.DeclaringType != typeof(object))
				{
					var name = info.Name;
					members[name] = info;
				}
			}
		}


		bool HasBindingAttribute(MethodInfo value)
		{
			return value.CustomAttributes.Any(x => x.AttributeType == typeof(BindingAction));
		}
	}
}
