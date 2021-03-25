using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly ToDoContext _context;

        public ToDoListController(ToDoContext context)
        {
            _context = context;
        }

        // GET: ToDoList
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.ToDo.ToListAsync();

            return View(tasks);
        }

        // GET: ToDoList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.ToDo.FirstOrDefaultAsync(m => m.TaskId == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: ToDoList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoList/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoViewModel tasks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tasks);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(tasks);
        }

        // GET: tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.ToDo.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,Name,Category,Color,UnitPrice,AvailableQuantity")] ToDoViewModel updatedtaskDetails)
        {
            if (id != updatedtaskDetails.TaskId)
            {
                return NotFound();
            }

            var task = await _context.ToDo.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    task.Title = updatedtaskDetails.Title;
                    task.Completed = updatedtaskDetails.Completed;
                    task.UnitPrice = updatedtaskDetails.UnitPrice;
                    task.AvailableQuantity = updatedtaskDetails.AvailableQuantity;

                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tasksExists(updatedtaskDetails.TaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(updatedtaskDetails);
        }

        // GET: tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.ToDo.FirstOrDefaultAsync(m => m.TaskId == id);

            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.ToDo.FindAsync(id);

            _context.ToDo.Remove(task);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool tasksExists(long id)
        {
            return _context.ToDo.Any(e => e.TaskId == id);
        }
    }
}
