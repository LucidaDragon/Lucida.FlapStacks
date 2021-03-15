using Lucida.FlapStacks.Platform.x86_16.Ops;

namespace Lucida.FlapStacks.Platform.x86_16.Devices
{
	public class BootloaderDev : Device<Emitter8086>
	{
		public override string Name => "boot";

		private Value LoadLocation;
		private Value EndLocation;

		public override void BeginUse(Emitter8086 e)
		{
			LoadLocation = e.CreateLabel();
			EndLocation = e.CreateLabel();

			e.StartOffset = 0x7C00;

			e.Emit(new Imm16Op(Register.AX, EndLocation));
			e.Emit(new Imm16Op(Register.BX, LoadLocation));
			e.Emit(new SubOp(Register.AX, Register.BX));
			e.Emit(new Imm16Op(Register.BX, 0x0200));
			e.Emit(new AddOp(Register.AX, Register.BX));
			e.Emit(new Imm16Op(Register.CX, 0x0002));
			e.Emit(new LowOp(Register.DX));
			e.Emit(new Imm16Op(Register.BX, LoadLocation));
			e.Interrupt(0x13);
			e.Push(LoadLocation);
			e.Goto();

			e.AlignTo(0x7DFE);

			e.WriteByte(0x55);
			e.WriteByte(0xAA);

			e.MarkLabel(LoadLocation);
		}

		public override void EndUse(Emitter8086 e)
		{
			e.MarkLabel(EndLocation);
		}

		public override void Read(Emitter8086 e)
		{
			e.Pop();
			e.PushZero();
			e.Swap();
			e.Goto();
		}

		public override void Write(Emitter8086 e)
		{
			e.Pop();
			e.Swap();
			e.Pop();
			e.Goto();
		}

		public override Device CreateNew()
		{
			return new BootloaderDev();
		}
	}
}
