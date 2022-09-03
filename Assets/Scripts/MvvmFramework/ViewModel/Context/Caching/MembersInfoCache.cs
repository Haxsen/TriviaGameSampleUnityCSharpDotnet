using System;
using System.Collections.Generic;
using System.Reflection;

namespace MVVM.ViewModel
{
	public abstract class MembersInfoCache<T, TMember> where T : MemberInfo
	{
		static readonly Dictionary<string, Dictionary<string, T>> _typeMembersByType = new Dictionary<string, Dictionary<string, T>>();

		protected readonly Dictionary<string, TMember> _instanceMembersByName = new Dictionary<string, TMember>();
		protected IViewModelContext _context;
		protected ILog _logger;

		Type _type;
		bool _isInitialized;


		public Dictionary<string, TMember>.ValueCollection InstanceMembers
		{
			get
			{
				if (!_isInitialized)
				{
					InitializeInstanceCache();
				}
				return _instanceMembersByName.Values;
			}
		}

		public Dictionary<string, TMember> InstanceMembersByName => _instanceMembersByName;


		public void SetType(IViewModelContext context, Type type)
		{
			_context = context;
			_type = type;
		}

		public void SetLogger(ILog logger)
		{
			_logger = logger;
		}

		public bool TryGetInstanceMember(string name, out TMember context)
		{
			if (!_isInitialized)
			{
				InitializeInstanceCache();
			}

			var found = _instanceMembersByName.TryGetValue(name, out context);
			return found;
		}


		protected Dictionary<string, T> GetCache()
		{
			var typeFullName = _type.FullName;
			if (_typeMembersByType.TryGetValue(typeFullName, out var members))
			{
				return members;
			}
			FillCache(_type);
			return _typeMembersByType[typeFullName];
		}

		protected abstract void FillTypeMembers(Type type, Dictionary<string, T> members);

		protected abstract void FillInstanceMembers();


		void FillCache(Type type)
		{
			var members = new Dictionary<string, T>();
			FillTypeMembers(type, members);
			_typeMembersByType[type.FullName] = members;
		}

		void InitializeInstanceCache()
		{
			FillInstanceMembers();
			_isInitialized = true;
		}
	}
}
