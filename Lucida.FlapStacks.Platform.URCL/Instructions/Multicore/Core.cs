namespace Lucida.FlapStacks.Platform.URCL.Instructions.Multicore
{
	public class Core : Instruction
	{
		public override string Keyword => "core";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Core();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			e.Core();
			Operands[0].Pop(e);
		}
	}
}
