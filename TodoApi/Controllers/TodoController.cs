using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public TodoController(TodoContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;

            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetItems()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,"http://jplsjpls-testjp.apps.us-east-2.online-starter.openshift.com/api/todo");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            var itens = new List<TodoItem>();

            if (response.IsSuccessStatusCode)
            {
                itens = await response.Content
                    .ReadAsAsync<List<TodoItem>>();
            }
            else
            {
                itens = new List<TodoItem>();
            }

            return itens;

        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {

            var itens = new List<TodoItem>();
            var item1 = new TodoItem();
            item1.Id = 01;
            item1.Name = "Criar web API";
            item1.IsComplete = false;

            var item2 = new TodoItem();
            item2.Id = 02;
            item2.Name = "Hospedar em algum serviço";
            item2.IsComplete = false;

            itens.Add(item1);
            itens.Add(item2);

            return itens;
        }

        // GET: api/Todo/5
/*        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }*/


        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
       /* [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }*/

        // PUT: api/Todo/5
       /* [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
       /* [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/
    }
}
