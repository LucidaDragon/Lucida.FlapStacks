using System;
using System.Collections.Generic;
using System.Text;

namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Cmp : Instruction
	{
		public override string Keyword => "cmp";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Cmp();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[0].Push(e);
			Operands[1].Push(e);

			var onBorrow = e.CreateLabel();
			var onNotBorrow = e.CreateLabel();
			var done = e.CreateLabel();

			e.Push(onBorrow);
			e.Push(onNotBorrow);
			e.SubtractBorrow();

			e.MarkLabel(onBorrow);
			SetCarry(e);
			e.Push(done);
			e.Goto();

			e.MarkLabel(onNotBorrow);
			ClearCarry(e);

			e.MarkLabel(done);

			StoreResult(e);
		}
	}
}
