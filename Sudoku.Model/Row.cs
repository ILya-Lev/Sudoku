using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model
{
	public class Row : Structure
	{
		public Row (List<int> field, int index) : base(field, index)
		{
			Cells = field.Where((n, idx) => idx / Constants.LENGTH == Index)
						 .Select((n, idx) => new Cell { Column = idx, Row = Index, Square = , Value = n })
						 .ToArray();
		}
	}
}
