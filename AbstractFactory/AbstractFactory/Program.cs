using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Паттерн "Абстрактная фабрика" (Abstract Factory) предоставляет интерфейс для создания семейств
	 взаимосвязанных объектов с определенными интерфейсами без указания конкретных типов данных объектов.*/

namespace AbstractFactory
{
	class Program
	{
		static void Main(string[] args)
		{
			Transport car = new Transport(new CarFactory());
			car.Move();
			car.Rest();

			Transport airplane = new Transport(new AirplaneFactory());
			airplane.Move();
			airplane.Rest();

			Console.ReadLine();
		}
	}
	//абстрактный класс отдых
	abstract class Recreation
	{
		public abstract void Rest();
	}
	// абстрактный класс движение
	abstract class Movement
	{
		public abstract void Move();
	}


	class RestCar : Recreation
	{
		public override void Rest()
		{
			Console.WriteLine("Автомобиль в гараже ...");
		}
	}

	class RestAirplane : Recreation
	{
		public override void Rest()
		{
			Console.WriteLine("Самолёт в ангаре ...");
		}
	}
	// движение полета
	class FlyMovement : Movement
	{
		public override void Move()
		{
			Console.WriteLine("Самолёт в полёте");
		}
	}
	// движение езды
	class RideMovement : Movement
	{
		public override void Move()
		{
			Console.WriteLine("Автомобиль едет");
		}
	}
	// класс абстрактной фабрики
	abstract class TransportFactory
	{
		public abstract Movement CreateMovement();
		public abstract Recreation CreateRecreation();
	}
	// Фабрика создания автомобиля
	class CarFactory : TransportFactory
	{
		public override Movement CreateMovement()
		{
			return new RideMovement();
		}

		public override Recreation CreateRecreation()
		{
			return new RestCar();
		}
	}
	// Фабрика создания самолёта
	class AirplaneFactory : TransportFactory
	{
		public override Movement CreateMovement()
		{
			return new FlyMovement();
		}

		public override Recreation CreateRecreation()
		{
			return new RestAirplane();
		}
	}
	// Транспорт
	class Transport
	{
		private Movement movement;
		private Recreation recreation;
		public Transport(TransportFactory factory)
		{
			recreation = factory.CreateRecreation();
			movement = factory.CreateMovement();
		}
		public void Move()
		{
			movement.Move();
		}
		public void Rest()
		{
			recreation.Rest();
		}
	}
}
