using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MVC_Attendance.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ITIPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITIPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Intakes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intakes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intakes_ITIPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ITIPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartPeriod = table.Column<TimeOnly>(type: "time", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EmployeeType = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UniversityID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GraduationYear = table.Column<int>(type: "int", nullable: true),
                    AbsenceDegree = table.Column<double>(type: "float", nullable: false),
                    NumberOfAbsences = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntakesTracks",
                columns: table => new
                {
                    IntakeId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntakesTracks", x => new { x.IntakeId, x.TrackId });
                    table.ForeignKey(
                        name: "FK_IntakesTracks_Intakes_IntakeId",
                        column: x => x.IntakeId,
                        principalTable: "Intakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntakesTracks_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    AttendanceTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    LeavingTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supervises",
                columns: table => new
                {
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    IntakeId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervises", x => new { x.TrackId, x.IntakeId, x.InstructorId });
                    table.ForeignKey(
                        name: "FK_Supervises_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supervises_Intakes_IntakeId",
                        column: x => x.IntakeId,
                        principalTable: "Intakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supervises_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => new { x.StudentId, x.date });
                    table.ForeignKey(
                        name: "FK_Permissions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StdIntakeTrack",
                columns: table => new
                {
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    IntakeId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StdIntakeTrack", x => new { x.IntakeId, x.TrackId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StdIntakeTrack_Intakes_IntakeId",
                        column: x => x.IntakeId,
                        principalTable: "Intakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StdIntakeTrack_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StdIntakeTrack_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ITIPrograms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Professional Training Program" },
                    { 2, "Intensive Training Program" },
                    { 3, "Summer Training Program" }
                });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Professional Web Development and BI" },
                    { 2, "Open Source" },
                    { 3, "Artificial Intelegence" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "Password", "Phone", "Role" },
                values: new object[,]
                {
                    { 1, null, "Admin@admin.com", "Admin", "Admin", "Admin@123", "01111111111", 3 },
                    { 2, "Kafr El Shi5, Egypt", "Ali@gmail.com", "Ali", "Ali2", "Ali@123", "01111111111", 0 },
                    { 3, "Tanta, Egypt", "Ahmed@gmail.com", "Ahmed", "Ahmed2", "Ahmed@123", "01111111111", 0 },
                    { 4, null, "Nadya@gmail.com", "Nadya", "Saleh", "Nadya@123", "01111111111", 1 },
                    { 5, null, "Ayman@gmail.com", "Ayman", "Lotfy", "Ayman@123", "01111111111", 1 },
                    { 6, null, "Mahmoud@gmail.com", "Mahmoud", "Mahmoud", "Mahmoud@123", "01111111111", 2 },
                    { 7, null, "Ashraf@gmail.com", "Ashraf", "Ashraf2", "admin@123", "01111111111", 2 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EmployeeType", "HireDate", "Salary" },
                values: new object[,]
                {
                    { 6, 0, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10000.0 },
                    { 7, 1, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10000.0 }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                column: "Id",
                values: new object[]
                {
                    4,
                    5
                });

            migrationBuilder.InsertData(
                table: "Intakes",
                columns: new[] { "Id", "Name", "ProgramId" },
                values: new object[] { 1, "44", 1 });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "Date", "StartPeriod", "TrackId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 4, 1), new TimeOnly(9, 0, 0), 1 },
                    { 2, new DateOnly(2024, 4, 2), new TimeOnly(9, 0, 0), 1 },
                    { 3, new DateOnly(2024, 4, 3), new TimeOnly(9, 0, 0), 1 },
                    { 4, new DateOnly(2024, 4, 4), new TimeOnly(9, 0, 0), 1 },
                    { 5, new DateOnly(2024, 4, 5), new TimeOnly(9, 0, 0), 1 },
                    { 6, new DateOnly(2024, 4, 6), new TimeOnly(9, 0, 0), 1 },
                    { 7, new DateOnly(2024, 4, 7), new TimeOnly(9, 0, 0), 1 },
                    { 8, new DateOnly(2024, 4, 8), new TimeOnly(9, 0, 0), 1 },
                    { 9, new DateOnly(2024, 4, 9), new TimeOnly(9, 0, 0), 1 },
                    { 10, new DateOnly(2024, 4, 10), new TimeOnly(9, 0, 0), 1 },
                    { 11, new DateOnly(2024, 4, 11), new TimeOnly(9, 0, 0), 1 },
                    { 12, new DateOnly(2024, 4, 12), new TimeOnly(9, 0, 0), 1 },
                    { 13, new DateOnly(2024, 4, 13), new TimeOnly(9, 0, 0), 1 },
                    { 14, new DateOnly(2024, 4, 14), new TimeOnly(9, 0, 0), 1 },
                    { 15, new DateOnly(2024, 4, 15), new TimeOnly(9, 0, 0), 1 },
                    { 16, new DateOnly(2024, 4, 16), new TimeOnly(9, 0, 0), 1 },
                    { 17, new DateOnly(2024, 4, 17), new TimeOnly(9, 0, 0), 1 },
                    { 18, new DateOnly(2024, 4, 18), new TimeOnly(9, 0, 0), 1 },
                    { 19, new DateOnly(2024, 4, 19), new TimeOnly(9, 0, 0), 1 },
                    { 20, new DateOnly(2024, 4, 20), new TimeOnly(9, 0, 0), 1 },
                    { 21, new DateOnly(2024, 4, 21), new TimeOnly(9, 0, 0), 1 },
                    { 22, new DateOnly(2024, 4, 22), new TimeOnly(9, 0, 0), 1 },
                    { 23, new DateOnly(2024, 4, 23), new TimeOnly(9, 0, 0), 1 },
                    { 24, new DateOnly(2024, 4, 24), new TimeOnly(9, 0, 0), 1 },
                    { 25, new DateOnly(2024, 4, 25), new TimeOnly(9, 0, 0), 1 },
                    { 26, new DateOnly(2024, 4, 26), new TimeOnly(9, 0, 0), 1 },
                    { 27, new DateOnly(2024, 4, 27), new TimeOnly(9, 0, 0), 1 },
                    { 28, new DateOnly(2024, 4, 28), new TimeOnly(9, 0, 0), 1 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AbsenceDegree", "Faculty", "GraduationYear", "NumberOfAbsences", "Specialization", "UniversityID" },
                values: new object[,]
                {
                    { 2, 0.0, "Engineering", 2023, 0, "Computer Science", "Kafr El Shi5" },
                    { 3, 0.0, "Engineering", 2023, 0, "Mechancial Engineering", "Tanta" }
                });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "Id", "AttendanceTime", "Date", "LeavingTime", "ScheduleId", "UserId" },
                values: new object[,]
                {
                    { 1, null, new DateOnly(1, 1, 1), null, 1, 2 },
                    { 2, null, new DateOnly(1, 1, 1), null, 1, 3 },
                    { 3, null, new DateOnly(1, 1, 1), null, 2, 2 },
                    { 4, null, new DateOnly(1, 1, 1), null, 2, 3 },
                    { 5, null, new DateOnly(1, 1, 1), null, 3, 2 },
                    { 6, null, new DateOnly(1, 1, 1), null, 3, 3 },
                    { 7, null, new DateOnly(1, 1, 1), null, 4, 2 },
                    { 8, null, new DateOnly(1, 1, 1), null, 4, 3 },
                    { 9, null, new DateOnly(1, 1, 1), null, 5, 2 },
                    { 10, null, new DateOnly(1, 1, 1), null, 5, 3 },
                    { 11, null, new DateOnly(1, 1, 1), null, 6, 2 },
                    { 12, null, new DateOnly(1, 1, 1), null, 6, 3 },
                    { 13, null, new DateOnly(1, 1, 1), null, 7, 2 },
                    { 14, null, new DateOnly(1, 1, 1), null, 7, 3 },
                    { 15, null, new DateOnly(1, 1, 1), null, 8, 2 },
                    { 16, null, new DateOnly(1, 1, 1), null, 8, 3 },
                    { 17, null, new DateOnly(1, 1, 1), null, 9, 2 },
                    { 18, null, new DateOnly(1, 1, 1), null, 9, 3 },
                    { 19, null, new DateOnly(1, 1, 1), null, 10, 2 },
                    { 20, null, new DateOnly(1, 1, 1), null, 10, 3 },
                    { 21, null, new DateOnly(1, 1, 1), null, 11, 2 },
                    { 22, null, new DateOnly(1, 1, 1), null, 11, 3 },
                    { 23, null, new DateOnly(1, 1, 1), null, 12, 2 },
                    { 24, null, new DateOnly(1, 1, 1), null, 12, 3 },
                    { 25, null, new DateOnly(1, 1, 1), null, 13, 2 },
                    { 26, null, new DateOnly(1, 1, 1), null, 13, 3 },
                    { 27, null, new DateOnly(1, 1, 1), null, 14, 2 },
                    { 28, null, new DateOnly(1, 1, 1), null, 14, 3 },
                    { 29, null, new DateOnly(1, 1, 1), null, 15, 2 },
                    { 30, null, new DateOnly(1, 1, 1), null, 15, 3 },
                    { 31, null, new DateOnly(1, 1, 1), null, 16, 2 },
                    { 32, null, new DateOnly(1, 1, 1), null, 16, 3 },
                    { 33, null, new DateOnly(1, 1, 1), null, 17, 2 },
                    { 34, null, new DateOnly(1, 1, 1), null, 17, 3 },
                    { 35, null, new DateOnly(1, 1, 1), null, 18, 2 },
                    { 36, null, new DateOnly(1, 1, 1), null, 18, 3 },
                    { 37, null, new DateOnly(1, 1, 1), null, 19, 2 },
                    { 38, null, new DateOnly(1, 1, 1), null, 19, 3 },
                    { 39, null, new DateOnly(1, 1, 1), null, 20, 2 },
                    { 40, null, new DateOnly(1, 1, 1), null, 20, 3 },
                    { 41, null, new DateOnly(1, 1, 1), null, 21, 2 },
                    { 42, null, new DateOnly(1, 1, 1), null, 21, 3 },
                    { 43, null, new DateOnly(1, 1, 1), null, 22, 2 },
                    { 44, null, new DateOnly(1, 1, 1), null, 22, 3 },
                    { 45, null, new DateOnly(1, 1, 1), null, 23, 2 },
                    { 46, null, new DateOnly(1, 1, 1), null, 23, 3 },
                    { 47, null, new DateOnly(1, 1, 1), null, 24, 2 },
                    { 48, null, new DateOnly(1, 1, 1), null, 24, 3 },
                    { 49, null, new DateOnly(1, 1, 1), null, 25, 2 },
                    { 50, null, new DateOnly(1, 1, 1), null, 25, 3 },
                    { 51, null, new DateOnly(1, 1, 1), null, 26, 2 },
                    { 52, null, new DateOnly(1, 1, 1), null, 26, 3 },
                    { 53, null, new DateOnly(1, 1, 1), null, 27, 2 },
                    { 54, null, new DateOnly(1, 1, 1), null, 27, 3 },
                    { 55, null, new DateOnly(1, 1, 1), null, 28, 2 },
                    { 56, null, new DateOnly(1, 1, 1), null, 28, 3 }
                });

            migrationBuilder.InsertData(
                table: "IntakesTracks",
                columns: new[] { "IntakeId", "TrackId", "StartDate", "Status" },
                values: new object[] { 1, 1, new DateOnly(1, 1, 1), "Active" });

            migrationBuilder.InsertData(
                table: "StdIntakeTrack",
                columns: new[] { "IntakeId", "StudentId", "TrackId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 1, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Supervises",
                columns: new[] { "InstructorId", "IntakeId", "TrackId" },
                values: new object[] { 5, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ScheduleId",
                table: "Attendances",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_UserId",
                table: "Attendances",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_ProgramId",
                table: "Intakes",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_IntakesTracks_TrackId",
                table: "IntakesTracks",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TrackId",
                table: "Schedules",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_StdIntakeTrack_StudentId",
                table: "StdIntakeTrack",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StdIntakeTrack_TrackId",
                table: "StdIntakeTrack",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervises_InstructorId",
                table: "Supervises",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervises_IntakeId",
                table: "Supervises",
                column: "IntakeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "IntakesTracks");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "StdIntakeTrack");

            migrationBuilder.DropTable(
                name: "Supervises");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Intakes");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ITIPrograms");
        }
    }
}
