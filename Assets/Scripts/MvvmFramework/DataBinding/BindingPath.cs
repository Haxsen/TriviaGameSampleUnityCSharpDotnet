using System;
using System.Collections.Generic;

namespace MVVM.DataBinding
{
	public class BindingPath
	{
		public enum SegmentType
		{
			Regular,
			Index
		}


		public abstract class Segment
		{
			public readonly string PathPart;


			public abstract SegmentType Type { get; }


			public Segment(string pathPart)
			{
				PathPart = pathPart;
			}
		}


		public class RegularSegment : Segment
		{
			public override SegmentType Type => SegmentType.Regular;


			public RegularSegment(string pathPart) : base(pathPart)
			{
			}
		}


		public class ListSegment : Segment
		{
			public readonly int Index;


			public override SegmentType Type => SegmentType.Index;


			public ListSegment(string pathPart, int index) : base(pathPart)
			{
				Index = index;
			}
		}


		const string SKIP_CONTAINER_STRING = "../";

		static readonly string[] SKIP_CONTAINER_SEPARATORS = { SKIP_CONTAINER_STRING };


		readonly string _path;
		int _skippedContainers;
		List<Segment> _segments;


		#region Properties

		public IList<Segment> Segments => _segments;

		public int SkippedContainers => _skippedContainers;

		#endregion Properties


		public BindingPath(string path)
		{
			_path = path;
			Parse();
		}


		BindingPath(string path, List<Segment> segments, int skippedContainers)
		{
			_path = path;
			_segments = segments;
			_skippedContainers = skippedContainers;
		}


		// TODO: add binding path range class to hold sliced segments list without cloning existing bindingPath instance
		public BindingPath WithoutLastSegment()
		{
			var segments = _segments.GetRange(0, _segments.Count - 1);
			var clone = new BindingPath(_path, segments, _skippedContainers);
			return clone;
		}

		void Parse()
		{
			var parts = _path.Split(SKIP_CONTAINER_SEPARATORS, StringSplitOptions.None);
			_skippedContainers = parts.Length - 1;
			var inContextPathPart = parts[_skippedContainers];
			_segments = GetSegments(inContextPathPart);
		}

		List<Segment> GetSegments(string path)
		{
			var segments = new List<Segment>();
			var parts = path.Split('.');
			foreach (var part in parts)
			{
				var bracketsOpenIndex = part.IndexOf('[');
				var hasIndex = bracketsOpenIndex > -1;
				var segment = hasIndex
					? GetIndexedSegment(part, bracketsOpenIndex)
					: new RegularSegment(part);

				segments.Add(segment);
			}

			return segments;
		}

		Segment GetIndexedSegment(string part, int bracketsOpenIndex)
		{
			var bracketsClosingIndex = part.IndexOf(']');
			var indexPartLength = bracketsClosingIndex - bracketsOpenIndex - 1;
			var indexSubstring = part.Substring(bracketsOpenIndex + 1, indexPartLength);
			var index = int.Parse(indexSubstring);

			var segment = new ListSegment(part.Substring(0, bracketsOpenIndex), index);
			return segment;
		}
	}
}
