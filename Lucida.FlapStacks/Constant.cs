namespace Lucida.FlapStacks
{
	public class Constant : Value
	{
		public ulong Value { get; }

		public Constant(long value)
		{
			Value = (ulong)value;
		}

		public Constant(ulong value)
		{
			Value = value;
		}

		public override ulong Get()
		{
			return Value;
		}
	}
}
