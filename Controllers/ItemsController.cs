using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MyDbContext dbContext;
        public ItemsController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET: ItemsController
        public async Task<ActionResult> Index()
        {
            var items = await dbContext.Items.ToListAsync();
            return View(items);
        }

        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Insert([Bind("Id", "Name", "Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                dbContext.Items.Add(item);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public IActionResult Edit(int Id)
        {
            var item = dbContext.Items.SingleOrDefault(x => x.Id == Id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, [Bind("Id", "Name", "Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                dbContext.Items.Update(item);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public IActionResult Delete(int Id)
        {
            var item = dbContext.Items.Find(Id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int Id)
        {
            var item = await dbContext.Items.FindAsync(Id);
            if (item != null)
            {
                dbContext.Items.Remove(item);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
