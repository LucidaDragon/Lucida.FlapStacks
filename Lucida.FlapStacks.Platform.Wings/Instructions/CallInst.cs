namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class CallInst : Instruction
	{
		public override string Keyword => "call";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Call();
		}

		protected override Instruction CreateNew()
		{
			return new CallInst();
		}
	}
}
