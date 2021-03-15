using Lucida.FlapStacks.Platform.x86_16.Ops;

namespace Lucida.FlapStacks.Platform.x86_16.Devices
{
	public class TextIn : Device<Emitter8086>
	{
		public override string Name => "text-in";

		public override void BeginUse(Emitter8086 e) { }

		public override Device CreateNew()
		{
			return new TextIn();
		}

		public override void EndUse(Emitter8086 e) { }

		public override void Read(Emitter8086 e)
		{
			e.Emit(new XorOp(Register.AX, Register.AX));
			e.Interrupt(0x16);
			e.Emit(new Imm16Op(Register.BX, 0x00FF));
			e.Emit(new AndOp(Register.AX, Register.BX));
			e.Pop(Register.BX);
			e.Pop(Register.BX);
			e.Push(Register.AX);
			e.Emit(new JumpOp(Register.BX));
		}

		public override void Write(Emitter8086 e)
		{
			e.Pop(Register.AX);
			e.Pop(Register.BX);
			e.Pop(Register.BX);
			e.Emit(new JumpOp(Register.AX));
		}
	}
}
