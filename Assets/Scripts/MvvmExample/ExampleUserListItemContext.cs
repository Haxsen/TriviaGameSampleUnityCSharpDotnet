using MVVM.ViewModel;

namespace MvvmExample
{
	public class UserDto
	{
		public string UserName;
		public int Age;
	}

	public class ExampleUserListItemContext : SettableContext<UserDto>
	{
		readonly StringProperty Name = new StringProperty();
		readonly IntProperty Age = new IntProperty();


		protected override void Set(UserDto data)
		{
			Name.Set(data.UserName);
			Age.Set(data.Age);
		}

		void IncrementAge()
		{
			Age.Set(Age.GetValue() + 1);
		}
	}
}
