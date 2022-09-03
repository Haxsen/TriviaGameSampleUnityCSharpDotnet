using MVVM.ViewModel;

namespace MvvmExample
{
	public class TestViewModel : ViewModelContext
	{
		readonly BooleanProperty ShowButton = new BooleanProperty(true);
		readonly StringProperty Title = new StringProperty("Nothing");
		readonly StringProperty Name = new StringProperty();
		readonly IntProperty Count = new IntProperty();

		InnerViewModel Inner = new InnerViewModel();
		
		public TestViewModel()
		{
			Name.Set("Awesome exercise name");
		}
		
		[BindingAction]
		void Increment()
		{
			Count.Set(Count.GetValue() + 1);
			Title.Set("Something changed");
			ShowButton.Set(Count.GetValue() < 10);
		}
	}
}
