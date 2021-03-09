namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class AddOp : Op
	{
		public override int GetSize(Emitter8086 emitter) => 2;

		public Register Target { get; }
		public Register Source { get; }
		public bool WithCarry { get; }

		public AddOp(Register target, Register source, bool withCarry = false)
		{
			Target = target;
			Source = source;
			WithCarry = withCarry;
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			stream.WriteByte((byte)(0x03 + (WithCarry ? 0x10 : 0)));
			stream.WriteByte((byte)(0xC0 + ((int)Target << 3) + (int)Source));
		}
	}
}
