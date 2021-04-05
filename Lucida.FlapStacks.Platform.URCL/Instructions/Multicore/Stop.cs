namespace Lucida.FlapStacks.Platform.URCL.Instructions.Multicore
{
	public class Stop : Instruction
	{
		public override string Keyword => "stop";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Stop();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[0].Push(e);
			e.Stop();
		}
	}
}
