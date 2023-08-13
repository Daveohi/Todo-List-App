using Todo_List_App.Controllers;

namespace Todo_List_App
{
    public class TodoItemRepository : Repository<TodoItem>
    {
        public TodoItemRepository(SessionFactory sessionFactory) : base(sessionFactory)
        {

        }
    }
}