using Sudoku.DataLoader;
using Sudoku.Model;
using System;
using System.Linq;

namespace Sudoku
{
	class Program
	{
		static void Main (string[] args)
		{
			//SolveOneField();
			SolveManyFields();
		}

		private static void SolveOneField ()
		{
			var numbers = FileLoader.LoadOneFieldFromFile("field.txt");
			var field = new Field(numbers);

			field.Print();

			var sudokuSolver = new SudokuSolver();
			var stepsNumber = sudokuSolver.Solve(field);

			sudokuSolver.Field.Print();

			var checkResult = sudokuSolver.Field.Check();

			if (checkResult != Constants.CheckStatus.Ok)
				Console.WriteLine($"the field has not been solved in {stepsNumber} steps." +
								  $" Found problem {checkResult}");
			else
				Console.WriteLine($"the field has been solved in {stepsNumber} steps");
		}

		private static void SolveManyFields ()
		{
			long topLeftCornerSum = 0;
			int solvedCounter = 0;
			var grids = FileLoader.LoadManyFieldsFromFile("p096_sudoku.txt");
			foreach (var grid in grids)
			{
				var field = new Field(grid);
				field.Print();

				var sudokuSolver = new SudokuSolver();
				var stepsNumber = sudokuSolver.Solve(field);

				var checkResult = sudokuSolver.Field.Check();
				sudokuSolver.Field.Print();

				if (checkResult != Constants.CheckStatus.Ok)
				{
					Console.WriteLine($"the field has not been solved in {stepsNumber} steps." +
									  $" Found problem {checkResult}");

					FileLoader.SaveInFile(sudokuSolver.Field);
				}
				else
				{
					Console.WriteLine($"the field has been solved in {stepsNumber} steps");

					topLeftCornerSum += sudokuSolver.Field.Rows[0].Cells.Take(3)
										.Select((c, idx) => (int) Math.Pow(10, 2 - idx) * c.Value)
										.Sum();
					solvedCounter++;
				}
			}

			Console.WriteLine($"Solved {solvedCounter} of {grids.Count} fields.\n" +
							  $" Top left corner sum is {topLeftCornerSum}.");
		}
	}
}
