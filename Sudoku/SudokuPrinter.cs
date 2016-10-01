using Sudoku.Model;
using System;

namespace Sudoku
{
	public static class SudokuPrinter
	{
		public static void Print (this Field field)
		{
			for (int i = 0; i < field.Rows.Length; i++)
			{
				var row = field.Rows[i];
				for (int index = 0; index < row.Cells.Length; index++)
				{
					var cell = row.Cells[index];
					Console.Write($"{cell.Value} ");
					if (index % 3 == 2) Console.Write("| ");
				}
				Console.WriteLine();
				if (i % 3 == 2) Console.WriteLine();
			}
		}
	}
}
