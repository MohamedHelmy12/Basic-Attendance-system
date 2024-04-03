using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Attendance.Migrations
{
    /// <inheritdoc />
    public partial class student_intake_track : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_StdIntakeTrack_StudentId",
                table: "StdIntakeTrack",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StdIntakeTrack_TrackId",
                table: "StdIntakeTrack",
                column: "TrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StdIntakeTrack");
        }
    }
}
