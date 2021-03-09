namespace Lucida.FlapStacks
{
	public abstract class Stream
	{
		public abstract bool CanRead { get; }
		public abstract bool CanWrite { get; }

		public abstract void WriteByte(byte value);
		public abstract byte ReadByte();

		public void WriteByte(sbyte value)
		{
			WriteByte((byte)value);
		}

		public void WriteLittleEndian(ushort value)
		{
			WriteLength(2, value, false);
		}

		public void WriteBigEndian(ushort value)
		{
			WriteLength(2, value, true);
		}

		public void WriteLittleEndian(uint value)
		{
			WriteLength(4, value, false);
		}

		public void WriteBigEndian(uint value)
		{
			WriteLength(4, value, true);
		}

		public void WriteLittleEndian(ulong value)
		{
			WriteLength(8, value, false);
		}

		public void WriteBigEndian(ulong value)
		{
			WriteLength(8, value, true);
		}

		public void WriteLittleEndian(short value)
		{
			WriteLittleEndian((ushort)value);
		}

		public void WriteBigEndian(short value)
		{
			WriteBigEndian((ushort)value);
		}

		public void WriteLittleEndian(int value)
		{
			WriteLittleEndian((uint)value);
		}

		public void WriteBigEndian(int value)
		{
			WriteBigEndian((uint)value);
		}

		public void WriteLittleEndian(long value)
		{
			WriteLittleEndian((ulong)value);
		}

		public void WriteBigEndian(long value)
		{
			WriteBigEndian((ulong)value);
		}

		private void WriteLength(int length, ulong value, bool bigEndian)
		{
			var buffer = new byte[length];

			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)value;
				value >>= 8;
			}

			if (bigEndian)
			{
				for (int i = buffer.Length - 1; i >= 0; i--)
				{
					WriteByte(buffer[i]);
				}
			}
			else
			{
				for (int i = 0; i < buffer.Length; i++)
				{
					WriteByte(buffer[i]);
				}
			}
		}
	}
}
