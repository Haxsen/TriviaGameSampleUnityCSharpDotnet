using MVVM.ViewModel;

namespace ViewModel
{
	public struct Padding
	{
		public int Left;
		public int Right;
		public int Top;
		public int Bottom;

		public Padding(int left, int right, int top, int bottom)
		{
			Left = left;
			Right = right;
			Top = top;
			Bottom = bottom;
		}
	}

	public class PaddingProperty : SettableProperty<Padding>
	{
		public PaddingProperty(Padding value = default)
					: base(value)
		{
		}
	}
}
