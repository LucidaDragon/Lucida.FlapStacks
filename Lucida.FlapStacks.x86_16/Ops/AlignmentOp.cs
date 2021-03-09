namespace Lucida.FlapStacks.Platform.x86_16.Ops
{
	public class AlignmentOp : Op
	{
		public ulong AlignTo { get; set; }

		public AlignmentOp() { }

		public AlignmentOp(ulong alignTo)
		{
			AlignTo = alignTo;
		}

		public override int GetSize(Emitter8086 emitter)
		{
			var current = emitter.GetAddress(this);
			return (int)(AlignTo - current);
		}

		public override void Emit(Emitter8086 emitter, Stream stream)
		{
			var times = GetSize(emitter);
			for (int i = 0; i < times; i++)
			{
				stream.WriteByte(0);
			}
		}
	}
}
