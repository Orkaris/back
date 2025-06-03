using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orkaris_Back.Migrations
{
    /// <inheritdoc />
    public partial class AddExerciseMuscleRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_muscle_mus",
                columns: table => new
                {
                    mus_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mus_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_muscle_mus", x => x.mus_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_exercise_muscle_link",
                columns: table => new
                {
                    exr_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mus_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_exercise_muscle_link", x => new { x.exr_id, x.mus_id });
                    table.ForeignKey(
                        name: "FK_ExerciseMuscle_Exercise",
                        column: x => x.exr_id,
                        principalTable: "t_e_Exercise_exr",
                        principalColumn: "exr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseMuscle_Muscle",
                        column: x => x.mus_id,
                        principalTable: "t_e_muscle_mus",
                        principalColumn: "mus_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_exercise_muscle_link_mus_id",
                table: "t_e_exercise_muscle_link",
                column: "mus_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_exercise_muscle_link");

            migrationBuilder.DropTable(
                name: "t_e_muscle_mus");
        }
    }
}
