using Lucida.FlapStacks.Platform.x86_16.Ops;

namespace Lucida.FlapStacks.Platform.x86_16.Optimizers
{
	public class PushFollowedByReturn : Optimizer
	{
		public override int MinLength => 2;

		public override bool Apply(List<Op> ops, int index)
		{
			if (ops[index] is PushImmOp imm && ops[index + 1] is RetOp)
			{
				ops[index] = new Imm16Op(Register.AX, imm.Value);
				ops[index + 1] = new JumpOp(Register.AX);
				return true;
			}
			else if (ops[index] is PushOp push && ops[index + 1] is RetOp)
			{
				ops[index] = new JumpOp(push.Source);
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
