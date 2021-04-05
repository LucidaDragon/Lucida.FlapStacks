namespace Lucida.FlapStacks.Platform.JS.Devices
{
	public class TextBoth : Device<JSEmitter>
	{
		public override string Name => "text";

		private readonly TextIn TextIn = new TextIn();
		private readonly TextOut TextOut = new TextOut();

		public override Device CreateNew()
		{
			return new TextBoth();
		}

		public override void BeginUse(JSEmitter e)
		{
			TextIn.BeginUse(e);
			TextOut.BeginUse(e);
		}

		public override void EndUse(JSEmitter e)
		{
			TextOut.EndUse(e);
			TextIn.EndUse(e);
		}

		public override void Read(JSEmitter e)
		{
			TextIn.Read(e);
		}

		public override void Write(JSEmitter e)
		{
			TextOut.Write(e);
		}
	}
}
