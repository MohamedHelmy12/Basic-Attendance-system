using Microsoft.AspNetCore.Mvc;
using MVC_Attendance.Models;
using MVC_Attendance.Repository;

namespace MVC_Attendance.Controllers
{
    public class TrackController : Controller
    {
        
        private readonly ITrackRepository trackRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ISuperviseRepository superviseRepository;

        public TrackController(ITrackRepository trackRepository, IStudentRepository studentRepository, ISuperviseRepository superviseRepository )
        {
            this.trackRepository = trackRepository;
            this.studentRepository = studentRepository;
            this.superviseRepository = superviseRepository;
        }
        public IActionResult Index()
        {  
            return View();
        }

        public IActionResult show()
        {
            List<Track> tracks = trackRepository.GetAll();
            List<Supervise> supervises = superviseRepository.GetAll();
            ViewData["Supervises"] = supervises;


            return View(tracks);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(new Track());
        }
        [HttpPost]
        public IActionResult create(Track track)
        {
            if (ModelState.IsValid)
            {
                trackRepository.Add(track);
                return RedirectToAction("show");
            }
            else
            {
                
                return View(track);
            }
          
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Track track = trackRepository.GetById(id);
         
            return View(track);
        }
        [HttpPost]
        public IActionResult Edit(int id,Track track)
        {
            if(ModelState.IsValid)
            {
                trackRepository.Update(id, track);

                return RedirectToAction("show");
            }
            else
            {
                return View(track);
            }
           
        }
        public IActionResult Delete(int id)
        {
            trackRepository.Delete(id);
           
            return RedirectToAction("show");
        }

        public IActionResult ShowStudents(int id)
        {
           
            Track track = trackRepository.GetById(id);

            
            List<Student> studentsInTrack = studentRepository.GetStudentsByTrackId(id);

            return View(studentsInTrack);
        }
    }
}
