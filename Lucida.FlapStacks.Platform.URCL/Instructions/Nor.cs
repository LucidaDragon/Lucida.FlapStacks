namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Nor : Instruction
	{
		public override string Keyword => "nor";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Nor();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Or();
			e.Not();
			SaveResult(e);
		}
	}
}
