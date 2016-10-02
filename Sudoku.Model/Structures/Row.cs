using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model.Structures
{
	public class Row : Structure
	{
		public Row (IReadOnlyList<Cell> field, int index) : base(field, index)
		{
			var cells = field.Where((n, idx) => idx / Constants.LENGTH == Index).ToArray();

			cells.CopyTo(Cells, 0);
		}
	}
}
