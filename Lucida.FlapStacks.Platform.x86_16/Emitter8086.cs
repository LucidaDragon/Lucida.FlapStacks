using Lucida.FlapStacks.Platform.x86_16.Ops;
using Lucida.FlapStacks.Platform.x86_16.Optimizers;
using System;

namespace Lucida.FlapStacks.Platform.x86_16
{
	public class Emitter8086 : Emitter
	{
		public override string Name => "8086";

		public ulong StartOffset { get; set; } = 0;
		public ulong CurrentOffset
		{
			get
			{
				var result = 0UL;

				for (int i = 0; i < Operations.Count; i++)
				{
					result += (ulong)Operations[i].GetSize(this);
				}

				return result + StartOffset;
			}
		}
		public List<Op> Operations { get; } = new List<Op>();

		private readonly Optimizer[] Optimizations = new Optimizer[]
		{
			new PushFollowedByPop(),
			new PushFollowedByReturn(),
			new DuplicateWithPeek(),
			new LoadFromFixedAddress(),
			new StoreToFixedAddress(),
			new MoveToSelf(),
			new RedundantStackSave()
		};

		public override void Add()
		{
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new AddOp(Register.AX, Register.BX));
			Push(Register.AX);
		}

		public override void AddCarry()
		{
			Pop(Register.DX);
			Pop(Register.CX);
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new AddOp(Register.AX, Register.BX));
			Push(Register.AX);

			var jumpFalse = new JumpOp(Register.DX);
			Emit(new JumpConditionalOp(Condition.Carry, (sbyte)jumpFalse.GetSize(this)));
			Emit(jumpFalse);
			Emit(new JumpOp(Register.CX));
		}

		public override void AddWithCarry()
		{
			Pop(Register.DX);
			Pop(Register.CX);
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new CarryFlagOp(true));
			Emit(new AddOp(Register.AX, Register.BX, true));
			Push(Register.AX);

			var jumpFalse = new JumpOp(Register.DX);
			Emit(new JumpConditionalOp(Condition.Carry, (sbyte)jumpFalse.GetSize(this)));
			Emit(jumpFalse);
			Emit(new JumpOp(Register.CX));
		}

		public void AlignTo(ulong alignTo)
		{
			Emit(new AlignmentOp(alignTo));
		}

		public override void And()
		{
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new AndOp(Register.AX, Register.BX));
			Push(Register.AX);
		}

		public override void Bool()
		{
			Pop(Register.AX);
			Emit(new OrOp(Register.AX, Register.AX));

			var b0 = new PushImmOp(new Constant(0));

			var a0 = new PushImmOp(new Constant(1));
			var a1 = new JumpConditionalOp(Condition.NotCarry, (sbyte)b0.GetSize(this));

			Emit(new JumpConditionalOp(Condition.Zero, (sbyte)(a0.GetSize(this) + a1.GetSize(this))));
			Emit(a0);
			Emit(a1);
			Emit(b0);
		}

		public override void BranchEqual()
		{
			Branch(Condition.Equal);
		}

		public override void BranchSignedGreater()
		{
			Branch(Condition.Greater);
		}

		public override void BranchSignedLess()
		{
			Branch(Condition.Less);
		}

		public override void BranchUnsignedGreater()
		{
			Branch(Condition.Above);
		}

		public override void BranchUnsignedLess()
		{
			Branch(Condition.Below);
		}

		public override void BranchZero()
		{
			Pop(Register.DX);
			Pop(Register.CX);
			Pop(Register.AX);
			Emit(new OrOp(Register.AX, Register.AX));
			var jumpFalse = new JumpOp(Register.DX);
			Emit(new JumpConditionalOp(Condition.Zero, (sbyte)jumpFalse.GetSize(this)));
			Emit(jumpFalse);
			Emit(new JumpOp(Register.CX));
		}

		public override void Call()
		{
			Pop(Register.AX);
			Emit(new CallOp(Register.AX));
		}

		public override Value CreateLabel()
		{
			return new Label();
		}

		public override void Divide()
		{
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new XorOp(Register.DX, Register.DX));
			Emit(new DivOp(Register.BX));
			Emit(new LowOp(Register.AX));
			Push(Register.AX);
		}

		public override void Duplicate()
		{
			Pop(Register.AX);
			Push(Register.AX);
			Push(Register.AX);
		}

		public void Emit(Op op)
		{
			Operations.Add(op);
		}

		public override void Goto()
		{
			Emit(new RetOp());
		}

		public void Interrupt(byte value)
		{
			Emit(new InterruptOp(value));
		}

		public override void LeftShift()
		{
			Pop(Register.CX);
			Pop(Register.AX);
			Emit(new LshOp(Register.AX));
			Push(Register.AX);
		}

		public override void LoadHeap()
		{
			Pop(Register.BX);
			Emit(new Imm8Op(Register.CX, 1));
			Emit(new LshOp(Register.BX));
			Emit(new LoadOp(Register.BX));
			Push(Register.BX);
		}

		public override void LoadStack()
		{
			Pop(Register.AX);
			Emit(new Imm8Op(Register.CX, 1));
			Emit(new LshOp(Register.AX));
			Emit(new MovOp(Register.BX, Register.BP));
			Emit(new AddOp(Register.BX, Register.AX));
			Emit(new LoadStackOp(Register.AX));
			Push(Register.AX);
		}

		public ulong GetAddress(Op op)
		{
			var address = 0UL;

			for (int i = 0; i < Operations.Count; i++)
			{
				var current = Operations[i];

				if (current == op) return address + StartOffset;

				address += (ulong)current.GetSize(this);
			}

			throw new Exception("Could not find operation address.");
		}

		public override void MarkLabel(Value value)
		{
			if (value is Label label)
			{
				label.Mark(this);
			}
		}

		public override void MaxHeap()
		{
			Push(new Constant(0xFFFF));
		}

		public override void Multiply()
		{
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new MulOp(Register.BX));
			Push(Register.AX);
		}

		public override void Negate()
		{
			Pop(Register.AX);
			Emit(new NegOp(Register.AX));
			Push(Register.AX);
		}

		public override void Not()
		{
			Pop(Register.AX);
			Emit(new NotOp(Register.AX));
			Push(Register.AX);
		}

		public override void Or()
		{
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new OrOp(Register.AX, Register.BX));
			Push(Register.AX);
		}

		public override void Pop()
		{
			Pop(Register.AX);
		}

		public void Pop(Register reg)
		{
			Emit(new PopOp(reg));
		}

		public override void PopBasePointer()
		{
			Pop(Register.BP);
		}

		public override void Push(Value value)
		{
			Emit(new PushImmOp(value));
		}

		public void Push(Register reg)
		{
			Emit(new PushOp(reg));
		}

		public override void PushBasePointer()
		{
			Push(Register.BP);
		}

		public override void Remainder()
		{
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new XorOp(Register.DX, Register.DX));
			Emit(new XorOp(Register.CX, Register.CX));
			Emit(new DivOp(Register.BX));
			Emit(new Imm8Op(Register.CX, 8));
			Emit(new RshOp(Register.AX));
			Push(Register.AX);
		}

		public override void RightShift()
		{
			Pop(Register.CX);
			Pop(Register.AX);
			Emit(new RshOp(Register.AX));
			Push(Register.AX);
		}

		public override void Save(Stream stream)
		{
			for (int i = 0; i < Operations.Count; i++)
			{
				for (int j = 0; j < Optimizations.Length; j++)
				{
					var optimizer = Optimizations[j];

					if ((Operations.Count - i) >= optimizer.MinLength && optimizer.Apply(Operations, i))
					{
						i = 0;
						break;
					}
				}
			}

			for (int i = 0; i < Operations.Count; i++)
			{
				var op = Operations[i];

				if (op != null) op.Emit(this, stream);
			}
		}

		public override void SetBasePointer()
		{
			Emit(new MovOp(Register.BP, Register.SP));
		}

		public override void StoreHeap()
		{
			Pop(Register.BX);
			Emit(new Imm8Op(Register.CX, 1));
			Emit(new LshOp(Register.BX));
			Pop(Register.AX);
			Emit(new StoreOp(Register.AX));
		}

		public override void StoreStack()
		{
			Pop(Register.AX);
			Emit(new Imm8Op(Register.CX, 1));
			Emit(new LshOp(Register.AX));
			Emit(new MovOp(Register.BX, Register.BP));
			Emit(new AddOp(Register.BX, Register.AX));
			Pop(Register.AX);
			Emit(new StoreStackOp(Register.AX));
		}

		public override void Subtract()
		{
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new SubOp(Register.AX, Register.BX));
			Push(Register.AX);
		}

		public override void SubtractBorrow()
		{
			Pop(Register.DX);
			Pop(Register.CX);
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new SubOp(Register.AX, Register.BX));
			Push(Register.AX);

			var jumpFalse = new JumpOp(Register.DX);
			Emit(new JumpConditionalOp(Condition.Carry, (sbyte)jumpFalse.GetSize(this)));
			Emit(jumpFalse);
			Emit(new JumpOp(Register.CX));
		}

		public override void SubtractWithBorrow()
		{
			Pop(Register.DX);
			Pop(Register.CX);
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new CarryFlagOp(true));
			Emit(new SubOp(Register.AX, Register.BX, true));
			Push(Register.AX);

			var jumpFalse = new JumpOp(Register.DX);
			Emit(new JumpConditionalOp(Condition.Carry, (sbyte)jumpFalse.GetSize(this)));
			Emit(jumpFalse);
			Emit(new JumpOp(Register.CX));
		}

		public override void Swap()
		{
			Pop(Register.AX);
			Pop(Register.BX);
			Push(Register.AX);
			Push(Register.BX);
		}

		public override void WriteByte(byte value)
		{
			Emit(new DataByteOp(value));
		}

		public override void WriteWord(Value value)
		{
			Emit(new DataWordOp(value));
		}

		public override void Xor()
		{
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new XorOp(Register.AX, Register.BX));
			Push(Register.AX);
		}

		private void Branch(Condition condition)
		{
			Pop(Register.DX);
			Pop(Register.CX);
			Pop(Register.BX);
			Pop(Register.AX);
			Emit(new CompareOp(Register.AX, Register.BX));
			var jumpFalse = new JumpOp(Register.DX);
			Emit(new JumpConditionalOp(condition, (sbyte)jumpFalse.GetSize(this)));
			Emit(jumpFalse);
			Emit(new JumpOp(Register.CX));
		}
	}
}
