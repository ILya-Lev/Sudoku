using Sudoku.Model.Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model
{
	public class Field
	{
		private IReadOnlyList<Cell> _cells;
		public Row[] Rows { get; }
		public Column[] Columns { get; }
		public Square[] Squares { get; }

		public Field (List<int> field)
		{
			_cells = field.Select(n => new Cell { Value = n }).ToList();

			var rows = new List<Row>();
			var columns = new List<Column>();
			var squares = new List<Square>();
			for (int i = 0; i < Constants.LENGTH; i++)
			{
				rows.Add(new Row(_cells, i));
				columns.Add(new Column(_cells, i));
				squares.Add(new Square(_cells, i));
			}

			Rows = rows.ToArray();
			Columns = columns.ToArray();
			Squares = squares.ToArray();
		}

		public bool IsFilled () => Rows.All(r => r.IsFilled());

		public int FilledAmount () => Rows.Sum(r => r.FilledCount());

		public void Replicate () => RunActionOverAllStructures(s => s.ReplicateSelf());
		public void MatchPendingPair () => RunActionOverAllStructures(s => s.Match2PendingValues());

		private void RunActionOverAllStructures (Action<Structure> action)
		{
			Rows.ToList().ForEach(action);
			Columns.ToList().ForEach(action);
			Squares.ToList().ForEach(action);
		}

		public void MatchSinglePendingNumber ()
		{
			for (int i = 0; i < Constants.LENGTH; i++)
			{
				if (Rows[i].MatchSinglePendingNumber()) break;
				if (Columns[i].MatchSinglePendingNumber()) break;
				if (Squares[i].MatchSinglePendingNumber()) break;
			}
		}
	}
}
