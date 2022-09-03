using UnityEngine;
using UnityEngine.UI;

using MVVM.DataBinding;

using Sirenix.OdinInspector;

[RequireComponent(typeof(Graphic))]
public class GraphicColorBinding : IntBinding
{
	[SerializeField] Color[] _colors;

	[ShowInInspector, ReadOnly] Graphic _target;


	#region MonoCallbacks

	void Awake()
	{
		_target = GetComponent<Graphic>();
	}

	#endregion MonoCallbacks


	protected override void SetValue(int index)
	{
		if (index >= 0 && index < _colors.Length)
		{
			_target.color = _colors[index];
		}
		else
		{
			Debug.LogError($"{nameof(GraphicColorBinding)}.{nameof(SetValue)} Index ({index}) out of color list range (0 - {_colors.Length - 1})", this);
		}
	}
}
