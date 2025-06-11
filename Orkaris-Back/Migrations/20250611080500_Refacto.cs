using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orkaris_Back.Migrations
{
    /// <inheritdoc />
    public partial class Refacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_Exercise_exr",
                columns: table => new
                {
                    exr_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exr_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    exr_description = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    exr_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.exr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_sport_spo",
                columns: table => new
                {
                    spo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    spo_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport", x => x.spo_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_user_usr",
                columns: table => new
                {
                    usr_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usr_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usr_email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    usr_password = table.Column<string>(type: "text", nullable: false),
                    usr_gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    usr_height = table.Column<int>(type: "integer", nullable: false),
                    usr_weight = table.Column<int>(type: "integer", nullable: false),
                    usr_birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    usr_profile_type = table.Column<int>(type: "integer", nullable: false),
                    usr_is_verified = table.Column<bool>(type: "boolean", nullable: false),
                    usr_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.usr_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_exercise_goal_exg",
                columns: table => new
                {
                    exg_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exg_reps = table.Column<int>(type: "integer", nullable: false),
                    exg_sets = table.Column<int>(type: "integer", nullable: false),
                    exr_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    exr_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseGoal", x => x.exg_id);
                    table.ForeignKey(
                        name: "FK_t_e_exercise_goal_exg_t_e_Exercise_exr_exr_id",
                        column: x => x.exr_id,
                        principalTable: "t_e_Exercise_exr",
                        principalColumn: "exr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_category_cat",
                columns: table => new
                {
                    cat_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cat_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    spo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cat_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.cat_id);
                    table.ForeignKey(
                        name: "FK_t_e_category_cat_t_e_sport_spo_spo_id",
                        column: x => x.spo_id,
                        principalTable: "t_e_sport_spo",
                        principalColumn: "spo_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_Workout_wkt",
                columns: table => new
                {
                    pfr_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pfr_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usr_id = table.Column<Guid>(type: "uuid", nullable: false),
                    pfr_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.pfr_id);
                    table.ForeignKey(
                        name: "FK_t_e_Workout_wkt_t_e_user_usr_usr_id",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_email_confirmation_tokens_ect",
                columns: table => new
                {
                    ect_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ect_token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usr_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ect_expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ect_is_used = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfirmationToken", x => x.ect_id);
                    table.ForeignKey(
                        name: "FK_t_email_confirmation_tokens_ect_t_e_user_usr_usr_id",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_exercise_goal_performance_egp",
                columns: table => new
                {
                    egp_id = table.Column<Guid>(type: "uuid", nullable: false),
                    egp_reps = table.Column<int>(type: "integer", nullable: false),
                    egp_sets = table.Column<int>(type: "integer", nullable: false),
                    egp_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    exg_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseGoalPerformance", x => x.egp_id);
                    table.ForeignKey(
                        name: "FK_t_e_exercise_goal_performance_egp_t_e_exercise_goal_exg_exg~",
                        column: x => x.exg_id,
                        principalTable: "t_e_exercise_goal_exg",
                        principalColumn: "exg_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_exercise_category_ext",
                columns: table => new
                {
                    exr_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cat_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseCategory", x => new { x.exr_id, x.cat_id });
                    table.ForeignKey(
                        name: "FK_t_j_exercise_category_ext_t_e_Exercise_exr_exr_id",
                        column: x => x.exr_id,
                        principalTable: "t_e_Exercise_exr",
                        principalColumn: "exr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_exercise_category_ext_t_e_category_cat_cat_id",
                        column: x => x.cat_id,
                        principalTable: "t_e_category_cat",
                        principalColumn: "cat_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_session_ses",
                columns: table => new
                {
                    ses_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ses_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    usr_id = table.Column<Guid>(type: "uuid", nullable: false),
                    wrk_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ses_duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    ses_created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.ses_id);
                    table.ForeignKey(
                        name: "FK_t_e_session_ses_t_e_Workout_wkt_wrk_id",
                        column: x => x.wrk_id,
                        principalTable: "t_e_Workout_wkt",
                        principalColumn: "pfr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_e_session_ses_t_e_user_usr_usr_id",
                        column: x => x.usr_id,
                        principalTable: "t_e_user_usr",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_session_performance_spe",
                columns: table => new
                {
                    spe_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ses_id = table.Column<Guid>(type: "uuid", nullable: false),
                    spe_feeling = table.Column<string>(type: "text", nullable: true),
                    spe_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPerformance", x => x.spe_id);
                    table.ForeignKey(
                        name: "FK_t_e_session_performance_spe_t_e_session_ses_ses_id",
                        column: x => x.ses_id,
                        principalTable: "t_e_session_ses",
                        principalColumn: "ses_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_session_exercise_goal_seg",
                columns: table => new
                {
                    ses_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exg_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionExercise", x => new { x.ses_id, x.exg_id });
                    table.ForeignKey(
                        name: "FK_t_j_session_exercise_goal_seg_t_e_exercise_goal_exg_exg_id",
                        column: x => x.exg_id,
                        principalTable: "t_e_exercise_goal_exg",
                        principalColumn: "exg_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_session_exercise_goal_seg_t_e_session_ses_ses_id",
                        column: x => x.ses_id,
                        principalTable: "t_e_session_ses",
                        principalColumn: "ses_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_category_cat_cat_name",
                table: "t_e_category_cat",
                column: "cat_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_category_cat_spo_id",
                table: "t_e_category_cat",
                column: "spo_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_Exercise_exr_exr_name",
                table: "t_e_Exercise_exr",
                column: "exr_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_exercise_goal_exg_exr_id",
                table: "t_e_exercise_goal_exg",
                column: "exr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_exercise_goal_performance_egp_exg_id",
                table: "t_e_exercise_goal_performance_egp",
                column: "exg_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_session_performance_spe_ses_id",
                table: "t_e_session_performance_spe",
                column: "ses_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_session_ses_usr_id",
                table: "t_e_session_ses",
                column: "usr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_session_ses_wrk_id",
                table: "t_e_session_ses",
                column: "wrk_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_sport_spo_spo_name",
                table: "t_e_sport_spo",
                column: "spo_name",
                unique: true);

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
                name: "IX_t_e_Workout_wkt_usr_id",
                table: "t_e_Workout_wkt",
                column: "usr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_email_confirmation_tokens_ect_usr_id",
                table: "t_email_confirmation_tokens_ect",
                column: "usr_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_exercise_category_ext_cat_id",
                table: "t_j_exercise_category_ext",
                column: "cat_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_session_exercise_goal_seg_exg_id",
                table: "t_j_session_exercise_goal_seg",
                column: "exg_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_exercise_goal_performance_egp");

            migrationBuilder.DropTable(
                name: "t_e_session_performance_spe");

            migrationBuilder.DropTable(
                name: "t_email_confirmation_tokens_ect");

            migrationBuilder.DropTable(
                name: "t_j_exercise_category_ext");

            migrationBuilder.DropTable(
                name: "t_j_session_exercise_goal_seg");

            migrationBuilder.DropTable(
                name: "t_e_category_cat");

            migrationBuilder.DropTable(
                name: "t_e_exercise_goal_exg");

            migrationBuilder.DropTable(
                name: "t_e_session_ses");

            migrationBuilder.DropTable(
                name: "t_e_sport_spo");

            migrationBuilder.DropTable(
                name: "t_e_Exercise_exr");

            migrationBuilder.DropTable(
                name: "t_e_Workout_wkt");

            migrationBuilder.DropTable(
                name: "t_e_user_usr");
        }
    }
}
