using System;
using System.Collections.Generic;

using MVVM.ViewModel;

namespace MVVM.DataBinding
{
	public static class BindingUtils
	{
		public static IContextContainer FindContainer(this IContextContainer container, int skippedContainersCount)
		{
			var currentContainer = container;
			for (var i = 0; i < skippedContainersCount; i++)
			{
				currentContainer = currentContainer.ParentContainer;
				if (currentContainer == null)
				{
					throw new NullReferenceException("No parent container found");
				}
			}

			return currentContainer;
		}

		public static IViewModelContext FindContext(this IViewModelContext currentContext, IEnumerable<BindingPath.Segment> segments)
		{
			foreach (var segment in segments)
			{
				if (currentContext == null)
				{
					return null;
				}

				if (segment.Type == BindingPath.SegmentType.Regular)
				{
					currentContext.TryGetContext(segment.PathPart, out currentContext);
				}
				else
				{
					var listSegment = segment as BindingPath.ListSegment;
					currentContext.TryGetCollection(segment.PathPart, out var collection);
					currentContext = collection[listSegment.Index];
				}
			}

			return currentContext;
		}
	}
}
