using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatsConstructor.WebApi.Migrations
{
    public partial class Develop12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "QuestionType",
                table: "Questions",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionType",
                table: "Questions");
        }
    }
}
