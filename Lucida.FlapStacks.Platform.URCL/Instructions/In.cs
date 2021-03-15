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
	}
}
