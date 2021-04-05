namespace Lucida.FlapStacks
{
	public class List<T>
	{
		public T this[int index]
		{
			get
			{
				if (index >= 0 && index < Count)
				{
					return Store[index];
				}
				else
				{
					return default;
				}
			}
			set
			{
				if (index > 0 && index < Count)
				{
					Store[index] = value;
				}
			}
		}

		public int Count { get; private set; } = 0;

		public int Capacity => Store.Length;

		private const int MinSize = 1024;

		private T[] Store = new T[MinSize];

		public void Add(T item)
		{
			if (Count == Capacity)
			{
				Grow();
			}

			Store[Count] = item;
			Count++;
		}

		public void RemoveAt(int index)
		{
			for (int i = index; i < Store.Length - 1; i++)
			{
				Store[i] = Store[i + 1];
			}

			Count--;

			if (Count < Capacity / 2 && Capacity > MinSize)
			{
				Shrink();
			}
		}

		public void Clear()
		{
			Count = 0;
			Store = new T[MinSize];
		}

		public T[] ToArray()
		{
			var result = new T[Count];

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = Store[i];
			}

			return result;
		}

		private void Grow()
		{
			var newStore = new T[Store.Length * 2];

			for (int i = 0; i < Store.Length; i++)
			{
				newStore[i] = Store[i];
			}

			Store = newStore;
		}

		private void Shrink()
		{
			var newStore = new T[Store.Length / 2];

			for (int i = 0; i < newStore.Length; i++)
			{
				newStore[i] = Store[i];
			}

			Store = newStore;
		}
	}
}
