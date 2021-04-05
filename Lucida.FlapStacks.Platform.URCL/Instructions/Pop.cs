namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Pop : Instruction
	{
		public override string Keyword => "pop";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Pop();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[0].Pop(e);
			e.SetBasePointer();
		}
	}
}
