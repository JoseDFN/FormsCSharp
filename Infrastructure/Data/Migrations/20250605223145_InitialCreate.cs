using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories_catalog",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_catalog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "options_response",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    optiontext = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options_response", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "surveys",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    instruction = table.Column<string>(type: "text", nullable: true),
                    componentreact = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    componenthtml = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category_options",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    catalogoptions_id = table.Column<int>(type: "integer", nullable: false),
                    categoriesoptions_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_category_options_categories_catalog_categoriesoptions_id",
                        column: x => x.categoriesoptions_id,
                        principalTable: "categories_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_category_options_options_response_catalogoptions_id",
                        column: x => x.catalogoptions_id,
                        principalTable: "options_response",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chapters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    survey_id = table.Column<int>(type: "integer", nullable: false),
                    chapter_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    chapter_title = table.Column<string>(type: "text", nullable: false),
                    componenthtml = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    componentreact = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapters", x => x.id);
                    table.ForeignKey(
                        name: "FK_chapters_surveys_survey_id",
                        column: x => x.survey_id,
                        principalTable: "surveys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    chapter_id = table.Column<int>(type: "integer", nullable: false),
                    question_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    response_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    question_text = table.Column<string>(type: "text", nullable: false),
                    comment_question = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_questions_chapters_chapter_id",
                        column: x => x.chapter_id,
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subquestion_id = table.Column<int>(type: "integer", nullable: false),
                    subquestion_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    subquestiontext = table.Column<string>(type: "text", nullable: false),
                    comment_subquestion = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_questions_questions_subquestion_id",
                        column: x => x.subquestion_id,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "summary_options",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_survey = table.Column<int>(type: "integer", nullable: false),
                    code_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    id_question = table.Column<int>(type: "integer", nullable: false),
                    valor_t = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_summary_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_summary_options_questions_id_question",
                        column: x => x.id_question,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_summary_options_surveys_id_survey",
                        column: x => x.id_survey,
                        principalTable: "surveys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "option_questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    option_id = table.Column<int>(type: "integer", nullable: false),
                    optioncatalog_id = table.Column<int>(type: "integer", nullable: false),
                    optionquestion_id = table.Column<int>(type: "integer", nullable: false),
                    subquestion_id = table.Column<int>(type: "integer", nullable: true),
                    numberoption = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    comment_optionres = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_option_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_option_questions_categories_catalog_optioncatalog_id",
                        column: x => x.optioncatalog_id,
                        principalTable: "categories_catalog",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_option_questions_options_response_option_id",
                        column: x => x.option_id,
                        principalTable: "options_response",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_option_questions_questions_optionquestion_id",
                        column: x => x.optionquestion_id,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_option_questions_sub_questions_subquestion_id",
                        column: x => x.subquestion_id,
                        principalTable: "sub_questions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_options_catalogoptions_id",
                table: "category_options",
                column: "catalogoptions_id");

            migrationBuilder.CreateIndex(
                name: "IX_category_options_categoriesoptions_id",
                table: "category_options",
                column: "categoriesoptions_id");

            migrationBuilder.CreateIndex(
                name: "IX_chapters_survey_id",
                table: "chapters",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_option_questions_option_id",
                table: "option_questions",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "IX_option_questions_optioncatalog_id",
                table: "option_questions",
                column: "optioncatalog_id");

            migrationBuilder.CreateIndex(
                name: "IX_option_questions_optionquestion_id",
                table: "option_questions",
                column: "optionquestion_id");

            migrationBuilder.CreateIndex(
                name: "IX_option_questions_subquestion_id",
                table: "option_questions",
                column: "subquestion_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_chapter_id",
                table: "questions",
                column: "chapter_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_questions_subquestion_id",
                table: "sub_questions",
                column: "subquestion_id");

            migrationBuilder.CreateIndex(
                name: "IX_summary_options_id_question",
                table: "summary_options",
                column: "id_question");

            migrationBuilder.CreateIndex(
                name: "IX_summary_options_id_survey",
                table: "summary_options",
                column: "id_survey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category_options");

            migrationBuilder.DropTable(
                name: "option_questions");

            migrationBuilder.DropTable(
                name: "summary_options");

            migrationBuilder.DropTable(
                name: "categories_catalog");

            migrationBuilder.DropTable(
                name: "options_response");

            migrationBuilder.DropTable(
                name: "sub_questions");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "chapters");

            migrationBuilder.DropTable(
                name: "surveys");
        }
    }
}
