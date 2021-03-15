namespace Lucida.FlapStacks.Platform.x86_16.Devices
{
	public class TextBoth : Device<Emitter8086>
	{
		public override string Name => "text";

		private readonly TextIn TextIn = new TextIn();
		private readonly TextOut TextOut = new TextOut();

		public override Device CreateNew()
		{
			return new TextBoth();
		}

		public override void BeginUse(Emitter8086 e)
		{
			TextIn.BeginUse(e);
			TextOut.BeginUse(e);
		}

		public override void EndUse(Emitter8086 e)
		{
			TextIn.BeginUse(e);
			TextOut.BeginUse(e);
		}

		public override void Read(Emitter8086 e)
		{
			TextIn.Read(e);
			TextOut.Read(e);
		}

		public override void Write(Emitter8086 e)
		{
			TextIn.Write(e);
			TextOut.Write(e);
		}
	}
}
