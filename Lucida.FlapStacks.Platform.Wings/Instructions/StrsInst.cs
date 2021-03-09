namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class StrsInst : Instruction
	{
		public override string Keyword => "strs";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.StoreStack();
		}

		protected override Instruction CreateNew()
		{
			return new StrsInst();
		}
	}
}
