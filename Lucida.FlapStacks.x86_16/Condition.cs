namespace Lucida.FlapStacks.Platform.x86_16
{
	public enum Condition
	{
		Overflow = 0,

		NotOverflow = 1,

		Carry = 2,
		Below = 2,
		NotAboveEqual = 2,

		NotCarry = 3,
		NotBelow = 3,
		AboveEqual = 3,

		Zero = 4,
		Equal = 4,

		NotZero = 5,
		NotEqual = 5,

		NotAbove = 6,
		BelowEqual = 6,

		Above = 7,
		NotBelowEqual = 7,

		Signed = 8,

		NotSigned = 9,

		ParityEven = 10,

		ParityOdd = 11,

		Less = 12,
		NotGreaterEqual = 12,

		NotLess = 13,
		GreaterEqual = 13,

		NotGreater = 14,
		LessEqual = 14,

		Greater = 15,
		NotLessEqual = 15
	}
}
