using Lucida.FlapStacks.Platform.x86_16.Ops;

namespace Lucida.FlapStacks.Platform.x86_16.Optimizers
{
	public class StoreToFixedAddress : Optimizer
	{
		public override int MinLength => 5;

		public override bool Apply(List<Op> ops, int index)
		{
			if (ops[index] is Imm16Op addr &&
				addr.Target == Register.BX &&
				ops[index + 1] is Imm8Op climm &&
				climm.Target == Register.CX &&
				ops[index + 2] is LshOp lsh &&
				addr.Target == lsh.Target &&
				ops[index + 3] is PopOp popValue &&
				ops[index + 4] is StoreOp store &&
				popValue.Target == store.Source)
			{
				ops[index] = new PopOp(Register.AX);
				ops[index + 1] = new StoreAddrAxOp(new LeftShiftedValue(addr.Value, climm.Value));
				ops.RemoveAt(index + 2);
				ops.RemoveAt(index + 2);
				ops.RemoveAt(index + 2);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
