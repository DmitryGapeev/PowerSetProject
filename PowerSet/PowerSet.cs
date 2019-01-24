using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AlgorithmsDataStructures
{

	// наследуйте этот класс от HashTable
	// или расширьте его методами из HashTable
	public class PowerSet<T>
	{
		public T[] _values = new T[4];
		public SlotStatus[] _slotsStatus = new SlotStatus[4];
		private int _step = 3;
		private int _count = 0;

		public enum SlotStatus
		{
			Empty,
			Fill
		}

		private void ReHash()
		{
			T[] oldArray = _values;
			SlotStatus[] oldStatusArray = _slotsStatus;

			_values = new T[_count * 2];
			_slotsStatus = new SlotStatus[_count * 2];
			_count = 0;

			for (int i = 0; i < oldArray.Length; i++)
			{
				if (oldStatusArray[i] == SlotStatus.Fill)
					Put(oldArray[i]);
			}
		}

		private int HashFunc(T value)
		{
			int hash = Math.Abs(value.GetHashCode());
			return hash % _values.Length;
		}

		private int SeekSlot(T value)
		{
			int position = HashFunc(value);

			for (int i = 0; i < _slotsStatus.Length; i++)
			{
				if (_slotsStatus[position] == SlotStatus.Empty)
					return position;

				position += _step;
				position %= _slotsStatus.Length;
			}

			return -1;
		}

		private int GetSlot(T value)
		{
			int position = HashFunc(value);
			for (int i = 0; i < _slotsStatus.Length; i++)
			{
				if (_slotsStatus[position] == SlotStatus.Fill && _values[position].Equals(value))
					return position;

				position += _step;
				position %= _slotsStatus.Length;
			}

			return -1;
		}

		public PowerSet()
		{
			for (int i = 0; i < _slotsStatus.Length; i++)
				_slotsStatus[i] = SlotStatus.Empty;
		}

		public int Size()
		{
			return _count;
		}

		public void Put(T value)
		{
			if (!Get(value))
			{
				int position = SeekSlot(value);

				if (position != -1)
				{
					_values[position] = value;
					_slotsStatus[position] = SlotStatus.Fill;
					_count++;
					if (_count == _values.Length)
						ReHash();
				}
				else
					ReHash();
			}
		}


		public bool Get(T value)
		{
			return GetSlot(value) != -1;
		}

		public bool Remove(T value)
		{
			int position = GetSlot(value);

			if (position != -1)
			{
				_values[position] = default(T);
				_slotsStatus[position] = SlotStatus.Empty;
				_count--;
			}

			return position != -1;
		}

		public PowerSet<T> Intersection(PowerSet<T> set2)
		{

			if (set2 == null)
				return null;

			PowerSet<T> resultSet = new PowerSet<T>();

			for (int i = 0; i < set2._values.Length; i++)
			{
				T value = set2._values[i];
				if (set2._slotsStatus[i] == SlotStatus.Fill && Get(value))
					resultSet.Put(value);
			}

			return resultSet;
		}

		public PowerSet<T> Union(PowerSet<T> set2)
		{
			if (set2 == null)
				return null;

			PowerSet<T> resultSet = new PowerSet<T>();

			for (int i = 0; i < _values.Length; i++)
			{
				if (_slotsStatus[i] == SlotStatus.Fill)
					resultSet.Put(_values[i]);
			}

			for (int i = 0; i < set2._values.Length; i++)
			{
				if (set2._slotsStatus[i] == SlotStatus.Fill)
					resultSet.Put(set2._values[i]);
			}

			return resultSet;
		}

		public PowerSet<T> Difference(PowerSet<T> set2)
		{
			if (set2 == null)
				return null;

			PowerSet<T> resultSet = new PowerSet<T>();

			for (int i = 0; i < _values.Length; i++)
			{
				T currentValue = _values[i];
				if (_slotsStatus[i] == SlotStatus.Fill && !set2.Get(currentValue))
					resultSet.Put(currentValue);
			}

			return resultSet;
		}

		public bool IsSubset(PowerSet<T> set2)
		{
			if (set2 == null)
				return false;

			bool result = true;

			for (int i = 0; i < set2._values.Length; i++)
			{
				if (set2._slotsStatus[i] == SlotStatus.Fill && !Get(set2._values[i]))
				{
					result = false;
					break;
				}
			}

			return result;
		}
	}
}