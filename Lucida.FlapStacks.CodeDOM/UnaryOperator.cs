namespace Lucida.FlapStacks.CodeDOM
{
	public abstract class UnaryOperator : Operator
	{
		public abstract Type SourceType { get; }

		public abstract Value Compute(Value value);
	}
}
