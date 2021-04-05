using System;

namespace Lucida.FlapStacks
{
	public abstract class LinearMemoryEmitter : Emitter
	{
		public Value StackPointer { get; private set; }
		public Value BasePointer { get; private set; }
		public Value A { get; private set; }
		public Value B { get; private set; }
		public Value C { get; private set; }
		public Value D { get; private set; }
		public Value E { get; private set; }
		public Value F { get; private set; }
		public Value StackBottom { get; private set; }
		public Value StackTop { get; private set; }
		public Value HeapEnd { get; private set; }
		public Value HeapStart { get; private set; }

		public abstract Value MaxAddress { get; }
		public abstract Value ValueSize { get; }

		public Value ValueMask => new BitMask(ValueSize);
		public Value SignMask => new XoredValue(ValueMask, new RightShiftedValue(ValueMask, new Constant(1)));

		private Value AllocationOffset = new Constant(0);

		public LinearMemoryEmitter()
		{
			StackPointer = Allocate();
			BasePointer = Allocate();
			A = Allocate();
			B = Allocate();
			C = Allocate();
			D = Allocate();
			E = Allocate();
			F = Allocate();
			Allocate(new DividedValue(RemainingMemory(), new Constant(2)), out Value stackTop, out Value stackBottom);
			StackBottom = stackBottom;
			StackTop = stackTop;
			Allocate(RemainingMemory(), out Value heapStart, out Value heapEnd);
			HeapEnd = heapEnd;
			HeapStart = heapStart;
		}

		private Value Allocate()
		{
			return Allocate(new Constant(1), out _, out _);
		}

		private Value Allocate(Value size, out Value start, out Value end)
		{
			end = new SubtractedValue(MaxAddress, AllocationOffset, true);
			AllocationOffset = new AddedValue(AllocationOffset, size, true, MaxAddress);
			start = new SubtractedValue(MaxAddress, new SubtractedValue(AllocationOffset, new Constant(1), true), true);
			return start;
		}

		private Value RemainingMemory()
		{
			return new AddedValue(new SubtractedValue(MaxAddress, AllocationOffset), new Constant(1));
		}

		private class BitMask : Value
		{
			public Value ValueSize { get; set; }

			public BitMask(Value valueSize)
			{
				ValueSize = valueSize;
			}

			public override ulong Get()
			{
				var result = 0UL;
				var size = ValueSize.Get();

				for (ulong i = 0; i < size; i++)
				{
					result |= 1;
					result <<= 1;
				}

				return result;
			}
		}

		private class AddedValue : Value
		{
			public Value A { get; set; }
			public Value B { get; set; }
			public bool ThrowIfOutOfRange { get; set; }
			public Value MaxValue { get; set; }

			public AddedValue(Value a, Value b, bool throwIfOutOfRange = false, Value maxValue = default)
			{
				A = a;
				B = b;
				ThrowIfOutOfRange = throwIfOutOfRange;
				MaxValue = maxValue;
			}

			public override ulong Get()
			{
				var a = A.Get();
				var b = B.Get();

				if (ThrowIfOutOfRange && b > (MaxValue.Get() - a))
				{
					throw new Exception("Ran out of statically allocated memory for the target architecture.");
				}

				return a + b;
			}
		}

		private class SubtractedValue : Value
		{
			public Value A { get; set; }
			public Value B { get; set; }
			public bool ThrowIfOutOfRange { get; set; }

			public SubtractedValue(Value a, Value b, bool throwIfOutOfRange = false)
			{
				A = a;
				B = b;
				ThrowIfOutOfRange = throwIfOutOfRange;
			}

			public override ulong Get()
			{
				var a = A.Get();
				var b = B.Get();

				if (ThrowIfOutOfRange && b > a)
				{
					throw new Exception("Ran out of statically allocated memory for the target architecture.");
				}

				return a - b;
			}
		}

		private class DividedValue : Value
		{
			public Value A { get; set; }
			public Value B { get; set; }

			public DividedValue(Value a, Value b)
			{
				A = a;
				B = b;
			}

			public override ulong Get()
			{
				return A.Get() / B.Get();
			}
		}

		private class XoredValue : Value
		{
			public Value A { get; set; }
			public Value B { get; set; }

			public XoredValue(Value a, Value b)
			{
				A = a;
				B = b;
			}

			public override ulong Get()
			{
				return A.Get() ^ B.Get();
			}
		}

		private class RightShiftedValue : Value
		{
			public Value A { get; set; }
			public Value B { get; set; }

			public RightShiftedValue(Value a, Value b)
			{
				A = a;
				B = b;
			}

			public override ulong Get()
			{
				return A.Get() >> (int)B.Get();
			}
		}

		/// <summary>
		/// Copy a value from the source address to the destination address.
		/// </summary>
		/// <param name="fromAddress">The source address to copy from.</param>
		/// <param name="toAddress">The destination address to copy to.</param>
		public abstract void Move(Value fromAddress, Value toAddress);

		/// <summary>
		/// Copy a value from an address stored at the indirect source address to the destination address.
		/// </summary>
		/// <param name="indirectFromAddress">The address that the source address to copy from is stored at.</param>
		/// <param name="toAddress">The destination address to copy to.</param>
		public abstract void MoveIndirectFrom(Value indirectFromAddress, Value toAddress);

		/// <summary>
		/// Copy a value from the source address to the address stored at the indirect destination address.
		/// </summary>
		/// <param name="indirectFromAddress">The source address to copy from.</param>
		/// <param name="toAddress">The address that the destination address to copy to is stored at.</param>
		public abstract void MoveIndirectTo(Value fromAddress, Value toIndirectAddress);

		/// <summary>
		/// Copy a value to the destination address.
		/// </summary>
		/// <param name="immediate">The value to copy.</param>
		/// <param name="address">The destination address to copy to.</param>
		public abstract void Immediate(Value immediate, Value toAddress);

		/// <summary>
		/// Increment the value at the specified address.
		/// </summary>
		/// <param name="address">The address of the value to increment.</param>
		public abstract void Increment(Value address);

		/// <summary>
		/// Decrement the value at the specified address.
		/// </summary>
		/// <param name="address">The address of the value to decrement.</param>
		public abstract void Decrement(Value address);

		/// <summary>
		/// Add two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void Add(Value a, Value b, Value destination);

		/// <summary>
		/// Add two values from the source addresses and store the result at the destination address, then store the carry bit.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The first destination address to store the result to.</param>
		/// <param name="carryOut">The second destination address to store the carry to.</param>
		public abstract void AddCarry(Value a, Value b, Value destination, Value carryOut);

		/// <summary>
		/// Add two values from the source addresses with a constant of one and store the result at the destination address, then store the carry bit.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The first destination address to store the result to.</param>
		/// <param name="carryOut">The second destination address to store the carry to.</param>
		public abstract void AddWithCarry(Value a, Value b, Value destination, Value carryOut);

		/// <summary>
		/// Subtract two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void Subtract(Value a, Value b, Value destination);

		/// <summary>
		/// Subtract two values from the source addresses and store the result at the destination address, then store the borrow bit.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The first destination address to store the result to.</param>
		/// <param name="borrowOut">The second destination address to store the borrow to.</param>
		public abstract void SubtractBorrow(Value a, Value b, Value destination, Value borrowOut);

		/// <summary>
		/// Subtract two values from the source addresses with a constant of one and store the result at the destination address, then store the borrow bit.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The first destination address to store the result to.</param>
		/// <param name="borrowOut">The second destination address to store the borrow to.</param>
		public abstract void SubtractWithBorrow(Value a, Value b, Value destination, Value borrowOut);

		/// <summary>
		/// Multiply two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void Multiply(Value a, Value b, Value destination);

		/// <summary>
		/// Divide two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void Divide(Value a, Value b, Value destination);

		/// <summary>
		/// Modulo two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void Remainder(Value a, Value b, Value destination);

		/// <summary>
		/// Not the value at the specified address.
		/// </summary>
		/// <param name="address">The address of the value to not.</param>
		public abstract void Not(Value address);

		/// <summary>
		/// And two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void And(Value a, Value b, Value destination);

		/// <summary>
		/// Or two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void Or(Value a, Value b, Value destination);

		/// <summary>
		/// Xor two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void Xor(Value a, Value b, Value destination);

		/// <summary>
		/// Left shift two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void LeftShift(Value a, Value b, Value destination);

		/// <summary>
		/// Right shift two values from the source addresses and store the result at the destination address.
		/// </summary>
		/// <param name="a">The first source address to load from.</param>
		/// <param name="b">The second source address to load from.</param>
		/// <param name="destination">The destination address to store to.</param>
		public abstract void RightShift(Value a, Value b, Value destination);

		/// <summary>
		/// Jump to the address stored at the indirect address.
		/// </summary>
		/// <param name="indirectAddress">The indirect address to jump to.</param>
		public abstract void GotoIndirect(Value indirectAddress);

		/// <summary>
		/// Branch between two indirect addresses based on if the value at the source address is zero.
		/// </summary>
		/// <param name="fromAddress">The source address to load from.</param>
		/// <param name="indirectTrueAddress">The indirect address to branch to if the condition is true.</param>
		/// <param name="indirectFalseAddress">The indirect address to branch to if the condition is false.</param>
		public abstract void BranchZeroIndirect(Value fromAddress, Value indirectTrueAddress, Value indirectFalseAddress);

		/// <summary>
		/// Store the index of the executing core at the destination address.
		/// </summary>
		/// <param name="toAddress">The destination address to store to.</param>
		public abstract void Core(Value toAddress);

		/// <summary>
		/// Store the number of cores at the destination address.
		/// </summary>
		/// <param name="toAddress">The destination address to store to.</param>
		public abstract void Cores(Value toAddress);

		/// <summary>
		/// Start the core with the index stored at the source address and jump it to the address stored at the indirect address.
		/// </summary>
		/// <param name="fromAddress">The source address to load from.</param>
		/// <param name="indirectAddress">The indirect address to jump to.</param>
		public abstract void Start(Value fromAddress, Value indirectAddress);

		/// <summary>
		/// Stop the core with the index stored at the source address.
		/// </summary>
		/// <param name="fromAddress">The source address to load from.</param>
		public abstract void Stop(Value fromAddress);

		/// <summary>
		/// Wait for the core with the index stored at the source address to stop.
		/// </summary>
		/// <param name="fromAddress">The source address to load from.</param>
		public abstract void Join(Value fromAddress);

		/// <summary>
		/// Lock the address stored at the indirect address and store the success bit.
		/// </summary>
		/// <param name="indirectAddress">The indirect address to lock.</param>
		/// <param name="successAddress">The destination address to store to.</param>
		public abstract void Lock(Value indirectAddress, Value successAddress);

		/// <summary>
		/// Unlock the address stored at the indirect address.
		/// </summary>
		/// <param name="indirectAddress">The indirect address to unlock.</param>
		public abstract void Unlock(Value indirectAddress);

		public void PushFrom(Value address)
		{
			Decrement(StackPointer);
			MoveIndirectTo(address, StackPointer);
		}

		public void PopTo(Value address)
		{
			MoveIndirectFrom(StackPointer, address);
			Increment(StackPointer);
		}

		public override void Add()
		{
			PopTo(B);
			PopTo(A);
			Add(A, B, A);
			PushFrom(A);
		}

		public override void AddCarry()
		{
			PopTo(D);
			PopTo(C);
			PopTo(B);
			PopTo(A);
			AddCarry(A, B, A, B);
			PushFrom(A);
			BranchZeroIndirect(B, D, C);
		}

		public override void AddWithCarry()
		{
			PopTo(D);
			PopTo(C);
			PopTo(B);
			PopTo(A);
			AddWithCarry(A, B, A, B);
			PushFrom(A);
			BranchZeroIndirect(B, D, C);
		}

		public override void And()
		{
			PopTo(B);
			PopTo(A);
			And(A, B, A);
			PushFrom(A);
		}

		public override void Bits()
		{
			Push(ValueSize);
		}

		public override void Bool()
		{
			var ifZero = CreateLabel();
			var ifNotZero = CreateLabel();
			var end = CreateLabel();
			PopTo(A);
			Immediate(ifZero, B);
			Immediate(ifNotZero, C);
			BranchZeroIndirect(A, B, C);
			MarkLabel(ifZero);
			Immediate(new Constant(0), A);
			PushFrom(A);
			Immediate(end, A);
			GotoIndirect(A);
			MarkLabel(ifNotZero);
			Immediate(new Constant(1), A);
			PushFrom(A);
			MarkLabel(end);
		}

		public override void BranchEqual()
		{
			PopTo(D);
			PopTo(C);
			PopTo(B);
			PopTo(A);
			Xor(A, B, A);
			BranchZeroIndirect(A, C, D);
		}

		public override void BranchSignedGreater()
		{
			var test = CreateLabel();
			PopTo(D);
			PopTo(C);
			PopTo(B);
			PopTo(A);
			Xor(A, B, E);
			Immediate(test, F);
			BranchZeroIndirect(E, D, F);
			MarkLabel(test);
			Subtract(A, B, E);
			Not(B);
			And(A, B, F);
			And(B, E, B);
			And(A, E, A);
			Or(A, B, A);
			Or(A, F, A);
			BranchZeroIndirect(A, C, D);
		}

		public override void BranchSignedLess()
		{
			PopTo(D);
			PopTo(C);
			PopTo(B);
			PopTo(A);
			Subtract(A, B, E);
			Not(B);
			And(A, B, F);
			And(B, E, B);
			And(A, E, A);
			Or(A, B, A);
			Or(A, F, A);
			BranchZeroIndirect(A, D, C);
		}

		public override void BranchUnsignedGreater()
		{
			var test = CreateLabel();
			PopTo(D);
			PopTo(C);
			PopTo(B);
			PopTo(A);
			SubtractBorrow(A, B, B, A);
			Immediate(test, E);
			BranchZeroIndirect(B, D, E);
			MarkLabel(E);
			BranchZeroIndirect(A, D, C);
		}

		public override void BranchUnsignedLess()
		{
			PopTo(D);
			PopTo(C);
			PopTo(B);
			PopTo(A);
			SubtractBorrow(A, B, B, A);
			BranchZeroIndirect(A, D, C);
		}

		public override void BranchZero()
		{
			PopTo(C);
			PopTo(B);
			PopTo(A);
			BranchZeroIndirect(A, B, C);
		}

		public override void Call()
		{
			var returnAddress = CreateLabel();
			PopTo(B);
			Immediate(returnAddress, A);
			PushFrom(A);
			GotoIndirect(B);
			MarkLabel(returnAddress);
		}

		public override void Core()
		{
			Core(A);
			PushFrom(A);
		}

		public override void Cores()
		{
			Cores(A);
			PushFrom(A);
		}

		public override void Divide()
		{
			PopTo(B);
			PopTo(A);
			Divide(A, B, A);
			PushFrom(A);
		}

		public override void Duplicate()
		{
			MoveIndirectFrom(StackPointer, A);
			PushFrom(A);
		}

		public override void Goto()
		{
			PopTo(A);
			GotoIndirect(A);
		}

		public override void Join()
		{
			PopTo(A);
			Join(A);
		}

		public override void LeftShift()
		{
			PopTo(B);
			PopTo(A);
			LeftShift(A, B, A);
			PushFrom(A);
		}

		public override void LoadHeap()
		{
			PopTo(A);
			MoveIndirectFrom(A, A);
			PushFrom(A);
		}

		public override void LoadStack()
		{
			PopTo(A);
			Add(BasePointer, A, A);
			MoveIndirectFrom(A, A);
			PushFrom(A);
		}

		public override void Lock()
		{
			PopTo(D);
			PopTo(C);
			PopTo(B);
			Lock(B, A);
			BranchZeroIndirect(A, D, C);
		}

		public override void MaxHeap()
		{
			Push(HeapEnd);
		}

		public override void Multiply()
		{
			PopTo(B);
			PopTo(A);
			Multiply(A, B, A);
			PushFrom(A);
		}

		public override void Negate()
		{
			PopTo(B);
			Immediate(new Constant(0), A);
			Subtract(A, B, A);
			PushFrom(A);
		}

		public override void Not()
		{
			PopTo(A);
			Not(A);
			PushFrom(A);
		}

		public override void Or()
		{
			PopTo(B);
			PopTo(A);
			Or(A, B, A);
			PushFrom(A);
		}

		public override void Pop()
		{
			Increment(StackPointer);
		}

		public override void PopBasePointer()
		{
			PopTo(BasePointer);
		}

		public override void Push(Value value)
		{
			Immediate(value, A);
			PushFrom(A);
		}

		public override void PushBasePointer()
		{
			PushFrom(BasePointer);
		}

		public override void Remainder()
		{
			PopTo(B);
			PopTo(A);
			Remainder(A, B, A);
			PushFrom(A);
		}

		public override void RightShift()
		{
			PopTo(B);
			PopTo(A);
			RightShift(A, B, A);
			PushFrom(A);
		}

		public override void SetBasePointer()
		{
			Move(StackPointer, BasePointer);
		}

		public override void Start()
		{
			PopTo(A);
			PopTo(B);
			Start(B, A);
		}

		public override void Stop()
		{
			PopTo(A);
			Stop(A);
		}

		public override void StoreHeap()
		{
			PopTo(A);
			PopTo(B);
			MoveIndirectTo(B, A);
		}

		public override void StoreStack()
		{
			PopTo(A);
			PopTo(B);
			Add(BasePointer, A, A);
			MoveIndirectTo(B, A);
		}

		public override void Subtract()
		{
			PopTo(B);
			PopTo(A);
			Subtract(A, B, A);
			PushFrom(A);
		}

		public override void SubtractBorrow()
		{
			PopTo(D);
			PopTo(C);
			PopTo(B);
			PopTo(A);
			SubtractBorrow(A, B, A, B);
			PushFrom(A);
			BranchZeroIndirect(B, D, C);
		}

		public override void SubtractWithBorrow()
		{
			PopTo(D);
			PopTo(C);
			PopTo(B);
			PopTo(A);
			SubtractWithBorrow(A, B, A, B);
			PushFrom(A);
			BranchZeroIndirect(B, D, C);
		}

		public override void Swap()
		{
			PopTo(A);
			PopTo(B);
			PushFrom(A);
			PushFrom(B);
		}

		public override void Unlock()
		{
			PopTo(A);
			Unlock(A);
		}

		public override void Xor()
		{
			PopTo(B);
			PopTo(A);
			Xor(A, B, A);
			PushFrom(A);
		}
	}
}
