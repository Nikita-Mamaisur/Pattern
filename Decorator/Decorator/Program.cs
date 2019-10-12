using System;

namespace Decorator

  /* Декоратор (Decorator) представляет структурный шаблон проектирования, который позволяет динамически 
     подключать к объекту дополнительную функциональность.

     Для определения нового функционала в классах нередко используется наследование. Декоратор же предоставляет 
     наследованию более гибкую альтернативу, поскольку позволяe динамически в процессе выполнения определять
     новые возможности у объектов.*/
{
	class Program
	{
		static void Main(string[] args)
		{
			GaStation order001 = new NinetyFifth();
			order001 = new GlassCleaning(order001); // 95й с чисткой стекла
			Console.WriteLine("Название: {0}", order001.Name);
			Console.WriteLine("Цена: {0}", order001.GetCost());

			GaStation order002 = new NinetyFifth();
			order002 = new InteriorCleaning(order002);// 95й с чисткой салона
			Console.WriteLine("Название: {0}", order002.Name);
			Console.WriteLine("Цена: {0}", order002.GetCost());

			GaStation order003 = new NinetyEighth();
			order003 = new GlassCleaning(order003);
			order003 = new InteriorCleaning(order003);// 98й с чисткой стекла и салона
			Console.WriteLine("Название: {0}", order003.Name);
			Console.WriteLine("Цена: {0}", order003.GetCost());

			Console.ReadLine();
		}
	}

	abstract class GaStation
	{
		public GaStation(string n)
		{
			this.Name = n;
		}
		public string Name { get; protected set; }
		public abstract int GetCost();
	}

	class NinetyFifth : GaStation
	{
		public NinetyFifth() 
			: base("АИ-95") 
		{ }
		public override int GetCost()
		{
			return 6000;  // "руб/100л"
		}
	}
	class NinetyEighth : GaStation
	{
		public NinetyEighth()
			: base("АИ-98")  
		{ }
		public override int GetCost()
		{
			return 7500;  // "руб/100л"
		}
	}


    /* Декоратором является абстрактный класс GaStationDecorator, который унаследован от класса GaStation и 
       содержит ссылку на декорируемый объект GaStation.В отличие от формальной схемы здесь установка 
       декорируемого объекта происходит не в методе SetComponent, а в конструкторе.*/

    abstract class GaStationDecorator : GaStation
	{
		protected GaStation fuel;
		public GaStationDecorator(string n, GaStation fuel) : base(n)
		{
			this.fuel = fuel;
		}
	}

	class GlassCleaning : GaStationDecorator
	{
		public GlassCleaning(GaStation p)
			: base(p.Name + ", очитска стекла", p)
		{ }

		public override int GetCost()
		{
			return fuel.GetCost() + 200;
		}
	}

	class InteriorCleaning : GaStationDecorator
	{
		public InteriorCleaning(GaStation p)
			: base(p.Name + ", чистка салона", p)
		{ }

		public override int GetCost()
		{
			return fuel.GetCost() + 500;
		}
	}
}
