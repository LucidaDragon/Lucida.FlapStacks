namespace Lucida.FlapStacks.Platform.JS.Devices
{
	public class TextIn : Device<JSEmitter>
	{
		public override string Name => "text-in";

		private const string Null = "null";
		private const string KeyWaitI = "keywait";
		private const string KeyWaitContext = "keywaitcontext";
		private const string KeyQueue = "keyqueue";

		public override Device CreateNew()
		{
			return new TextIn();
		}

		public override void BeginUse(JSEmitter e)
		{
			e.Begin();
			e.Log($"{nameof(TextIn)} {nameof(BeginUse)}");
			e.Assign(KeyWaitI, Null);
			e.Assign(KeyWaitContext, Null);
			e.Assign(KeyQueue, "[]");
			e.Emit(new ConstantStatement("document.onkeypress = function(e) {"));
			e.Emit($"{KeyQueue}.push(e.keyCode)");
			e.Emit(new ConstantStatement($"if ({KeyWaitI} != null) {{"));
			e.Emit($"{JSEmitter.Contexts}[{KeyWaitContext}]{JSEmitter.StackField}.push({KeyQueue}.shift())");
			e.Assign($"{JSEmitter.Contexts}[{KeyWaitContext}]{JSEmitter.IField}", KeyWaitI);
			e.Assign($"{JSEmitter.Contexts}[{KeyWaitContext}]{JSEmitter.ExecField}", "true");
			e.Assign(KeyWaitI, Null);
			e.Emit($"{JSEmitter.Launch}()");
			e.Emit(new ConstantStatement("}"));
			e.Emit(new ConstantStatement("};"));
			e.End();
		}

		public override void EndUse(JSEmitter e) { }

		public override void Read(JSEmitter e)
		{
			e.Begin();
			e.Log($"{nameof(TextIn)} {nameof(Read)}");
			e.Emit(e.PopStack());
			e.Emit(new ConstantStatement($"if ({KeyQueue}.length != 0) {{"));
			e.Assign(JSEmitter.I, e.Op(e.PopStack(), "-", "1"));
			e.Emit(e.PushStack($"{KeyQueue}.shift()"));
			e.Emit(new ConstantStatement("} else {"));
			e.Assign(JSEmitter.Exec, "false");
			e.Assign(KeyWaitContext, JSEmitter.CoreIndex);
			e.Assign(KeyWaitI, e.PopStack());
			e.Emit(new ConstantStatement("}"));
			e.End();
		}

		public override void Write(JSEmitter e)
		{
			e.Begin();
			e.Log($"{nameof(TextIn)} {nameof(Write)}");
			e.Assign(JSEmitter.I, e.Op(e.PopStack(), "-", "1"));
			e.Emit(e.PopStack());
			e.Emit(e.PopStack());
			e.End();
		}
	}
}
