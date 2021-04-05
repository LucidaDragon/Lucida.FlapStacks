using System;

namespace Lucida.FlapStacks.CodeDOM.Expressions
{
	public abstract class StoreExpression : Expression
	{
		public override Type ResultType => Source.ResultType;

		public Expression Source { get; set; }
		public Expression Target { get; set; }

		public override void Emit(Emitter emitter)
		{
			if (Target.ResultType.PointerType.Size != 1) throw new Exception("Target pointer type must have a size of 1.");
			if (ResultType.Size != 1) throw new Exception("Source result type must have a size of 1.");
			if (Source.ResultType != Target.ResultType.PointerType) throw new Exception("Source result type must match target pointer type.");

			Source.Emit(emitter);
			emitter.Duplicate();
			Target.Emit(emitter);
			emitter.StoreHeap();
		}
	}
}
