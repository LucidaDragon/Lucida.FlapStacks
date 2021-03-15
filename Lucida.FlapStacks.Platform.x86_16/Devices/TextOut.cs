using Lucida.FlapStacks.Platform.x86_16.Ops;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lucida.FlapStacks.Platform.x86_16.Devices
{
	public class TextOut : Device<Emitter8086>
	{
		public override string Name => "text-out";

		public override void BeginUse(Emitter8086 e)
		{
			e.Emit(new Imm16Op(Register.AX, 3));
			e.Interrupt(0x10);
		}

		public override Device CreateNew()
		{
			return new TextOut();
		}

		public override void EndUse(Emitter8086 e) { }

		public override void Read(Emitter8086 e)
		{
			e.Pop(Register.AX);
			e.Pop(Register.BX);
			e.PushZero();
			e.Emit(new JumpOp(Register.AX));
		}

		public override void Write(Emitter8086 e)
		{
			e.Pop(Register.CX);
			e.Pop(Register.DX);
			e.Pop(Register.CX);
			e.Push(Register.DX);
			e.Emit(new LowOp(Register.CX));
			e.Emit(new Imm16Op(Register.AX, 0x0900));
			e.Emit(new OrOp(Register.AX, Register.CX));
			e.Emit(new Imm16Op(Register.BX, 0x07));
			e.Emit(new Imm8Op(Register.CX, 1));
			e.Interrupt(0x10);
			e.Emit(new Imm16Op(Register.AX, 0x0300));
			e.Emit(new XorOp(Register.BX, Register.BX));
			e.Interrupt(0x10);
			e.Emit(new Imm16Op(Register.AX, 1));
			e.Emit(new AddOp(Register.DX, Register.AX));
			e.Push(Register.DX);
			e.Emit(new MovOp(Register.AX, Register.DX));
			e.Emit(new LowOp(Register.AX));
			e.Emit(new XorOp(Register.DX, Register.DX));
			e.Emit(new Imm16Op(Register.BX, 80));
			e.Emit(new DivOp(Register.BX));
			e.Pop(Register.BX);
			e.Push(Register.DX);
			e.Emit(new XorOp(Register.DX, Register.DX));
			e.Emit(new Imm16Op(Register.CX, 8));
			e.Emit(new RshOp(Register.BX));
			e.Emit(new AddOp(Register.AX, Register.BX));
			e.Emit(new Imm16Op(Register.BX, 25));
			e.Emit(new DivOp(Register.BX));
			e.Emit(new LshOp(Register.DX));
			e.Pop(Register.BX);
			e.Emit(new OrOp(Register.DX, Register.BX));
			e.Emit(new XorOp(Register.BX, Register.BX));
			e.Emit(new Imm16Op(Register.AX, 0x0200));
			e.Interrupt(0x10);
			e.Goto();
		}
	}
}
