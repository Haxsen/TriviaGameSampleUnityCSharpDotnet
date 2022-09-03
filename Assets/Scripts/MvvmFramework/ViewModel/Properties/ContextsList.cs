using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MVVM.ViewModel
{
	public abstract class ContextsList :
		IBindableProperty<List<ViewModelContext>>,
		IInjectionConsumer,
		IViewModelContext,
		IEnumerable<ViewModelContext>
	{
		public event Action OnChange;


		protected readonly List<ViewModelContext> _list = new List<ViewModelContext>();
		protected Injector _injector;

		bool _isEnabled;


		#region Properties

		public int Count => _list.Count;

		public ViewModelContext this[int index] => _list[index];

		#endregion Properties


		public void Inject(Injector injector)
		{
			_injector = injector;
			foreach (var context in _list)
			{
				context.Inject(injector);
			}
		}


		public IEnumerator<ViewModelContext> GetEnumerator() => _list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
		
		public Dictionary<string, IBindableProperty> GetProperties() => null;
		
		public Dictionary<string, IViewModelContext> GetContexts() => null;
		
		public Dictionary<string, ContextsList> GetCollections() => null;
		
		public Dictionary<string, Action<object[]>> GetMethods() => null;
		
		public List<ViewModelContext> GetValue()
		{
			return _list;
		}

		public object GetBoxedValue()
		{
			return GetValue();
		}

		public bool TryGetProperty(string name, out IBindableProperty property)
		{
			property = null;
			return false;
		}

		public bool TryGetMethod(string name, out Action<object[]> action)
		{
			action = null;
			return false;
		}

		public bool TryGetContext(string name, out IViewModelContext context)
		{
			context = null;
			return false;
		}

		public bool TryGetCollection(string name, out ContextsList context)
		{
			context = null;
			return false;
		}

		public void Enable()
		{
			if (_isEnabled)
			{
				return;
			}

			_isEnabled = true;

			foreach (var context in _list)
			{
				context.Enable();
			}
		}

		public void Disable()
		{
			if (!_isEnabled)
			{
				return;
			}

			_isEnabled = false;

			foreach (var context in _list)
			{
				context.Disable();
			}
		}

		public void Destroy()
		{
			foreach (var context in _list)
			{
				context.Destroy();
			}
		}


		protected void NotifyChanged()
		{
			OnChange?.Invoke();
		}

	}

	public class ContextsList<TContext, T> :
		ContextsList,
		IEnumerable<TContext>,
		IBindableProperty<int>
		where TContext : SettableContext<T>, new() where T : class
	{
		readonly Stack<TContext> _cache = new Stack<TContext>();


		public new TContext this[int index] => _list[index] as TContext;


		public IEnumerator<TContext> GetEnumerator()
		{
			var enumerator = _list.GetEnumerator();
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					yield return (TContext)enumerator.Current;
				}
			}
		}

		public TContext GetContextAtIndex(int index)
		{
			return _list[index] as TContext;
		}

		public void Set(IEnumerable<T> data)
		{
			foreach (var ctx in _list)
			{
				ctx.Disable();
				ctx.Destroy();
				_cache.Push(ctx as TContext);
			}
			_list.Clear();

			var contexts = data.Select(item =>
			{
				var ctx = GetItemContext();
				ctx.SetData(item);
				return ctx;
			});
			_list.AddRange(contexts);
			NotifyChanged();
		}

		public void Add(T item)
		{
			var ctx = GetItemContext();
			ctx.SetData(item);
			_list.Add(ctx);
			NotifyChanged();
		}

		public void Insert(int index, T item)
		{
			var ctx = GetItemContext();
			ctx.SetData(item);
			_list.Insert(index, ctx);
			NotifyChanged();
		}

		public void SetAt(int index, T item)
		{
			var ctx = _list[index] as SettableContext<T>;
			ctx.SetData(item);
			NotifyChanged();
		}

		public void Clear()
		{
			foreach (var ctx in _list)
			{
				ctx.Disable();
				ctx.Destroy();
			}
			_list.Clear();
			NotifyChanged();
		}

		public bool Remove(TContext item)
		{
			item.Disable();
			item.Destroy();
			var success = _list.Remove(item);
			NotifyChanged();
			return success;
		}

		public bool Remove(T item)
		{
			var index = IndexOf(item);
			if (index > -1)
			{
				RemoveAt(index);
				return true;
			}

			return false;
		}

		public int IndexOf(T item)
		{
			var index = _list.FindIndex(x => ((TContext)x).Data.Equals(item));
			return index;
		}

		public void RemoveAt(int index)
		{
			var ctx = _list[index];
			ctx.Disable();
			ctx.Destroy();
			_list.RemoveAt(index);
			NotifyChanged();
		}

		public bool IsEmpty()
		{
			return _list.Count == 0;
		}


		TContext GetItemContext()
		{
			var ctx = _cache.Count > 0
				? _cache.Pop()
				: new TContext();

			ctx.Inject(_injector);
			ctx.Enable();

			return ctx;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			var enumerator = GetEnumerator();
			return enumerator;
		}

		int IBindableProperty<int>.GetValue()
		{
			return _list.Count;
		}
	}
}
