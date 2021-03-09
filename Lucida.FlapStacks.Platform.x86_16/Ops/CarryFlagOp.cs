namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class CarryFlagOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 1;

		public bool Value { get; }

		public CarryFlagOp(bool value)
		{
			Value = value;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			if (Value)
			{
				stream.WriteByte(0xF9);
			}
			else
			{
				stream.WriteByte(0xF8);
			}
		}
	}
}
