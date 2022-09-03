using UnityEngine;

using System.Collections.Generic;

using MVVM.DataBinding;

using Sirenix.OdinInspector;

namespace MVVM.Bindings
{
	public class PrefabSelectionBinding : IntBinding
	{
		[SerializeField] List<GameObject> _prefabs;
		// if false destroys past instance even if template prefab the same
		[SerializeField] bool _keepIfSameTemplate = true;

		[ShowInInspector, ReadOnly] GameObject _instance;
		[ShowInInspector, ReadOnly] GameObject _lastPrefab;


		protected override void SetValue(int value)
		{
			var template = _prefabs[value];
			if (!template)
			{
				return;
			}

			if (_lastPrefab == template && !_keepIfSameTemplate)
			{
				return;
			}

			Destroy(_instance);
			_instance = Instantiate(template);

			var container = _instance.GetComponent<BaseContextContainer>();
			if (container)
			{
				container.SetParentContainer(Container);
			}
		}
	}
}
