namespace Lucida.FlapStacks.Platform.x86_16.Optimizers
{
	public class LeftShiftedValue : Value
	{
		public Value Value { get; }
		public byte Times { get; }

		public LeftShiftedValue(Value value, byte times)
		{
			Value = value;
			Times = times;
		}

		public override ulong Get()
		{
			return Value.Get() << Times;
		}
	}
}
