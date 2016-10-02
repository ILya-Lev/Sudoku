using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model.Structures
{
	public class Square : Structure
	{
		public Square (IReadOnlyList<Cell> field, int index) : base(field, index)
		{
			var startRow = Index / 3 * 3;
			var startColumn = Index % 3 * 3;
			var finishRow = startRow + 3;
			var finishColumn = startColumn + 3;

			Func<int, int, bool> isInRange = (r, c) => startRow <= r && r < finishRow
													   && startColumn <= c && c < finishColumn;

			var cells = field.Where((n, idx) => isInRange(idx / Constants.LENGTH, idx % Constants.LENGTH))
							.ToArray();

			cells.CopyTo(Cells, 0);
		}
	}
}
