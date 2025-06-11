using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orkaris_Back.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightToExerciseGoalAndPerformance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "egp_weight",
                table: "t_e_exercise_goal_performance_egp",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "exg_weight",
                table: "t_e_exercise_goal_exg",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "egp_weight",
                table: "t_e_exercise_goal_performance_egp");

            migrationBuilder.DropColumn(
                name: "exg_weight",
                table: "t_e_exercise_goal_exg");
        }
    }
}
