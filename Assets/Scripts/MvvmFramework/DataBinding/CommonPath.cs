using UnityEngine;

namespace MVVM.DataBinding
{
	public class CommonPath : BaseContextContainer
	{
		[SerializeField] string _path;


		public override void SetParentContainer(IContextContainer parentContainer)
		{
			base.SetParentContainer(parentContainer);
			UpdateContextByPath();
			UpdateContextsRecursively(transform);
		}


		void UpdateContextByPath()
		{
			var bindingPath = new BindingPath(_path);
			var skippedContainersCount = bindingPath.SkippedContainers;
			var currentContainer = ParentContainer.FindContainer(skippedContainersCount);
			if (currentContainer == null)
			{
				Debug.LogError($"{nameof(CommonPath)}.{nameof(UpdateContextByPath)}. No container found. Skipped count: {skippedContainersCount}");
				return;
			}

			var context = currentContainer.Context.FindContext(bindingPath.Segments);
			SetContext(context);
		}
	}
}
