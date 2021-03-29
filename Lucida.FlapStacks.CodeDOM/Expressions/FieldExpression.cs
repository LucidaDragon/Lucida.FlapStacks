using Lucida.FlapStacks.CodeDOM.Types;
using System;

namespace Lucida.FlapStacks.CodeDOM.Expressions
{
	public abstract class FieldExpression : Expression
	{
		public override Type ResultType => new NativePointerType(Source.ResultType.PointerType.Fields[Index.Get()]);

		public Expression Source { get; set; }
		public Value Index { get; set; }

		public override void Emit(Emitter emitter)
		{
			if (Source.ResultType.Size != 1) throw new Exception("Source result type must have a size of 1.");
			if (Source.ResultType.PointerType.Size == 0) throw new Exception("Source pointer type must have a size greather than 0.");
			if (Source.ResultType.PointerType.Fields.Length == 0) throw new Exception("Source pointer type must have 1 or more fields.");

			var index = Index.Get();
			var offset = 0UL;
			for (ulong i = 0; i < index; i++)
			{
				offset += Source.ResultType.PointerType.Fields[i].Size;
			}

			Source.Emit(emitter);
			emitter.Push(new Constant(offset));
			emitter.Add();
		}
	}
}
