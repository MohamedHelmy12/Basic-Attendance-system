using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class SuperviseRepository : ISuperviseRepository
    {
        AttDbContext db;

        public SuperviseRepository(AttDbContext db)
        {
            this.db = db;
        }
        public List<Supervise> GetAll()
        {
            return db.Supervises.ToList();
        }
        public Supervise GetById(int id)
        {
            return db.Supervises.FirstOrDefault(supervise => supervise.InstructorId == id);
        }
        public void Add(Supervise supervise )
        {
            db.Supervises.Add(supervise);
            db.SaveChanges();
        }
        public void Update(int id, Supervise supervise)
        {
            db.Supervises.Update(supervise);
            db.SaveChanges();

        }
        public void Delete(int id)
        {
            Supervise supervise  = GetById(id);
            db.Supervises.Remove(supervise);
            db.SaveChanges();
        }

       
       
    }
}
