namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Not : Instruction
	{
		public override string Keyword => "not";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Not();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Not();
			SaveResult(e);
		}
	}
}
