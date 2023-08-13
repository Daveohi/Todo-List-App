using Microsoft.AspNetCore.Mvc;
using Todo_List_App;
using Todo_List_App.Controllers;

namespace Todo_List_AppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private TodoItemRepository _todoItemRepository;

        public TodoItemsController(TodoItemRepository todoItemRepository)
        {
            //_logger = logger;
            _todoItemRepository = todoItemRepository;
        }

        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            var todoItems = _todoItemRepository.GetAll();
            //return DbContext.TodoItems.ToList();
            return todoItems;
        }

        [HttpPost]
        public ActionResult<TodoItem> AddTodoItem(TodoItem todoItem) 
        {
            var myTodoItem = new TodoItem();
            {
                //Id = todoItem.Id
                //Task = todoItem.Task
                //IsCompleted = todoItem.IsCompleted,
                //Priority = todoItem.Priority
            };

            _todoItemRepository.Add(myTodoItem);
            return CreatedAtAction(nameof(GetTodoItems), new {id = todoItem.Id}, todoItem);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(int id, string Task) 
        {
            var todoItem = _todoItemRepository.GetById(id);
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            //context.Entry(todoItem).State = EntityState.Modified;
            todoItem.Task = Task;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItemById(int id)
        {
            //var todoItem = NHibernate.Context.TodoItems.Find(id);
            var todoItem = _todoItemRepository.GetById(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _todoItemRepository.DeleteById(id);
            return Ok(todoItem.Id);

        }

        private readonly TodoItemRepository todoItemRepository;
    }
}