namespace Lucida.FlapStacks.Platform.URCL.Instructions.Multicore
{
	public class Cores : Instruction
	{
		public override string Keyword => "cores";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Cores();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			e.Cores();
			Operands[0].Pop(e);
		}
	}
}
