using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;
using MVC_Attendance.Repository;
using MVC_Attendance.viewModels;

using System.Collections.Generic;

namespace MVC_Attendance.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly ISuperviseRepository _superviseRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IIntakeRepository _intakeRepository;

        public SupervisorController(ISuperviseRepository superviseRepository,
                                    IInstructorRepository instructorRepository,
                                    ITrackRepository trackRepository,
                                    IIntakeRepository intakeRepository)
        {
            _superviseRepository = superviseRepository;
            _instructorRepository = instructorRepository;
            _trackRepository = trackRepository;
            _intakeRepository = intakeRepository;
        }

        public IActionResult Index()
        {
           // List<Intake> intakes = _intakeRepository.GetAll();

            List<Track> tracks = _trackRepository.GetAll();
            List<Supervise> supervises = _superviseRepository.GetAll();
            
          //  ViewData["intakes"] = intakes;
            ViewData["tracks"] = tracks;
           


            return View(supervises);
        }


        public IActionResult Add()
        {
            var viewModel = new SupervisorAddViewModel
            {
                Instructors = _instructorRepository.GetAllInstructors(),
                Tracks = _trackRepository.GetAll(),
                Intakes = _intakeRepository.GetAll()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Add(int instructorId, int trackId)
        {
            // Check if instructor, track, and intake exist
            Instructor instructor = _instructorRepository.GetInstructorById(instructorId);
            Track track = _trackRepository.GetById(trackId);
           // Intake intake = _intakeRepository.GetById(intakeId);

            if (instructor == null || track == null )
            {
                return NotFound();
            }

            // Check if there is already a supervisor for the specified track in intake
            bool trackHasSupervisor = _superviseRepository.GetAll().Any(supervise => supervise.TrackId == trackId);
           // bool intakeHasSupervisor = _superviseRepository.GetAll().Any(supervise => supervise.IntakeId == intakeId);

            if (trackHasSupervisor )
            {
                ModelState.AddModelError("", "This track  already has a supervisor.");
                
                var viewModel = new SupervisorAddViewModel
                {
                    Instructors = _instructorRepository.GetAllInstructors(),
                    Tracks = _trackRepository.GetAll(),
                    Intakes = _intakeRepository.GetAll()
                };
                return View(viewModel);
            }

            Supervise supervise = new Supervise
            {
                InstructorId = instructorId,
                TrackId = trackId,
                IntakeId = 1
            };

            _superviseRepository.Add(supervise);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int trackId, int intakeId, int instructorId)
        {
            // Retrieve the supervision record by trackId, intakeId, and instructorId
            Supervise supervise = _superviseRepository.GetByTrackIntakeInstructor(trackId, 1, instructorId);
            if (supervise == null)
            {
                return NotFound(); 
            }

            var viewModel = new SupervisorEditViewModel
            {
                Supervise = supervise,
                Instructors = _instructorRepository.GetAllInstructors(),
                Tracks = _trackRepository.GetAll(),
                Intakes = _intakeRepository.GetAll()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int TrackId,  int InstructorId, int newInstructorId, int newTrackId)
        {
            

            Supervise supervise = new Supervise
            {
                InstructorId = newInstructorId,
                TrackId = newTrackId,
                IntakeId = 1 // Retain the existing intake id
            };

            // Update the supervision record
            _superviseRepository.Update( supervise);

            return RedirectToAction("Index");
        }





        public IActionResult Delete(int trackId, int intakeId, int instructorId)
        {
        

            _superviseRepository.Delete(trackId,1,instructorId);

            return RedirectToAction("Index");
        }

    }
}
