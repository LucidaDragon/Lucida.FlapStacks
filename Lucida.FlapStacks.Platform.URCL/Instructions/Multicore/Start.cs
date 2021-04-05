namespace Lucida.FlapStacks.Platform.URCL.Instructions.Multicore
{
	public class Start : Instruction
	{
		public override string Keyword => "start";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Start();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[0].Push(e);
			Operands[1].Push(e);
			e.Start();
		}
	}
}
