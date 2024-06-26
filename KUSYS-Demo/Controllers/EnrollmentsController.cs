﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo.Models;
using KUSYS_Demo.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using KUSYS_Demo.Repositories.Implementation;

namespace KUSYS_Demo.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly DatabaseContext _context;
        public string userMailAddress;
        public string userRole;

        public EnrollmentsController(DatabaseContext context)
        {
            _context = context;
            userMailAddress = StaticDatas.getUserMail();
            userRole = StaticDatas.getUserRole();
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var enrollment = _context.Enrollment.Include(t => t.Course).Include(t => t.Student);

            if (userRole == "admin")
            {
                return View(enrollment.ToList());
            }
            if (userRole == "user")
            {
                enrollment = _context.Enrollment.Where(x => x.Student.Email == userMailAddress).Include(t => t.Course).Include(t => t.Student);
                if (enrollment == null)
                {
                    return NotFound();
                }
                return View(await enrollment.ToListAsync());
            }
            return View(await enrollment.ToListAsync());

        }

        //GET: Enrollments/Create
        public IActionResult Create(EnrollmentViewModel enrollmentModel)
        {
            if (userRole == "admin")
            {
                enrollmentModel.StudentList = new List<SelectListItem>();
                var students = _context.Student.ToList();
                foreach (var item in students)
                {
                    enrollmentModel.StudentList.Add(new SelectListItem
                    {
                        Text = item.FirstName + " " + item.LastName,
                        Value = Convert.ToString(item.Id)
                    });
                }
            }
            else if (userRole == "user")
            {
                var students = _context.Student.Where(x => x.Email == userMailAddress).ToList();
                if (students == null)
                {
                    return RedirectToAction(nameof(Index));

                }
                enrollmentModel.StudentList = new List<SelectListItem>();
                foreach (var item in students)
                {
                    enrollmentModel.StudentList.Add(new SelectListItem
                    {
                        Text = item.FirstName + " " + item.LastName,
                        Value = Convert.ToString(item.Id)
                    });
                }



            }
            else
            {
                return RedirectToAction(nameof(Index));

            }
            enrollmentModel.CourseList = new List<SelectListItem>();
            var courses = _context.Course.ToList();
            foreach (var item in courses)
            {
                enrollmentModel.CourseList.Add(new SelectListItem
                {
                    Text = item.Code + " " + item.Title,
                    Value = Convert.ToString(item.Id)
                });
            }



            return View(enrollmentModel);
        }
        public JsonResult CourseList(int id)
        {
            var courseIds = _context.Enrollment.Where(x => x.Student.Id == Convert.ToInt32(3)).Select(x => x.Course.Id).ToList();
            var courseList = _context.Course.ToList();
            var notSelectedCourses = from c in courseList where !courseIds.Any(o => o == c.Id) select c;
            var notSelectedCoursesList = notSelectedCourses.ToList();

            return Json(new SelectList(notSelectedCoursesList, "Id", "Title"));
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,CourseId")] EnrollmentViewModel enrollmentModel, string value)
        {

            var enrollment = new Enrollment();
            var student = _context.Student.Where(x => x.Id == enrollmentModel.StudentId).SingleOrDefault();
            var course = _context.Course.Where(x => x.Id == enrollmentModel.CourseId).SingleOrDefault();

            var matching = _context.Enrollment.Where(x => x.Student.Id == enrollmentModel.StudentId & x.Course.Id == enrollmentModel.CourseId);

            if (!matching.IsNullOrEmpty())
            {
                return RedirectToAction(nameof(Index));
            }
            enrollment.Student = student;
            enrollment.Course = course;
            _context.Add(enrollment);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollment == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollment == null)
            {
                return Problem("Entity set 'DatabaseContext.Enrollment'  is null.");
            }
            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollment.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.Id == id);
        }
    }
}
