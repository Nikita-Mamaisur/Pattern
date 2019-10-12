using System;

 /*Посредник (Mediator) — это поведенческий паттерн, который позволяет организовать работу множества
   слабо связанных объектов без непосредственного общения между ними. То есть, Посредник выступает
   промежуточным звеном между объектами, принимая и перенаправляя сообщения.*/

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int random = rnd.Next();

            ManagerMediator mediator = new ManagerMediator();
            Colleague executor = new ExecutorColleague(mediator);  //исполнитель
            Colleague representative = new RepresentativeColleague(mediator);  //представитель
            mediator.Executor = executor;
            mediator.Representative = representative;
            executor.Send("Пакеты готовы, можно отдавать заказчику");
            representative.Send($"Пакеты готовы, ревизия: No'{random}'");

            Console.ReadKey();
        }
    }

    abstract class Mediator
    {
        public abstract void Send(string msg, Colleague colleague);
    }
    abstract class Colleague
    {
        protected Mediator mediator;

        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }

        public virtual void Send(string message)
        {
            mediator.Send(message, this);
        }
        public abstract void Notify(string message);
    }

    // класс исполнителя
    class ExecutorColleague : Colleague
    {
        public ExecutorColleague(Mediator mediator)
            : base(mediator)
        { }

        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение представителю: " + message);
        }
    }
    // класс представителя
    class RepresentativeColleague : Colleague
    {
        public RepresentativeColleague(Mediator mediator)
            : base(mediator)
        { }

        public override void Notify(string message)
        {
            Console.WriteLine("Сообщение заказчику: " + message);
        }
    }

    class ManagerMediator : Mediator
    {
        public Colleague Executor { get; set; }
        public Colleague Representative { get; set; }
        public override void Send(string msg, Colleague colleague)
        {
               /* если отправитель - исполнитель, значит продукт готов
               отправляем сообщение представителю*/
            if (Executor == colleague)
                Executor.Notify(msg);
               /* если отправитель - представитель,
               отправляем сообщение заказчику*/
            else if (Representative == colleague)
                Representative.Notify(msg);
        }
    }
}
/*Участники

    Mediator: представляет интерфейс для взаимодействия с объектами Colleague

    Colleague: представляет интерфейс для взаимодействия с объектом Mediator

    ExecutorColleague и RepresentativeColleague: конкретные классы коллег, которые обмениваются друг с другом
    через объект Mediator

    ConcreteMediator: конкретный посредник, реализующий интерфейс типа Mediator
*/


/*
  Устраняется сильная связанность между объектами Colleague

  Упрощается взаимодействие между объектами: вместо связей по типу "все-ко-всем" применяется связь "один-ко-всем"

  Взаимодействие между объектами абстрагируется и выносится в отдельный интерфейс

  Централизуется управления отношениями между объектами
*/
