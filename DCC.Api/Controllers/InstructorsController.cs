﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DCC.Domain.Services;
using DCC.Domain.DTO;
using AutoMapper;
using DCC.Data.Models;
using DCC.Domain.Models;

namespace DCC.Api.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IInstructorsService _instructorsService;
        public InstructorsController(IInstructorsService instructorsService, IMapper mapper)
        {
            _mapper = mapper;
            _instructorsService = instructorsService;
        }

        // GET: Instructors
        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorsService.GetAllInstructorsAsync();
            var model = _mapper.Map<IEnumerable<InstructorDTO>>(instructors);
            return View(model);
        }

        // GET: Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,JobTitle,Image,IsDeleted")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                await _instructorsService.AddInstructorAsync(new InstructorRequest
                {
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Image = instructor.Image
                });

                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        //// GET: Instructors/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var instructor = await _context.Instructors.FindAsync(id);
        //    if (instructor == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(instructor);
        //}

        //// POST: Instructors/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,JobTitle,Image,AverageRating,AggregateRatings,NumberOfRatings,IsDeleted")] Instructor instructor)
        //{
        //    if (id != instructor.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(instructor);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!InstructorExists(instructor.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(instructor);
        //}

        //// GET: Instructors/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var instructor = await _context.Instructors
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (instructor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(instructor);
        //}

        //// POST: Instructors/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var instructor = await _context.Instructors.FindAsync(id);
        //    _context.Instructors.Remove(instructor);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool InstructorExists(int id)
        //{
        //    return _context.Instructors.Any(e => e.Id == id);
        //}
    }
}
