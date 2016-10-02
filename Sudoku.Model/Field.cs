using Sudoku.Model.Structures;
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

		public void Replicate ()
		{
			for (int i = 0; i < Constants.LENGTH; i++)
			{
				Rows[i].ReplicateSelf();
				Columns[i].ReplicateSelf();
				Squares[i].ReplicateSelf();
			}
		}
	}
}
