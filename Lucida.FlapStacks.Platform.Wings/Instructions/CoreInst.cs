namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class CoreInst : Instruction
	{
		public override string Keyword => "core";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Core();
		}

		protected override Instruction CreateNew()
		{
			return new CoreInst();
		}
	}
}
