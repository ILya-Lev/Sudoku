using Sudoku.Model;
using System;

namespace Sudoku
{
	public static class SudokuSolver
	{
		public static bool Solve (this Field field)
		{
			if (field.IsFilled())
			{
				Console.WriteLine("The Field is already solved");
				return true;
			}

			int filledBeforeAction = 0, filledAfterAction = 0;
			do
			{
				filledBeforeAction = field.FilledAmount();
				field.Replicate();
				filledAfterAction = field.FilledAmount();
			} while (filledAfterAction > filledBeforeAction);

			return field.IsFilled();
		}
	}
}
