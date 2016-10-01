using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model.Structures
{
	public abstract class Structure
	{
		public Cell[] Cells { get; } = new Cell[Constants.LENGTH];
		public int Index { get; set; }

		protected Structure (List<int> field, int index)
		{
			if (field.Count != Constants.SIZE)
				throw new ArgumentException($"Initial Field should be of 81 value. While it is {field.Count}.");
			if (field.Any(x => x > 9 || x < 0))
				throw new ArgumentOutOfRangeException($"Initial Field should not contain values less than 0 (empty cells) and greater than 9.");
			Index = index;
		}

		public List<int> PendingValues () => Enumerable.Range(1, Constants.LENGTH)
														.Except(Cells.Select(c => c.Value).Distinct())
														.ToList();

		public void ReplicateSelf () => Replicate(PendingValues());

		public void Replicate (List<int> pendingValues)
		{
			foreach (Cell cell in Cells.Where(c => c.IsEmpty))
			{
				//if (cell.PossibleValues.Count == 0)
				//	cell.PossibleValues = pendingValues;
				//else
				cell.PossibleValues = pendingValues.Union(cell.PossibleValues).ToList();
			}
		}

		public bool IsFilled () => Cells.All(c => c.IsEmpty == false);
		public int FilledCount () => Cells.Count(c => c.IsEmpty == false);
	}
}
