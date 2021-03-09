using System;

namespace Lucida.FlapStacks.Compiler
{
	public class MultiStream : Stream
	{
		public override bool CanRead
		{
			get
			{
				for (int i = 0; i < Streams.Count; i++)
				{
					if (Streams[i].CanRead) return true;
				}

				return false;
			}
		}
		public override bool CanWrite => false;

		private readonly List<Stream> Streams;

		public MultiStream(List<Stream> streams)
		{
			Streams = streams;
		}

		public override byte ReadByte()
		{
			for (int i = 0; i < Streams.Count; i++)
			{
				var stream = Streams[i];

				if (stream.CanRead)
				{
					return stream.ReadByte();
				}
			}

			throw new Exception("Tried to read past end of stream.");
		}

		public override void WriteByte(byte value) { }
	}
}
