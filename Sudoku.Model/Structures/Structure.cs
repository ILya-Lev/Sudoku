using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model.Structures
{
	public abstract class Structure
	{
		private static readonly List<int> _allPossibleValues = Enumerable.Range(1, Constants.LENGTH).ToList();

		public Cell[] Cells { get; } = new Cell[Constants.LENGTH];
		public int Index { get; set; }

		protected Structure (IReadOnlyList<Cell> field, int index)
		{
			if (field.Count != Constants.SIZE)
				throw new ArgumentException($"Initial Field should be of 81 value. While it is {field.Count}.");
			if (field.Any(c => c.Value > 9 || c.Value < 0))
				throw new ArgumentOutOfRangeException($"Initial Field should not contain values less than 0 (empty cells) and greater than 9.");
			Index = index;
		}

		public bool IsFilled () => Cells.All(c => c.IsEmpty == false);
		public int FilledCount () => Cells.Count(c => c.IsEmpty == false);

		public void ReplicateSelf () => Replicate(PendingValues());
		private List<int> PendingValues () => _allPossibleValues
														.Except(Cells.Select(c => c.Value).Distinct())
														.ToList();

		private void Replicate (List<int> pendingValues)
		{
			foreach (Cell cell in Cells.Where(c => c.IsEmpty))
			{
				if (cell.PossibleValues.Count == 0)
					cell.PossibleValues = pendingValues;
				else
					cell.PossibleValues = pendingValues.Intersect(cell.PossibleValues).ToList();
			}
		}

		public void Match2PendingValues ()
		{
			//todo: calculate frequency by means of LINQ

			var frequencePending = CellsByPossibleValues();

			var occureInTwo = frequencePending.Where(item => item.Value.Count == 2).ToList();
			for (int i = 0; i < occureInTwo.Count; i++)
			{
				var currentListCells = occureInTwo[i].Value.OrderBy(c => c.GetHashCode()).ToList();
				for (int j = i + 1; j < occureInTwo.Count; j++)
				{
					var testListCells = occureInTwo[j].Value.OrderBy(c => c.GetHashCode()).ToList();

					if (currentListCells == testListCells)
					{
						var newPossibleValues = new List<int> { occureInTwo[i].Key, occureInTwo[j].Key };
						occureInTwo[i].Value.ForEach(c => c.PossibleValues = newPossibleValues);
						occureInTwo[j].Value.ForEach(c => c.PossibleValues = newPossibleValues);
						break;
					}
				}
			}
		}

		private Dictionary<int, List<Cell>> CellsByPossibleValues ()
		{
			var frequencePending = new Dictionary<int, List<Cell>>();
			foreach (var cell in Cells.Where(c => c.IsEmpty))
			{
				foreach (var possibleValue in cell.PossibleValues)
				{
					if (!frequencePending.ContainsKey(possibleValue))
						frequencePending.Add(possibleValue, new List<Cell> { cell });
					else
						frequencePending[possibleValue].Add(cell);
				}
			}
			return frequencePending;
		}

		/// <summary>
		/// possible values of all field cells are not updated
		///  each time single possible value in a structure is found
		/// in order to avoid rules brake - insert only one value per method run over whole field
		/// supposed Field.Replicate() is called after this method and prior any other solving algorithms
		/// </summary>
		/// <returns>true if at least one cell could be filled as method work result</returns>
		public bool MatchSinglePendingNumber ()
		{
			var frequencePending = CellsByPossibleValues();
			var occuredOnce = frequencePending.Where(f => f.Value.Count == 1).ToList();
			if (occuredOnce.Any())
			{
				occuredOnce[0].Value.First().Value = occuredOnce[0].Key;
				return true;
			}
			return false;
		}
	}
}
