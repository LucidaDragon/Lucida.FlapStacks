using Lucida.FlapStacks.Platform.x86_16.Ops;

namespace Lucida.FlapStacks.Platform.x86_16.Optimizers
{
	public class PushFollowedByPop : Optimizer
	{
		public override int MinLength => 2;

		public override bool Apply(List<Op> ops, int index)
		{
			if (ops[index] is PushImmOp imm && ops[index + 1] is PopOp immPop)
			{
				ops[index] = new Imm16Op(immPop.Target, imm.Value);
				ops.RemoveAt(index + 1);
				return true;
			}
			else if (ops[index] is PushOp push && ops[index + 1] is PopOp pop)
			{
				ops[index] = new MovOp(pop.Target, push.Source);
				ops.RemoveAt(index + 1);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
