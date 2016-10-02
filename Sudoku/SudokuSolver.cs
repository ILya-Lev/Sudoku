using Sudoku.Model;

namespace Sudoku
{
	public static class SudokuSolver
	{
		public static int Solve (this Field field)
		{
			// to avoid infinite loop and keep the logic initially
			// after action is 1 > than before action, which is 0 
			int filledBeforeAction = 0, filledAfterAction = 1;
			int steps = 0;
			while (filledAfterAction > filledBeforeAction && !field.IsFilled())
			{
				steps++;
				filledBeforeAction = field.FilledAmount();
				field.Replicate();
				filledAfterAction = field.FilledAmount();
				field.Print();
			}

			return steps;
		}
	}
}
