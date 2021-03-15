namespace Lucida.FlapStacks.Platform.URCL
{
	public class LateinitValue : Value
	{
		public ulong Value { get; set; }

		public long SignedValue
		{
			get => (long)Value;
			set => Value = (ulong)value;
		}

		public override ulong Get()
		{
			return Value;
		}
	}
}
