using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public interface ISuperviseRepository
    {
        List<Supervise> GetAll();

        public Supervise GetByTrackIntakeInstructor(int trackId, int intakeId, int instructorId);
        

            void Add(Supervise supervise );

        void Update(Supervise supervise);

        public void Delete(int trackId, int intakeId, int instructorId);



    }
}
