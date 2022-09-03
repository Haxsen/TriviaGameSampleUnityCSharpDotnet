using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MVVM.DataBinding;

using Sirenix.OdinInspector;

namespace MVVM.Bindings
{
	public class LabelTextBinding : StringBinding
	{
		[Header("Components")]
		[SerializeField, Required] TextMeshProUGUI _label;

		[Header("Initialization")]
		[SerializeField] bool _rebuildLayout;


		#region MonoCallbacks

		void Reset()
		{
			_label ??= GetComponent<TextMeshProUGUI>();
		}

		#endregion MonoCallbacks


		protected override void SetValue(string value)
		{
			_label.text = value;
			if (_rebuildLayout)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);
			}
		}
	}
}
