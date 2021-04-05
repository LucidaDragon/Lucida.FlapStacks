namespace Lucida.FlapStacks.Platform.JS.Devices
{
	public class TextOut : Device<JSEmitter>
	{
		public override string Name => "text-out";

		private const string Screen = "screen";

		public override Device CreateNew()
		{
			return new TextOut();
		}

		public override void BeginUse(JSEmitter e)
		{
			e.Begin();
			e.Log($"{nameof(TextOut)} {nameof(BeginUse)}");
			e.Assign(Screen, "\"\"");
			e.End();
		}

		public override void EndUse(JSEmitter e) { }

		public override void Read(JSEmitter e)
		{
			e.Begin();
			e.Log($"{nameof(TextOut)} {nameof(Read)}");
			e.Assign(JSEmitter.I, e.Op(e.PopStack(), "-", "1"));
			e.Emit(e.PopStack());
			e.Emit(e.PopStack());
			e.Emit(e.PushStack("0"));
			e.End();
		}

		public override void Write(JSEmitter e)
		{
			e.Begin();
			e.Log($"{nameof(TextOut)} {nameof(Write)}");
			e.Emit(e.PopStack());
			e.Assign(JSEmitter.I, e.Op(e.PopStack(), "-", "1"));
			e.Emit($"{Screen} += String.fromCharCode({e.PopStack()})");
			e.Emit($"document.body.innerText = {Screen}");
			e.End();
		}
	}
}
