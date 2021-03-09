namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class NopInst : Instruction
	{
		public override string Keyword => "nop";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter) { }

		protected override Instruction CreateNew()
		{
			return new NopInst();
		}
	}
}
