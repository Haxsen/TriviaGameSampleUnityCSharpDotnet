using System;
using System.Collections.Generic;

namespace MVVM.ViewModel
{
	public class Injector
	{
		// TODO: temporary solution till injector provided by reference
		public static Injector Instance { get; private set; }


		readonly Dictionary<Type, object> _implementations = new Dictionary<Type, object>();
		ILog _log;


		ILog Log => _log ??= GetLogger();


		public Injector()
		{
			if (Instance != null)
			{
				Instance.Log?.Error("Instance already exist");
			}
			Instance = this;
		}


		public void Provide<T>(T implementation) where T : class
		{
			Provide(typeof(T), implementation);
		}

		public void Provide(Type type, object implementation)
		{
			if (_implementations.ContainsKey(type))
			{
				Log?.Warning($"{type.Name} implementation already provided");
			}
			else
			{
				_implementations[type] = implementation;
			}
		}

		public T Get<T>() where T : class
		{
			var impl = Get(typeof(T));

			var typedImpl = impl as T;
			return typedImpl;
		}

		public object Get(Type type)
		{
			var found = _implementations.TryGetValue(type, out var impl);
			if (!found)
			{
				Log?.Error($"No provider found for {type.Name}");
				return null;
			}

			if (!type.IsInstanceOfType(impl))
			{
				Log?.Error($"Provided implementation ({impl.GetType().Name}) is not assignable to {type.Name}");
				return null;
			}

			return impl;
		}

		ILog GetLogger()
		{
			var found = _implementations.TryGetValue(typeof(ILog), out var logger);
			if (!found)
			{
				return null;
			}
			return logger as ILog;
		}
	}
}
