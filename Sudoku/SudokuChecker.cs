using MoreLinq;
using Sudoku.Model;
using System.Linq;
using static Sudoku.Model.Constants.CheckStatus;

namespace Sudoku
{
	public static class SudokuChecker
	{
		public static Constants.CheckStatus Check (this Field field)
		{
			if (!field.IsFilled())
				return NotFilled;

			if (field.Rows.Any(r => r.Cells.DistinctBy(c => c.Value).Count() != Constants.LENGTH))
				return DuplicateInRow;

			if (field.Columns.Any(col => col.Cells.DistinctBy(c => c.Value).Count() != Constants.LENGTH))
				return DuplicateInColumn;

			if (field.Squares.Any(s => s.Cells.DistinctBy(c => c.Value).Count() != Constants.LENGTH))
				return DuplicateInSquare;

			return Ok;
		}
	}
}
