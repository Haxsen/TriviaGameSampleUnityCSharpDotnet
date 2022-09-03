using UnityEngine;
using UnityEngine.UI;

using MVVM.DataBinding;
using ViewModel;

namespace Bindings
{
	public class PaddingBinding : SinglePropertyBinding
	{
		[SerializeField] LayoutGroup _layoutGroup;


		#region MonoCallbacks

		void Reset()
		{
			_layoutGroup ??= GetComponent<LayoutGroup>();
		}

		#endregion


		protected override void ProcessChange()
		{
			if (Property is PaddingProperty paddingProp)
			{
				var padding = paddingProp.GetValue();
				_layoutGroup.padding = new RectOffset(padding.Left, padding.Right, padding.Top, padding.Bottom);
			}
		}
	}
}
