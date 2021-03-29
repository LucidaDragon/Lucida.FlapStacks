namespace Lucida.FlapStacks.CodeDOM
{
	public abstract class BinaryOperator : Operator
	{
		public abstract Type AType { get; }
		public abstract Type BType { get; }

		public abstract Value Compute(Value a, Value b);
	}
}
