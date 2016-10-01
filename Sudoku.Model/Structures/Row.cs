using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model.Structures
{
	public class Row : Structure
	{
		public Row (List<int> field, int index) : base(field, index)
		{
			var cells = field.Where((n, idx) => idx / Constants.LENGTH == Index)
						 .Select(n => new Cell { Value = n })
						 .ToArray();

			cells.CopyTo(Cells, 0);
		}
	}
}
