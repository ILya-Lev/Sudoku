using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model.Structures
{
	public class Column : Structure
	{
		public Column (IReadOnlyList<Cell> field, int index) : base(field, index)
		{
			var cells = field.Where((c, idx) => idx % Constants.LENGTH == Index).ToArray();
			cells.CopyTo(Cells, 0);
		}
	}
}
