namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class BpsetInst : Instruction
	{
		public override string Keyword => "bpset";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.SetBasePointer();
		}

		protected override Instruction CreateNew()
		{
			return new BpsetInst();
		}
	}
}
