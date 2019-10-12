using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    /*Паттерн Адаптер (Adapter) предназначен для преобразования интерфейса одного класса в интерфейс другого.
    Благодаря реализации данного паттерна мы можем использовать вместе классы с несовместимыми интерфейсами.*/

namespace Adapter
{
	class Program
	{
        static void Main()
        {
            // Create adapter and call a request
            Target target = new Target();
            target.Request();

            // add Adapter and call SpecificRequest
            target = new Adapter();
            target.Request();

            // Wait for user
            Console.ReadKey();
        }
    }
    class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("Called Target Request()");
        }
    }

    // "Adapter"

    class Adapter : Target
    {
        private Adaptee adaptee = new Adaptee();

        public override void Request()
        {
            // Possibly do some other work
            // and then call SpecificRequest
            adaptee.SpecificRequest();
        }
    }

    // "Adaptee"
    class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Called SpecificRequest()");
        }
    }
}
