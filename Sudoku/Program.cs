using Sudoku.DataLoader;
using Sudoku.Model;
using System;

namespace Sudoku
{
	class Program
	{
		static void Main (string[] args)
		{
			var numbers = FileLoader.LoadFromFile("field.txt");
			var field = new Field(numbers);

			field.Print();

			var stepsNumber = field.Solve();

			field.Print();

			var checkResult = field.Check();

			if (checkResult != Constants.CheckStatus.Ok)
				Console.WriteLine($"the field has not been solved in {stepsNumber} steps." +
								  $" Found problem {checkResult}");
			else
				Console.WriteLine($"the field has been solved in {stepsNumber} steps");
		}
	}
}
