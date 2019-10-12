using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* Одиночка (Singleton, Синглтон) - порождающий паттерн, который гарантирует, что для определенного класса 
    будет создан только один объект, а также предоставит к этому объекту точку доступа. */

namespace Singleton
{
	class Program
	{
		static void Main(string[] args)
		{
			Human human = new Human();

			human.genderName("male");
			Console.WriteLine(human.gender.Name);

			// Не получится изменить пол, так как объект уже создан    
			human.gender = Gender.setGeenderName("female");
			Console.WriteLine(human.gender.Name);

			Console.ReadLine();
		}
	}
	class Human
	{
		public Gender gender { get; set; }
		public void genderName(string name)
		{
			gender = Gender.setGeenderName(name);
		}
	}
	class Gender
	{
		private static Gender instance;

		public string Name { get; private set; }

		private Gender(string name)
		{
			this.Name = name;
		}

		public static Gender setGeenderName(string name)
		{
			if (instance == null)
				instance = new Gender(name);
			return instance;
		}
	}
}
