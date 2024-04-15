using MVC_Attendance.Models;

namespace MVC_Attendance.Repository
{
    public class IntakeRepository
    {
        AttDbContext db;
        public IntakeRepository(AttDbContext db)
        {
            this.db = db;
        }
        public List<Intake> GetAll()
        {
            return db.Intakes.ToList();
        }
        public Intake GetById(int id)
        {
            return db.Intakes.FirstOrDefault(intake => intake.Id == id);
        }
        public void Add(Intake intake)
        {
            db.Intakes.Add(intake);
            db.SaveChanges();
        }
        public void Update(int id, Intake intake)
        {
            db.Intakes.Update(intake);
            db.SaveChanges();

        }
        public void Delete(int id)
        {
            Intake intake = GetById(id);
            db.Intakes.Remove(intake);
            db.SaveChanges();
        }
    }
}
