using System.Web.Mvc;
using Practical_8.Data;
using Practical_8.Models;

namespace Practical_8.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryRepository repository =
            new InventoryRepository();

        public ActionResult Index()
        {
            return View(repository.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(int id)
        {
            Product product = repository.GetById(id);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Update(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Delete(int id)
        {
            Product product = repository.GetById(id);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}