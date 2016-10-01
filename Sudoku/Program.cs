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

			var isSolved = field.Solve();

			field.Print();

			Console.WriteLine($"the field is solved? {isSolved} ");

		}
	}
}
