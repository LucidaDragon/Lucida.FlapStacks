using System;

namespace Lucida.FlapStacks.CodeDOM.Expressions
{
	public abstract class LoadExpression : Expression
	{
		public override Type ResultType => Source.ResultType.PointerType;

		public Expression Source { get; set; }

		public override void Emit(Emitter emitter)
		{
			if (ResultType.Size != 1) throw new Exception("Source pointer type must have a size of 1.");

			Source.Emit(emitter);
			emitter.LoadHeap();
		}
	}
}
