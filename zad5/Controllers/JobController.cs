using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using zad5.dal;
using zad5.Models;

namespace zad5.Controllers
{
    public class JobController : Controller
    {



        private static readonly ICosmosDbService service = CosmosDbServiceProvider.Service!;
		private static List<Person>? persons;

        public async Task<ActionResult> Index()
		{
            persons = (List<Person>)await service.GetPersonsAsync("SELECT * FROM Person");
            ViewBag.Persons = persons;

            List<Job> jobs = (List<Job>)await service.GetJobsAsync("SELECT * FROM Job");

			jobs.ForEach(j =>
            {
                j.PersonFullName = persons.FirstOrDefault(p => p.Id == j.PersonId)?.ToString();
            });


            return View(jobs);
		}

		public async Task<ActionResult> CreateAsync()
		{
            ViewBag.Persons = new SelectList(persons, "Id", "Name");
            return View();
		}

		[HttpPost]
		public async Task<ActionResult> Create(Job job)
		{			
            if (ModelState.IsValid)
			{
				job.Id = Guid.NewGuid().ToString();
                job.Type = nameof(Job);
                await service.AddJobAsync(job);
				return RedirectToAction(nameof(Index));
			}
			return View(job);
		}



		public async Task<ActionResult> Edit(string id) => await ShowJob(id);

		public async Task<ActionResult> Delete(string id) => await ShowJob(id);

		public async Task<ActionResult> Details(string id) => await ShowJob(id);

		private async Task<ActionResult> ShowJob(string id)
		{
            ViewBag.Persons = new SelectList(persons, "Id", "Name");
            if (id == null)
			{
				return BadRequest();
			}
			var job = await service.GetJobAsync(id);
			if (job == null)
			{
				return NotFound();
			}
            job.PersonFullName = persons.FirstOrDefault(p => p.Id == job.PersonId)?.ToString();

            return View(job);
		}

		[HttpPost]
		public async Task<ActionResult> Edit(Job job)
		{
			if (ModelState.IsValid)
			{
                job.Type = nameof(Job);
                await service.UpdateJobAsync(job);
				return RedirectToAction(nameof(Index));
			}
			return View(job);
		}

		[HttpPost]
		public async Task<ActionResult> Delete(Job job)
		{
			await service.DeleteJobAsync(job);
			return RedirectToAction(nameof(Index));
		}

	}
}
