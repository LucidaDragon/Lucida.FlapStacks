using Lucida.FlapStacks.Platform.x86_16.Ops;

namespace Lucida.FlapStacks.Platform.x86_16.Optimizers
{
	public class DuplicateWithPeek : Optimizer
	{
		public override int MinLength => 3;

		public override bool Apply(List<Op> ops, int index)
		{
			if (ops[index] is PopOp pop && ops[index + 1] is PushOp pushA && ops[index + 2] is PushOp pushB && pop.Target == pushA.Source && pushA.Source == pushB.Source)
			{
				ops[index] = new MovOp(Register.BX, Register.SP);
				ops[index + 1] = new LoadOp(Register.BX);
				ops[index + 2] = new PushOp(Register.BX);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
