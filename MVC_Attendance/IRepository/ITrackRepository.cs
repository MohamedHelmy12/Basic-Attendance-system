namespace MVC_Attendance.IRepository
{
    public interface ITrackRepository
    {
        DateOnly GetTrackStartDate(int trackId, int intakeId);
    }
}
