using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MVVM.ViewModel
{
	public abstract class PropertiesCache<T> : MembersInfoCache<FieldInfo, T> where T : class
	{
		protected override void FillInstanceMembers()
		{
			var properties = GetCache();
			foreach (var property in properties)
			{
				var name = property.Key;
				var propObj = property.Value.GetValue(_context);
				var context = propObj as T;
				_instanceMembersByName[name] = context;
			}
		}

		protected override void FillTypeMembers(Type type, Dictionary<string, FieldInfo> members)
		{
			var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (var info in fields)
			{
				if (IsRequiredField(info))
				{
					var name = info.Name;
					if (members.ContainsKey(name))
					{
						throw new ArgumentException("Context with same name already exists");
					}
					else
					{
						members.Add(name, info);
					}
				}
			}
		}

		protected abstract bool IsRequiredField(FieldInfo info);
	}
}
