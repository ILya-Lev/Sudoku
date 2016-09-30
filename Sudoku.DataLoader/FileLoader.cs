using Sudoku.Model;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Sudoku.DataLoader
{
	public class FileLoader
	{
		private List<int> LoadFromFile (string fileName)
		{
			var allText = File.ReadAllText(fileName, Encoding.ASCII);

			var numbers = allText.ToCharArray()
								.Select(TryParseChar)
								.Where(n => n != null)
								.Select(n => n.Value)
								.TakeWhile((n, i) => i < Constants.SIZE);
			return numbers.ToList();
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
