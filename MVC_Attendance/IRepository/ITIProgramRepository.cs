using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public class ITIProgramRepository
    {
        AttDbContext db;
        public ITIProgramRepository(AttDbContext db)
        {
            this.db = db;
        }
        public List<ITIProgram> GetAll()
        {
            return db.ITIPrograms.ToList();
        }
        public ITIProgram GetById(int id)
        {
            return db.ITIPrograms.FirstOrDefault(track => track.Id == id);
        }
        public void Add(ITIProgram program)
        {
            db.ITIPrograms.Add(program);
            db.SaveChanges();
        }
        public void Update(int id, ITIProgram program)
        {
            db.ITIPrograms.Update(program);
            db.SaveChanges();

        }
        public void Delete(int id)
        {
            ITIProgram program = GetById(id);
            db.ITIPrograms.Remove(program);
            db.SaveChanges();
        }
    }
}
