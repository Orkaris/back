using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orkaris_Back.Migrations
{
    /// <inheritdoc />
    public partial class AddExerciseMuscleLinkNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMuscle_Exercise",
                table: "t_e_exercise_muscle_link");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMuscle_Muscle",
                table: "t_e_exercise_muscle_link");

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_exercise_muscle_link_t_e_Exercise_exr_exr_id",
                table: "t_e_exercise_muscle_link",
                column: "exr_id",
                principalTable: "t_e_Exercise_exr",
                principalColumn: "exr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t_e_exercise_muscle_link_t_e_muscle_mus_mus_id",
                table: "t_e_exercise_muscle_link",
                column: "mus_id",
                principalTable: "t_e_muscle_mus",
                principalColumn: "mus_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t_e_exercise_muscle_link_t_e_Exercise_exr_exr_id",
                table: "t_e_exercise_muscle_link");

            migrationBuilder.DropForeignKey(
                name: "FK_t_e_exercise_muscle_link_t_e_muscle_mus_mus_id",
                table: "t_e_exercise_muscle_link");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMuscle_Exercise",
                table: "t_e_exercise_muscle_link",
                column: "exr_id",
                principalTable: "t_e_Exercise_exr",
                principalColumn: "exr_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMuscle_Muscle",
                table: "t_e_exercise_muscle_link",
                column: "mus_id",
                principalTable: "t_e_muscle_mus",
                principalColumn: "mus_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
