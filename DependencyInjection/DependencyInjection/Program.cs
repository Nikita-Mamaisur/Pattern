/*
  Инверсия управления — один из популярных принципов объектно-ориентированного программирования,
  при помощи которого можно снизить связанность между компонентами, а так же повысить модульность и
  расширяемость ПО.

    Реализуется инверсия управления несколькими способами, среди которых есть внедрение зависимостей
    (Dependency Injection, DI)

    Пример кода без применения DI

    class TodoRepository {
        // other code
    }

    class TodoService {
        TodoRepository todoRepository = new TodoRepository();
        // other code
    }

    Kласс TodoService самостоятельно создаёт объект класса TodoRepository.
    Это образует сильную связь между классами TodoService и TodoRepository.
    Если в классе TodoService потребуется использование другого класса вместо TodoRepository,
    то придётся вносить соответствующие изменения в него. Даже если из TodoRepository выделить интерфейс, 
    связанность между классами слабее не станет, так как TodoService самостоятельно создаёт объект TodoRepository.

    Применение инверсии управления предполагает, что объект класса TodoRepository должен быть создан за
    пределами классаTodoService, но в дальнейшем должен быть передан объекту этого класса.
     */

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            //Пример внедрения зависимости через конструктор:
            TodoRepository todoRepository = new TodoRepository();
            TodoService todoService = new TodoService(todoRepository);
            System.Console.ReadKey();

            //Пример внедрения зависимости через set-метод:
            //TodoRepository todoRepository = new TodoRepository();
            //TodoService todoService = new TodoService();
            //todoService.setTodoRepository(todoRepository);
        }
    }
    //Пример внедрения зависимости через конструктор:
    class TodoService
    {
        TodoRepository todoRepository;
        public TodoService(TodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }
    }
    class TodoRepository
    {
        // other code
    }

    /*Пример внедрения зависимости через set-метод:
        class TodoService
    {
        TodoRepository todoRepository;
        public void setTodoRepository(TodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }
    }*/
}

