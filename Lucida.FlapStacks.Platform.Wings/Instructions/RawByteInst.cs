namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class RawByteInst : Instruction
	{
		public override string Keyword => "db";

		protected override int ArgumentCount => 1;

		public RawByteInst() { }

		public RawByteInst(byte value)
		{
			Arguments[0] = new Constant(value);
		}

		public override void Emit(Emitter emitter)
		{
			emitter.WriteByte((byte)Arguments[0].Get());
		}

		protected override Instruction CreateNew()
		{
			return new RawByteInst();
		}
	}
}
