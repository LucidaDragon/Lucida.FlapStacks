namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Mod : Instruction
	{
		public override string Keyword => "mod";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Mod();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Remainder();
			SaveResult(e);
		}
	}
}
