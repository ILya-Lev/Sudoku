using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Model
{
	public class Cell
	{
		private int _value;

		//#region location
		//public int Row { get; set; }
		//public int Column { get; set; }
		//public int Square { get; set; }
		//#endregion location

		public int Value
		{
			get
			{
				if (PossibleValues.Count == 1) _value = PossibleValues.First();
				return _value;
			}
			set { _value = value; }
		}

		public bool IsEmpty => Value == 0;
		public List<int> PossibleValues { get; set; } = new List<int>();

		public override string ToString ()
		{
			return Value.ToString();
		}
	}
}
