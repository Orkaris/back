using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orkaris_Back.Migrations
{
    /// <inheritdoc />
    public partial class AjoutUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "usr_email",
                table: "t_e_user_usr",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_user_usr_usr_email",
                table: "t_e_user_usr",
                column: "usr_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_user_usr_usr_name",
                table: "t_e_user_usr",
                column: "usr_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_type_tpe_tpe_name",
                table: "t_e_type_tpe",
                column: "tpe_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_sport_spo_spo_name",
                table: "t_e_sport_spo",
                column: "spo_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_Exercise_exr_exr_name",
                table: "t_e_Exercise_exr",
                column: "exr_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_e_user_usr_usr_email",
                table: "t_e_user_usr");

            migrationBuilder.DropIndex(
                name: "IX_t_e_user_usr_usr_name",
                table: "t_e_user_usr");

            migrationBuilder.DropIndex(
                name: "IX_t_e_type_tpe_tpe_name",
                table: "t_e_type_tpe");

            migrationBuilder.DropIndex(
                name: "IX_t_e_sport_spo_spo_name",
                table: "t_e_sport_spo");

            migrationBuilder.DropIndex(
                name: "IX_t_e_Exercise_exr_exr_name",
                table: "t_e_Exercise_exr");

            migrationBuilder.AlterColumn<string>(
                name: "usr_email",
                table: "t_e_user_usr",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
