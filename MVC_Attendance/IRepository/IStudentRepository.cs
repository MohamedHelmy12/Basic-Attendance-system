﻿using MVC_Attendance.Models;

namespace MVC_Attendance.IRepository
{
    public interface IStudentRepository
    {
        StdIntakeTrack GetStdIntakeTrack(int studentId);
    }
}