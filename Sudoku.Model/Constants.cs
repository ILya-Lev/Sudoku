namespace Sudoku.Model
{
	public static class Constants
	{
		public static int SIZE = 81;
		public static int LENGTH = 9;

		public enum CheckStatus
		{
			Ok,
			NotFilled,
			DuplicateInRow,
			DuplicateInColumn,
			DuplicateInSquare
		};
	}
}
