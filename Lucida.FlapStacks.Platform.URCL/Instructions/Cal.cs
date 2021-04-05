namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Cal : Instruction
	{
		public override string Keyword => "cal";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Cal();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[0].Push(e);
			e.SetBasePointer();
			e.Call();
			e.SetBasePointer();
		}
	}
}
