using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orkaris_Back.Migrations
{
    /// <inheritdoc />
    public partial class YooooDUration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ses_duration",
                table: "t_e_session_ses");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ses_duration",
                table: "t_e_session_performance_spe",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ses_duration",
                table: "t_e_session_performance_spe");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ses_duration",
                table: "t_e_session_ses",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
