namespace Lucida.FlapStacks.Platform.JS
{
	public class JSEmitter : Emitter
	{
		public override string Name => "js";

		public ulong HeapSize { get; set; } = ushort.MaxValue * 16;
		public ulong CoreCount { get; set; } = 2;

		private readonly List<Statement> Lines = new List<Statement>();
		public ulong Address { get; private set; } = 0;

		public const string Context = "context";
		public const string CoreIndex = "coreIndex";
		public const string A = Context + ".A";
		public const string B = Context + ".B";
		public const string C = Context + ".C";
		public const string D = Context + ".D";
		public const string BP = Context + ".BP";
		public const string IField = ".I";
		public const string I = Context + IField;
		public const string StackField = ".stack";
		public const string Stack = Context + StackField;
		public const string ExecField = ".exec";
		public const string Exec = Context + ExecField;
		public const string Heap = "heap";
		public const string Contexts = "contexts";
		public const string ExecuteCore = "ExecuteCore";
		public const string Launch = "Launch";
		public const string RunCores = "RunCores";

		private class HeapSizeValue : Value
		{
			private readonly JSEmitter Emitter;

			public HeapSizeValue(JSEmitter emitter) { Emitter = emitter; }

			public override ulong Get() => Emitter.HeapSize;
		}

		private class CoreCountValue : Value
		{
			private readonly JSEmitter Emitter;

			public CoreCountValue(JSEmitter emitter) { Emitter = emitter; }

			public override ulong Get() => Emitter.CoreCount;
		}

		public JSEmitter()
		{
			Emit(new ConstantStatement("<style>body { color: white; background-color: black; font-weight: 700; overflow-wrap: anywhere; }</style><script>"));
			Emit(new DynamicStatement(vals => $"({Heap} = []).length = {vals[0].Get()}; {Heap}.fill(0);", new HeapSizeValue(this)));
			Emit(new ConstantStatement($"{Contexts} = [];"));
			Emit(new DynamicStatement(vals => $"for (var i = 0; i < {vals[0].Get()}; i++) {{", new CoreCountValue(this)));
			Emit(new ConstantStatement($"var {Context} = ({{}}); {A} = 0; {B} = 0; {C} = 0; {D} = 0; {BP} = 0; {I} = 0; {Stack} = []; {Exec} = (i == 0); {Contexts}[i] = {Context};\n}}"));

			Emit(new ConstantStatement($"function {Launch}() {{ setTimeout({RunCores}, 50); }}"));
			Emit(new DynamicStatement(vals => $"function {RunCores}() {{ var running = false; var start = performance.now(); while((performance.now() - start) < 10) {{ running = false; for (var i = 0; i < {vals[0].Get()}; i++) {{ {ExecuteCore}(i); running |= {Contexts}[i]{ExecField}; }} if (!running) break; }} if (running) {Launch}(); }}", new CoreCountValue(this)));
			Emit(new ConstantStatement($"function {ExecuteCore}({CoreIndex}) {{"));
			Emit(new ConstantStatement($"var {Context} = {Contexts}[{CoreIndex}];"));
			Emit(new ConstantStatement($"if ({Exec}) {{\nconsole.log(\"Core \" + {CoreIndex} + \" \" + {I} + \" \" + {Stack});\nswitch ({I}) {{"));
			Emit(new ConstantStatement($"default:"));
			Assign(Exec, "false");
			Emit("break");
		}

		public override void Comment(string text)
		{
			Begin();
			Emit($"console.log(\"## {text.Replace("\\", "\\\\").Replace("\"", "\\\"")} ##\")");
			End();
		}

		public void Emit(string statement)
		{
			Emit(new ConstantStatement($"{statement};"));
		}

		public void Emit(Statement statement)
		{
			Lines.Add(statement);
		}

		public void Begin()
		{
			Emit(new ConstantStatement($"case {Address}:"));
		}

		public void End()
		{
			Emit($"{I}++");
			Emit("break");
			Address++;
		}

		public string Op(string a, string op, string b)
		{
			return $"({a} {op} {b})";
		}

		public string PopStack()
		{
			return $"{Stack}.pop()";
		}

		public string PushStack(string value)
		{
			return $"{Stack}.push({value})";
		}

		public string StackPointer()
		{
			return Op($"{Stack}.length", "-", "1");
		}

		public string Indexer(string source, string index)
		{
			return $"{source}[{index}]";
		}

		public string PeekStack()
		{
			return Indexer(Stack, StackPointer());
		}

		public string CastInt(string value)
		{
			return $"Math.floor({value})";
		}

		public string CastLow32(string value)
		{
			return $"({CastInt(value)} & 0xFFFFFFFF)";
		}

		public string CastHigh32(string value)
		{
			return CastLow32(Op(value, ">>", "32"));
		}

		public void Assign(string target, string value)
		{
			Emit($"{target} = {value}");
		}

		public string Select(string condition, string ifTrue, string ifFalse)
		{
			return $"({condition} ? {ifTrue} : {ifFalse})";
		}

		public string Unsigned(string value)
		{
			return Op(value, ">>>", "0");
		}

		private void DoBiOp(string op)
		{
			Begin();
			Log($"OP{op}");
			Assign(B, PopStack());
			Assign(A, PopStack());
			Emit(PushStack(CastLow32(Op(A, op, B))));
			End();
		}

		public void Log(string message)
		{
			Emit($"console.log(\"Core \" + {CoreIndex} + \": {message.Replace("\\", "\\\\").Replace("\"", "\\\"")}\")");
		}

		public override void Add()
		{
			DoBiOp("+");
		}

		public override void AddCarry()
		{
			Begin();
			Log(nameof(AddCarry));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(B, PopStack());
			Assign(A, PopStack());
			Assign(A, Op(A, "+", B));
			Emit(PushStack(CastLow32(A)));
			Assign(I, Op(Select(Op(CastHigh32(A), "!=", "0"), C, D), "-", "1"));
			End();
		}

		public override void AddWithCarry()
		{
			Begin();
			Log(nameof(AddWithCarry));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(B, PopStack());
			Assign(A, PopStack());
			Assign(A, Op(Op(A, "+", B), "+", "1"));
			Emit(PushStack(CastLow32(A)));
			Assign(I, Op(Select(Op(CastHigh32(A), "!=", "0"), C, D), "-", "1"));
			End();
		}

		public override void And()
		{
			DoBiOp("&");
		}

		public override void Bool()
		{
			Begin();
			Log(nameof(Bool));
			Emit(PushStack(Select(Op(PopStack(), "!=", "0"), "1", "0")));
			End();
		}

		public override void BranchEqual()
		{
			Begin();
			Log(nameof(BranchEqual));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(B, PopStack());
			Assign(A, PopStack());
			Assign(I, Select(Op(A, "==", B), C, D));
			End();
		}

		public override void BranchSignedGreater()
		{
			Begin();
			Log(nameof(BranchSignedGreater));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(B, PopStack());
			Assign(A, PopStack());
			Assign(I, Select(Op(A, ">", B), C, D));
			End();
		}

		public override void BranchSignedLess()
		{
			Begin();
			Log(nameof(BranchSignedLess));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(B, PopStack());
			Assign(A, PopStack());
			Assign(I, Select(Op(A, "<", B), C, D));
			End();
		}

		public override void BranchUnsignedGreater()
		{
			Begin();
			Log(nameof(BranchUnsignedGreater));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(B, PopStack());
			Assign(A, PopStack());
			Assign(I, Select(Op(Unsigned(A), ">", Unsigned(B)), C, D));
			End();
		}

		public override void BranchUnsignedLess()
		{
			Begin();
			Log(nameof(BranchUnsignedLess));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(B, PopStack());
			Assign(A, PopStack());
			Assign(I, Select(Op(Unsigned(A), "<", Unsigned(B)), C, D));
			End();
		}

		public override void BranchZero()
		{
			Begin();
			Log(nameof(BranchZero));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(A, PopStack());
			Assign(I, Select(Op(A, "==", "0"), C, D));
			End();
		}

		public override void Call()
		{
			Begin();
			Log(nameof(Call));
			Assign(A, Op(PopStack(), "-", "1"));
			Emit(PushStack(Op(I, "+", "1")));
			Assign(I, A);
			End();
		}

		public override Value CreateLabel()
		{
			return new Label();
		}

		public override void Divide()
		{
			DoBiOp("/");
		}

		public override void Duplicate()
		{
			Begin();
			Log(nameof(Duplicate));
			Emit(PushStack(PeekStack()));
			End();
		}

		public override void Goto()
		{
			Begin();
			Log(nameof(Goto));
			Assign(I, Op(PopStack(), "-", "1"));
			End();
		}

		public override void LeftShift()
		{
			DoBiOp("<<");
		}

		public override void LoadHeap()
		{
			Begin();
			Log(nameof(LoadHeap));
			Emit(PushStack(Indexer(Heap, PopStack())));
			End();
		}

		public override void LoadStack()
		{
			Begin();
			Log(nameof(LoadStack));
			Emit(PushStack(Indexer(Stack, Op(BP, "-", PopStack()))));
			End();
		}

		public override void MarkLabel(Value label)
		{
			if (label is Label l)
			{
				l.Mark(Address);
			}
		}

		public override void MaxHeap()
		{
			Begin();
			Log(nameof(MaxHeap));
			Emit(PushStack((HeapSize - 1).ToString()));
			End();
		}

		public override void Multiply()
		{
			DoBiOp("*");
		}

		public override void Negate()
		{
			Begin();
			Log(nameof(Negate));
			Emit(PushStack($"-{PopStack()}"));
			End();
		}

		public override void Not()
		{
			Begin();
			Log(nameof(Not));
			Emit(PushStack($"~{PopStack()}"));
			End();
		}

		public override void Or()
		{
			DoBiOp("|");
		}

		public override void Pop()
		{
			Begin();
			Log(nameof(Pop));
			Emit(PopStack());
			End();
		}

		public override void PopBasePointer()
		{
			Begin();
			Log(nameof(PopBasePointer));
			Assign(BP, PopStack());
			End();
		}

		public override void Push(Value value)
		{
			Begin();
			Log(nameof(Push));
			Emit(new DynamicStatement(vals => $"{PushStack(vals[0].Get().ToString())};", value));
			End();
		}

		public override void PushBasePointer()
		{
			Begin();
			Log(nameof(PushBasePointer));
			Emit(PushStack(BP));
			End();
		}

		public override void Remainder()
		{
			DoBiOp("%");
		}

		public override void RightShift()
		{
			DoBiOp(">>>");
		}

		public override void Save(Stream stream)
		{
			Emit(new ConstantStatement($"}}}}\nconsole.log(\"Execution suspended on core \" + {CoreIndex});\n}}"));
			Emit($"window.onload = function(e) {{ {Launch}(); }}");
			Emit(new ConstantStatement("</script>\n<body style=\"font-family: monospace;\"></body>"));

			for (int i = 0; i < Lines.Count; i++)
			{
				var str = Lines[i].GetString();

				for (int j = 0; j < str.Length; j++)
				{
					stream.WriteByte((byte)str[j]);
				}

				stream.WriteByte((byte)'\n');
			}
		}

		public override void SetBasePointer()
		{
			Begin();
			Log(nameof(SetBasePointer));
			Assign(BP, StackPointer());
			End();
		}

		public override void StoreHeap()
		{
			Begin();
			Log(nameof(StoreHeap));
			Assign(A, PopStack());
			Assign(Indexer(Heap, A), PopStack());
			End();
		}

		public override void StoreStack()
		{
			Begin();
			Log(nameof(StoreStack));
			Assign(A, PopStack());
			Assign(Indexer(Stack, Op(BP, "-", A)), PopStack());
			End();
		}

		public override void Subtract()
		{
			DoBiOp("-");
		}

		public override void SubtractBorrow()
		{
			Begin();
			Log(nameof(SubtractBorrow));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(B, PopStack());
			Assign(A, PopStack());
			Assign(A, Op(A, "-", B));
			Emit(PushStack(CastLow32(A)));
			Assign(I, Op(Select(Op(CastHigh32(A), "!=", "0"), C, D), "-", "1"));
			End();
		}

		public override void SubtractWithBorrow()
		{
			Begin();
			Log(nameof(SubtractWithBorrow));
			Assign(D, PopStack());
			Assign(C, PopStack());
			Assign(B, PopStack());
			Assign(A, PopStack());
			Assign(A, Op(Op(A, "-", B), "-", "1"));
			Emit(PushStack(CastLow32(A)));
			Assign(I, Op(Select(Op(CastHigh32(A), "!=", "0"), C, D), "-", "1"));
			End();
		}

		public override void Swap()
		{
			Begin();
			Log(nameof(Swap));
			Assign(A, PopStack());
			Assign(B, PopStack());
			Emit(PushStack(A));
			Emit(PushStack(B));
			End();
		}

		public override void WriteByte(byte value) { }

		public override void WriteWord(Value value) { }

		public override void Xor()
		{
			DoBiOp("^");
		}

		public override void Cores()
		{
			Begin();
			Log(nameof(Cores));
			Emit(new DynamicStatement(vals => $"{PushStack(vals[0].Get().ToString())};", new CoreCountValue(this)));
			End();
		}

		public override void Core()
		{
			Begin();
			Log(nameof(Core));
			Emit(PushStack(CoreIndex));
			End();
		}

		public override void Start()
		{
			Begin();
			Log(nameof(Start));
			Assign(A, PopStack());
			Assign(B, PopStack());
			Assign($"{Contexts}[{B}]{IField}", A);
			Assign($"{Contexts}[{B}]{ExecField}", "true");
			Emit($"if ({Op(B, "==", CoreIndex)}) {I} -= 1");
			End();
		}

		public override void Stop()
		{
			Begin();
			Log(nameof(Stop));
			Assign($"{Contexts}[{PopStack()}]{ExecField}", "false");
			End();
		}

		public override void Join()
		{
			Begin();
			Log(nameof(Join));
			Emit(new ConstantStatement($"if ({Contexts}[{PeekStack()}]{ExecField}) {{ {I} -= 1; }} else {{ {PopStack()}; }}"));
			End();
		}
	}
}
