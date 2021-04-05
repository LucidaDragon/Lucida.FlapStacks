using Lucida.FlapStacks.CodeDOM.Types;
using System;

namespace Lucida.FlapStacks.CodeDOM.Expressions
{
	public abstract class ValueExpression : Expression
	{
		public override Type ResultType => ValueType;

		public Type ValueType { get; set; } = new NativeIntType();
		public Value Value { get; set; }

		public override bool IsConstant => true;
		public override Value Constant => Value;

		public override void Emit(Emitter emitter)
		{
			if (ValueType.Size != 1) throw new Exception("Value type must have a size of 1.");

			emitter.Push(Value);
		}
	}
}
