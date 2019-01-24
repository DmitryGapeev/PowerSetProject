using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsDataStructures
{
	class Program
	{
		static void Main(string[] args)
		{
			//1. 2 пустых
			//2. пустой, со значениями
			//3. со значениями, пустой
			//4. больше значений, меньше значений
			//5. меньше значений, больше
			//6. нет общих

			//TestPowerSet();

			Func<PowerSet<int>, PowerSet<int>, PowerSet<int>> intersectionFunc = (set1, set2) =>  set1.Intersection(set2);

			TestFunc(CreateEmptyPowerSet(), CreateEmptyPowerSet(), intersectionFunc);
			TestFunc(CreateEmptyPowerSet(), CreatePowerSet(10, 20), intersectionFunc);

			TestFunc(CreatePowerSet(10,20), CreateEmptyPowerSet(), intersectionFunc);
			TestFunc(CreatePowerSet(10, 50), CreatePowerSet(20, 35), intersectionFunc);

			TestFunc(CreatePowerSet(20, 25), CreatePowerSet(10, 50), intersectionFunc);
			TestFunc(CreatePowerSet(10, 20), CreatePowerSet(30, 40), intersectionFunc);



			Console.ReadKey();
		}

		static void TestPowerSet()
		{
			Console.WriteLine("test power set");
			PowerSet<int> powerSet = new PowerSet<int>();

			for (int i = 0; i <= 10; i++)
				powerSet.Put(i);

			for (int i = 0; i < powerSet._values.Length; i++)
				Console.Write(powerSet._values[i] + " ");

			Console.WriteLine();
			Console.WriteLine("test put 11");
			Console.WriteLine("power set size before  = " + powerSet.Size());
			powerSet.Put(11);
			Console.WriteLine("power set size after = " + powerSet.Size());

			for (int i = 0; i < powerSet._values.Length; i++)
				Console.Write(powerSet._values[i] + " ");

			Console.WriteLine();

			Console.WriteLine("test put 10");
			Console.WriteLine("power set size before  = " + powerSet.Size());
			powerSet.Put(10);
			Console.WriteLine("power set size after = " + powerSet.Size());

			for (int i = 0; i < powerSet._values.Length; i++)
				Console.Write(powerSet._values[i] + " ");

			Console.WriteLine();
			Console.WriteLine("test remove 4");
			Console.WriteLine("power set size before  = " + powerSet.Size());
			bool result = powerSet.Remove(4);
			Console.WriteLine("power set size after = " + powerSet.Size());
			Console.WriteLine("remove result = " + result);
			for (int i = 0; i < powerSet._values.Length; i++)
				Console.Write(powerSet._values[i] + " ");

			Console.WriteLine();
			Console.WriteLine("test remove 4");
			Console.WriteLine("power set size before  = " + powerSet.Size());
		  result = powerSet.Remove(4);
			Console.WriteLine("power set size after = " + powerSet.Size());
			Console.WriteLine("remove result = " + result);
			for (int i = 0; i < powerSet._values.Length; i++)
				Console.Write(powerSet._values[i] + " ");

			Console.WriteLine();
			Console.WriteLine("test remove 20");
			Console.WriteLine("power set size before  = " + powerSet.Size());
		  result = powerSet.Remove(20);
			Console.WriteLine("power set size after = " + powerSet.Size());
			Console.WriteLine("remove result = " + result);
			for (int i = 0; i < powerSet._values.Length; i++)
				Console.Write(powerSet._values[i] + " ");

			Console.WriteLine("test get");

			for (int i = 0; i < powerSet._values.Length; i++)
			{
				Console.WriteLine(i + " get result = " + powerSet.Get(i));
			}

			Console.WriteLine();
			Console.WriteLine(new string('=', 50));

		}

		static void TestFunc(PowerSet<int> set1, PowerSet<int> set2, Func<PowerSet<int>, PowerSet<int>, PowerSet<int>> testFunc)
		{
			if(set1.Size() == 0 )
				Console.Write("set1 is empty");
			else
			{
				Console.WriteLine("set1");
				for (int i = 0; i < set1._values.Length; i++)
				{
					if(set1._slotsStatus[i] == PowerSet<int>.SlotStatus.Fill)
						Console.Write(set1._values[i] + " ");
				}
			}

			Console.WriteLine();

			if(set2.Size() == 0)
				Console.Write("set2 is empty");
			else
			{
				Console.WriteLine("set2");
				for (int i = 0; i < set2._values.Length; i++)
				{
					if (set2._slotsStatus[i] == PowerSet<int>.SlotStatus.Fill)
						Console.Write(set2._values[i] + " ");
				}
			}

			Console.WriteLine();

			PowerSet<int> resultSet = testAction(set1, set2);

			if (resultSet.Size() == 0)
				Console.Write("result set is empty");
			else
			{
				Console.WriteLine("result set");
				for (int i = 0; i < resultSet._values.Length; i++)
				{
					if (resultSet._slotsStatus[i] == PowerSet<int>.SlotStatus.Fill)
						Console.Write(resultSet._values[i] + " ");
				}
			}

			Console.WriteLine();

			Console.WriteLine(new string('=', 50));
		}

		static void TestUnion(PowerSet<int> set1, PowerSet<int> set2)
		{

		}

		static PowerSet<int> CreatePowerSet(int startValue, int finishValue)
		{
			PowerSet<int> resultSet = new PowerSet<int>();
			for (int i = startValue; i <= finishValue; i++)
				resultSet.Put(i);

			return resultSet;
		}

		static PowerSet<int> CreateEmptyPowerSet()
		{
			return new PowerSet<int>();
		}
	}
}
