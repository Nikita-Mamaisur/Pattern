using System;
using System.Collections.Generic;


	/*	Паттерн "Наблюдатель" (Observer) представляет поведенческий шаблон проектирования, который 
	использует отношение "один ко многим". В этом отношении есть один наблюдаемый объект и множество
	наблюдателей. И при изменении наблюдаемого объекта автоматически происходит оповещение всех наблюдателей.*/


namespace Observer
{
	class Program
	{
		static void Main(string[] args)
		{
			Bargaining bargaining = new Bargaining();  
			Еmployee employee = new Еmployee("Никита Мамайсур", bargaining);
			Guest guest = new Guest("Александр Шевчук", bargaining);
			// имитация торгов
			bargaining.NextLot();
			// брокер прекращает наблюдать за торгами
			guest.StopTrade();
			// имитация торгов
			bargaining.NextLot();

			Console.Read();
		}
	}

	interface IObserver  //наблюдатель .
	{
		void Update(Object ob); // вызывается наблюдаемым объектом для уведомления наблюдателя.
	}

	interface IObservable // наблюдаемый объект.
	{
		void AddObserver(IObserver o); // добавление наблюдателя
		void RemoveObserver(IObserver o); // удаление набюдателя
		void NotifyObservers(); // уведомление наблюдателей
	}

	class Bargaining : IObservable // Реалезация IObservable (наблюдаемый объект)
	{
		readonly BindInformation sInfo; // информация о торгах

		List<IObserver> observers; // коллекция наблюдателей
		public Bargaining()  // ctor
		{
			observers = new List<IObserver>(); // инициализация объектов
			sInfo = new BindInformation();
		}
		public void AddObserver(IObserver o) 
		{
			observers.Add(o);
		}

		public void RemoveObserver(IObserver o)
		{
			observers.Remove(o);
		}

		public void NotifyObservers()
		{
			foreach (IObserver o in observers)
			{
				o.Update(sInfo);
			}
		}

		public void NextLot()
		{
			Random rnd = new Random();
			sInfo.LotPrice = rnd.Next(100, 400);
			NotifyObservers();
		}
	}

	class BindInformation
	{
		public int LotPrice { get; set; }
	}

	class Guest : IObserver  // Наблюдатель
	{
		public string Name { get; set; }
		IObservable stock;
		public Guest(string name, IObservable obs)
		{
			this.Name = name;
			stock = obs;
			stock.AddObserver(this);
		}
		public void Update(object ob)
		{
			BindInformation sInfo = (BindInformation)ob;

			if (sInfo.LotPrice > 200)
				Console.WriteLine($"Гость {this.Name} заберает лот по цене: {sInfo.LotPrice}$");
			else
				Console.WriteLine($"Гость {this.Name} не интересуется лотом по цене: {sInfo.LotPrice}$");
		}
		public void StopTrade()
		{
			stock.RemoveObserver(this);
			stock = null;
			Console.WriteLine($"Гость {this.Name} заканчивает своё участие в аукционе.");
		}
	}

	class Еmployee : IObserver  // Наблюдатель
	{
		public string Name { get; set; }

		readonly IObservable stock;
		public Еmployee(string name, IObservable obs)
		{
			this.Name = name;
			stock = obs;
			stock.AddObserver(this);
		}
		public void Update(object ob)
		{
			BindInformation sInfo = (BindInformation)ob;

			if (sInfo.LotPrice < 200)
				Console.WriteLine($"Сотрудник {this.Name} забирает лот по цене: {sInfo.LotPrice}$");
			else
				Console.WriteLine($"Сотрудник {this.Name} отказывается от лота с ценой: {sInfo.LotPrice}$");
		}
	}
}
