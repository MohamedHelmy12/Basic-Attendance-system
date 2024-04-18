using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.IRepository;
using MVC_Attendance.Models;
using MVC_Attendance.Repository;
using MVC_Attendance.viewModels;



namespace MVC_Attendance.Controllers
{

    public class SupervisorController : Controller
    {
        private readonly ISuperviseRepository _superviseRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IIntakeRepository intake;
       // private readonly IIntakesTracksRepository intakesTracksRepository;




        public SupervisorController(ISuperviseRepository superviseRepository,
                                    IInstructorRepository instructorRepository,
                                    ITrackRepository trackRepository,
                                    IIntakeRepository intake
                                    )
        {
            _superviseRepository = superviseRepository;
            _instructorRepository = instructorRepository;
            _trackRepository = trackRepository;
            this.intake = intake;
           // this.intakesTracksRepository = intakesTracksRepository;
        }

        // GET: Supervisor/Add

        public IActionResult Add()
        {
            List<Instructor> instructors = _instructorRepository.GetAllInstructors();

            // Filter tracks and intakes that don't have supervisors assigned

            List<Track> tracks = _trackRepository
            .GetAll();


            List<Intake> intakes = intake
            .GetAll();



            var viewModel = new SupervisorAddViewModel
            {
                Instructors = instructors,
                Tracks = tracks,
                Intakes = intakes
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Add(int instructorId, int trackId, int intakeId)
        {
            // Check if instructor, track, and intake exist
            Instructor instructor = _instructorRepository.GetInstructorById(instructorId);
            Track track = _trackRepository.GetById(trackId);
            Intake intakes = intake.GetById(intakeId);

            if (instructor == null || track == null || intake == null)
            {
                return NotFound();
            }

            // Check if there is already a supervisor for the specified track or intake
            bool trackHasSupervisor = _superviseRepository.GetAll().Any(supervise => supervise.TrackId == trackId);
            bool intakeHasSupervisor = _superviseRepository.GetAll().Any(supervise => supervise.IntakeId == intakeId);

            if (trackHasSupervisor && intakeHasSupervisor)
            {
                ModelState.AddModelError("", "This track in this intake already has a supervisor.");
                // Repopulate dropdowns and return to the view
                var viewModel = new SupervisorAddViewModel
                {
                    Instructors = _instructorRepository.GetAllInstructors(),
                    Tracks = _trackRepository.GetAll(),
                    Intakes = intake.GetAll()
                };
                return View(viewModel);
            }

            Supervise supervise = new Supervise
            {
                InstructorId = instructorId,
                TrackId = trackId,
                IntakeId = intakeId
            };

            _superviseRepository.Add(supervise);

            return RedirectToAction("Index", "Home");
        }




    }


}
