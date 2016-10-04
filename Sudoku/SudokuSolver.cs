using Sudoku.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
	public class SudokuSolver
	{
		private int _filledBeforeAction;
		private int _filledAfterAction;
		private int _steps;

		public Field Field { get; private set; }

		public int Solve (Field field)
		{
			_steps = 0;

			Field = new Field(FieldsNumbers(field));
			var copyField = new Field(FieldsNumbers(field));
			while (!copyField.IsFilled())
			{
				var filledPerIteration = FillInCellsLogically(copyField);

				if (filledPerIteration == null)            //the grid is completely filled in
					break;

				if (filledPerIteration != 0)               //something changed per iteration - store it
					Field = new Field(FieldsNumbers(copyField));

				var targetCells = copyField.Rows.SelectMany(r => r.Cells.Where(c => c.IsEmpty).Select((c, idx) => new
				{
					RowIdx = r.Index,
					ColIdx = idx,
					Cell = c,
					PossibleValues = c.PossibleValues
				}))
				.OrderBy(item => item.PossibleValues.Count);

				bool solved = false;
				foreach (var targetCell in targetCells)
				{
					foreach (var possibleValue in targetCell.PossibleValues)
					{
						copyField.Rows[targetCell.RowIdx]
							.Cells.Where(c => c.IsEmpty)
							.ToList()[targetCell.ColIdx].Value = possibleValue;

						if (FillInCellsLogically(copyField) == null)
						{
							solved = copyField.Check() == Constants.CheckStatus.Ok;
							if (solved) break;
						}

						copyField = new Field(FieldsNumbers(field)); //of copy Field (prop of the class)
					}
					if (solved) break;
				}

				if (solved) break;
			}
			Field = new Field(FieldsNumbers(copyField));

			return _steps;
		}

		private static List<int> FieldsNumbers (Field field)
			=> field.Rows.SelectMany(r => r.Cells.Select(c => c.Value)).ToList();

		private int? FillInCellsLogically (Field copyField)
		{
			// to avoid infinite loop and keep the logic initially
			// after action is 1 > than before action, which is 0 
			_filledBeforeAction = 0;
			_filledAfterAction = 1;
			int filledInitially = copyField.FilledAmount();
			while (_filledAfterAction > _filledBeforeAction && !copyField.IsFilled())
			{
				_steps++;

				_filledBeforeAction = copyField.FilledAmount();
				copyField.Replicate();
				_filledAfterAction = copyField.FilledAmount();

				if (_filledBeforeAction == _filledAfterAction)
				{
					copyField.MatchSinglePendingNumber();
					_filledAfterAction = copyField.FilledAmount();
				}

				//copyField.Print();
			}
			return _filledAfterAction > _filledBeforeAction
				? default(int?)
				: _filledAfterAction - filledInitially;
		}
	}
}
