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
                    AttendanceTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    LeavingTime = table.Column<TimeOnly>(type: "time", nullable: false),
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
                    { 1, new DateOnly(2024, 4, 20), new TimeOnly(9, 0, 0), 1 },
                    { 2, new DateOnly(2024, 4, 21), new TimeOnly(9, 0, 0), 1 },
                    { 3, new DateOnly(2024, 4, 22), new TimeOnly(9, 0, 0), 1 },
                    { 4, new DateOnly(2024, 4, 23), new TimeOnly(9, 0, 0), 1 },
                    { 5, new DateOnly(2024, 4, 24), new TimeOnly(9, 0, 0), 1 },
                    { 6, new DateOnly(2024, 4, 25), new TimeOnly(9, 0, 0), 1 },
                    { 7, new DateOnly(2024, 4, 27), new TimeOnly(9, 0, 0), 1 }
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
                values: new object[] { 4, 1, 1 });

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
