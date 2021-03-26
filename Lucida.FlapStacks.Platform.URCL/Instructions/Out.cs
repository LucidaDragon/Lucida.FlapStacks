using Lucida.FlapStacks.Platform.URCL.Operands;
using System;

namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Out : Instruction
	{
		public override string Keyword => "out";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Out();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			var next = e.CreateLabel();
			Operands[1].Push(e);
			e.Push(next);
			e.Push(next);
			e.WriteDevice(Operands[0].Value.Get());
			e.MarkLabel(next);
		}
	}
}
