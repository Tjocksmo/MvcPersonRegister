using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcPersonRegister.Data;
using MvcPersonRegister.Models;
using MvcPersonRegister.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPersonRegister.Controllers
{
    public class PersonController : Controller
    {
        private DataDbContext _context;
        public PersonController(DataDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dtoList = await _context.Set<Person>().Select(x => PersonDto.FromDomain(x)).ToListAsync();
            return View(dtoList);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var person = await _context.FindAsync<Person>(id);

            if (person is null) return NotFound();

            return View(person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(PersonDto person)
        {
            if(ModelState.IsValid)
            {
                _context.Add(person.ToDomain());
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult>Delete(int id)
        {
            var person = await _context.FindAsync<Person>(id);
            if(person is null)
            {
                return NotFound();
            }
            return View(PersonDto.FromDomain(person));
        }

        [HttpPost, ActionName(nameof(Delete))]
        public async Task<IActionResult>ConfirmDelete(int id)
        {
            var person = await _context.FindAsync<Person>(id);
            _context.Set<Person>().Remove(person);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _context.FindAsync<Person>(id);
            if (person is null) return NotFound();

            return View(PersonDto.FromDomain(person));
        }        

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PersonDto personDto)
        {
            if (ModelState.IsValid)
            {
                var person = await _context.FindAsync<Person>(id);
                person.FirstName = personDto.FirstName;
                person.SecondName = personDto.SecondName;
                person.Email = personDto.Email;
                person.SocialSecNr = personDto.SocialSecurityNr;

                _context.Update(person);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(personDto);
        }
    }
}
