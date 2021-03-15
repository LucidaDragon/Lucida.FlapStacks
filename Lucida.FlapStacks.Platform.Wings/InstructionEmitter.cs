using Lucida.FlapStacks.Platform.Wings.Instructions;

namespace Lucida.FlapStacks.Platform.Wings
{
	public abstract class InstructionEmitter : Emitter
	{
		protected readonly List<Instruction> Instructions = new List<Instruction>();

		public override void Add()
		{
			Emit(new AddInst());
		}

		public override void AddCarry()
		{
			Emit(new AddcInst());
		}

		public override void AddWithCarry()
		{
			Emit(new AdcInst());
		}

		public override void And()
		{
			Emit(new AndInst());
		}

		public override void Bool()
		{
			Emit(new BoolInst());
		}

		public override void BranchEqual()
		{
			Emit(new IfeInst());
		}

		public override void BranchSignedGreater()
		{
			Emit(new IfsgInst());
		}

		public override void BranchSignedLess()
		{
			Emit(new IfslInst());
		}

		public override void BranchUnsignedGreater()
		{
			Emit(new IfugInst());
		}

		public override void BranchUnsignedLess()
		{
			Emit(new IfulInst());
		}

		public override void BranchZero()
		{
			Emit(new IfzInst());
		}

		public override void Call()
		{
			Emit(new CallInst());
		}

		public override Value CreateLabel()
		{
			return new Label();
		}

		public override void Divide()
		{
			Emit(new DivInst());
		}

		public override void Duplicate()
		{
			Emit(new DupInst());
		}

		public override void Goto()
		{
			Emit(new GotoInst());
		}

		public override void LeftShift()
		{
			Emit(new LshInst());
		}

		public override void LoadHeap()
		{
			Emit(new LodInst());
		}

		public override void LoadStack()
		{
			Emit(new LodsInst());
		}

		public override void MarkLabel(Value label)
		{
			if (label is Label l)
			{
				Emit(new MarkLabelInst($"L{l.Get()}"));
			}
		}

		public override void MaxHeap()
		{
			Emit(new HeapInst());
		}

		public override void Multiply()
		{
			Emit(new MulInst());
		}

		public override void Negate()
		{
			Emit(new NegInst());
		}

		public override void Not()
		{
			Emit(new NotInst());
		}

		public override void Or()
		{
			Emit(new OrInst());
		}

		public override void Pop()
		{
			Emit(new PopInst());
		}

		public override void PopBasePointer()
		{
			Emit(new BppopInst());
		}

		public override void Push(Value value)
		{
			var inst = new PushInst();
			inst.Arguments[0] = value;
			Emit(inst);
		}

		public override void PushBasePointer()
		{
			Emit(new BppushInst());
		}

		public override void Remainder()
		{
			Emit(new RemInst());
		}

		public override void RightShift()
		{
			Emit(new RshInst());
		}

		public override void SetBasePointer()
		{
			Emit(new BpsetInst());
		}

		public override void StoreHeap()
		{
			Emit(new StrInst());
		}

		public override void StoreStack()
		{
			Emit(new StrsInst());
		}

		public override void Subtract()
		{
			Emit(new SubInst());
		}

		public override void SubtractBorrow()
		{
			Emit(new SubbInst());
		}

		public override void SubtractWithBorrow()
		{
			Emit(new SbbInst());
		}

		public override void Swap()
		{
			Emit(new SwapInst());
		}

		public override void WriteByte(byte value)
		{
			Emit(new RawByteInst(value));
		}

		public override void WriteWord(Value value)
		{
			Emit(new RawWordInst(value));
		}

		public override void Xor()
		{
			Emit(new XorInst());
		}

		public void Emit(Instruction instruction)
		{
			Instructions.Add(instruction);
		}
	}
}
