using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.Models;
using MVC_Attendance.Repository;



namespace MVC_Attendance.Controllers
{
    
    public class SupervisorController : Controller
    {
        private readonly ISuperviseRepository _superviseRepository;
        private readonly InstructorRepository _instructorRepository;
        private readonly ITrackRepository _trackRepository;

        public SupervisorController(ISuperviseRepository superviseRepository,
                                    InstructorRepository instructorRepository,
                                    ITrackRepository trackRepository)
        {
            _superviseRepository = superviseRepository;
            _instructorRepository = instructorRepository;
            _trackRepository = trackRepository;
        }

        // GET: Supervisor/Add
        public IActionResult Add()
        {

            List<Instructor> instructorss = _instructorRepository.GetAll();
            List<Track> trackss = _trackRepository.GetAll();



            return View(instructorss);
        }

        // POST: Supervisor/Add
        [HttpPost]
        public IActionResult Add(int instructorId, int trackId)
        {
            // Check if instructor and track exist
            Instructor instructor = _instructorRepository.GetById(instructorId);
            Track track = _trackRepository.GetById(trackId);

            if (instructor == null || track == null)
            {
                return NotFound();
            }


            Supervise supervise = new Supervise
            {
                InstructorId = instructorId,
                TrackId = trackId
            };


            _superviseRepository.Add(supervise);

            return RedirectToAction("Index", "Home");
        }
    }

  
}
