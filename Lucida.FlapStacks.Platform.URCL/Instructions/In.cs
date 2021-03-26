namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class In : Instruction
	{
		public override string Keyword => "in";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new In();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			var next = e.CreateLabel();
			e.Push(next);
			e.Push(next);
			e.ReadDevice(Operands[1].Value.Get());
			e.MarkLabel(next);
			Operands[0].Pop(e);
		}
	}
}
