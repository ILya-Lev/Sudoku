using Sudoku.Model;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Sudoku.DataLoader
{
	public static class FileLoader
	{
		public static List<int> LoadOneFieldFromFile (string fileName)
		{
			var allText = File.ReadAllText(fileName, Encoding.ASCII);

			var numbers = allText.ToCharArray()
								.Select(TryParseChar)
								.Where(n => n != null)
								.Select(n => n.Value)
								.TakeWhile((n, i) => i < Constants.SIZE);
			return numbers.ToList();
		}

		public static List<List<int>> LoadManyFieldsFromFile (string fileName)
		{
			var grids = new List<List<int>>();
			var numbers = new List<int>();
			foreach (var line in File.ReadLines(fileName, Encoding.ASCII))
			{
				if (line.Contains("Grid")) continue;

				numbers.AddRange(line.ToCharArray()
								.Select(TryParseChar)
								.Where(n => n != null)
								.Select(n => n.Value));

				for (int i = 0; i < numbers.Count / Constants.SIZE; i++)
				{
					grids.Add(numbers.Skip(i * Constants.SIZE).Take(Constants.SIZE).ToList());
				}

				numbers = numbers.Skip(numbers.Count / Constants.SIZE * Constants.SIZE).ToList();
			}

			return grids;
		}

		public static void SaveInFile (Field field)
		{
			var uniqueName = $"unresolved.txt";
			for (int i = 0; File.Exists(uniqueName); i++)
				uniqueName = $"unresolved_{i}.txt";

			var rows = field.Rows.Select(r => r.Cells.Select(c => c.Value.ToString())
												.Aggregate((total, item) => total + item)
										);
			File.WriteAllLines(uniqueName, rows);

		}

		private static int? TryParseChar (char c)
		{
			int value;
			if (int.TryParse(new string(c, 1), NumberStyles.Any, CultureInfo.InvariantCulture, out value))
				return value;
			return null;
		}
	}
}
