using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKodul1.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Questions_QuestionId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_QuestionId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryQuestion",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryQuestion", x => new { x.CategoriesId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_CategoryQuestion_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryQuestion_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryQuestion_QuestionsId",
                table: "CategoryQuestion",
                column: "QuestionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryQuestion");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_QuestionId",
                table: "Categories",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Questions_QuestionId",
                table: "Categories",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
