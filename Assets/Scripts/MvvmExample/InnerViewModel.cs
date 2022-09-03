#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

using System.Collections.Generic;
using System.Collections;

using MVVM.ViewModel;

using ViewModel;

namespace MvvmExample
{
	public class InnerViewModel : ViewModelContext
	{
		readonly StringProperty InnerName = new StringProperty();
		readonly FloatProperty Time = new FloatProperty();
		readonly ContextsList<ExampleUserListItemContext, UserDto> Users = new ContextsList<ExampleUserListItemContext, UserDto>();

		ICoroutine _coroutine;
		
		#region Properties

		ILifecycle Lifecycle => Injector.Get<ILifecycle>();

		ICoroutineProvider Coroutines => Injector.Get<ICoroutineProvider>();

		#endregion Properties
		
		public InnerViewModel()
		{
			ChangeName();
		}
		
		#region Context callbacks

		protected override void Init()
		{
			var users = new List<UserDto>()
			{
				new UserDto { UserName = "Tom", Age = 24 },
				new UserDto { UserName = "Jack", Age = 10 },
				new UserDto { UserName = "William", Age = 40 },
				new UserDto { UserName = "Maria", Age = 14 },
				new UserDto { UserName = "Rob", Age = 12 },
			};
			Users.Set(users);

			_coroutine = Coroutines.StartCoroutine(LogDeltaTimeEverySecond());
		}

		protected override void OnEnable()
		{
			Log.Info("Enabled");
			Lifecycle.OnUpdate += Update;
		}

		protected override void OnDisable()
		{
			Log.Info("Disabled");
			Lifecycle.OnUpdate -= Update;
		}

		protected override void OnDestroy()
		{
			_coroutine.Stop();
			Log.Info("Destroyed");
		}

		#endregion Context callbacks
		
		void Update()
		{
			Time.Set(Lifecycle.Time);
		}

		[BindingAction]
		void AddRandomUser()
		{
#if UNITY_EDITOR
			var user = new UserDto()
			{
				UserName = GUID.Generate().ToString().Substring(0, 3),
				Age = Random.Range(1, 99)
			};
			Users.Add(user);
#endif
		}

		[BindingAction]
		void RemoveRandomUser()
		{
			if (Users.Count > 0)
			{
				var index = Random.Range(0, Users.Count);
				Log.Info("Removed item: " + index);
				Users.RemoveAt(index);
			}
		}
		
		[BindingAction]
		void ChangeName()
		{
#if UNITY_EDITOR
			InnerName.Set(GUID.Generate().ToString().Substring(0, 6));
#endif
		}

		[BindingAction]
		void LogNumber(int number)
		{
			Log.Info("Test number: " + number);
		}

		IEnumerator LogDeltaTimeEverySecond()
		{
			while (true)
			{
				Log.Info(Lifecycle.DeltaTime.ToString());
				yield return new WaitForSeconds(1f);
			}
		}
	}
}
