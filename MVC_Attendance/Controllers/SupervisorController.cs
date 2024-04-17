/*using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.Models;
using MVC_Attendance.Repository;

namespace MVC_Attendance.Controllers
{
    public class SupervisorController : Controller
    {
        private readonly ISuperviseRepository _superviseRepository;
      //  private readonly IInstructorRepository _instructorRepository;
        private readonly ITrackRepository _trackRepository;

        public SupervisorController(ISuperviseRepository superviseRepository,
                                    ITrackRepository trackRepository)
        {
            _superviseRepository = superviseRepository;
           
            _trackRepository = trackRepository;
        }

        // GET: Supervisor/Add
        public IActionResult Add()
        {
            
            List<Instructor> instructors = _instructorRepository.GetAll();
            List<Track> tracks = _trackRepository.GetAll();

            
            var viewModel = new SupervisorAddViewModel
            {
                Instructors = instructors,
                Tracks = tracks
            };

            return View(viewModel);
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
*/