using Microsoft.AspNetCore.Mvc;
using zad5.dal;
using zad5.Models;

namespace zad5.Controllers
{
    public class PersonController : Controller
    {

		private static readonly ICosmosDbService service = CosmosDbServiceProvider.Service!;

		public async Task<ActionResult> Index()
		{
			return View(await service.GetPersonsAsync("SELECT * FROM Person"));
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(Person person)
		{
			if (ModelState.IsValid)
			{
				person.Id = Guid.NewGuid().ToString();
				person.Type = nameof(Person);
                await service.AddPersonAsync(person);
				return RedirectToAction(nameof(Index));
			}
			return View(person);
		}

		public async Task<ActionResult> Edit(string id) => await ShowPerson(id);

		public async Task<ActionResult> Delete(string id) => await ShowPerson(id);

		public async Task<ActionResult> Details(string id) => await ShowPerson(id);

		private async Task<ActionResult> ShowPerson(string id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			var person = await service.GetPersonAsync(id);
			if (person == null)
			{
				return NotFound();
			}
			return View(person);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(Person person)
		{
			if (ModelState.IsValid)
			{
				person.Type = nameof(Person);
                await service.UpdatePersonAsync(person);
				return RedirectToAction(nameof(Index));
			}
			return View(person);
		}

		[HttpPost]
		public async Task<ActionResult> Delete(Person person)
		{
			await service.DeletePersonAsync(person);
			return RedirectToAction(nameof(Index));
		}

	}
}
