namespace Lucida.FlapStacks
{
	public abstract class MinimalLinearMemoryEmitter : LinearMemoryEmitter
	{
		public override void Core(Value toAddress)
		{
			Immediate(new Constant(0), toAddress);
		}

		public override void Cores(Value toAddress)
		{
			Immediate(new Constant(1), toAddress);
		}

		public override void Join(Value fromAddress)
		{
			var loop = CreateLabel();
			var skip = CreateLabel();
			Immediate(loop, E);
			Immediate(skip, F);
			MarkLabel(loop);
			BranchZeroIndirect(fromAddress, E, F);
			MarkLabel(skip);
		}

		public override void Lock(Value indirectAddress, Value successAddress)
		{
			Immediate(new Constant(1), successAddress);
		}

		public override void Start(Value fromAddress, Value indirectAddress)
		{
			var skip = CreateLabel();
			Immediate(skip, F);
			BranchZeroIndirect(fromAddress, indirectAddress, F);
			MarkLabel(skip);
		}

		public override void Stop(Value fromAddress)
		{
			var loop = CreateLabel();
			var skip = CreateLabel();
			Immediate(loop, E);
			Immediate(skip, F);
			MarkLabel(loop);
			BranchZeroIndirect(fromAddress, E, F);
			MarkLabel(skip);
		}

		public override void Unlock(Value indirectAddress) { }
	}
}
