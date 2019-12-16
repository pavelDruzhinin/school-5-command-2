using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatsConstructor.WebApi.Migrations
{
    public partial class Develop13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSessions_Chats_ChatId",
                table: "ChatSessions");

            migrationBuilder.DropColumn(
                name: "FinalText",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "WelcomeText",
                table: "Chats");

            migrationBuilder.AddColumn<byte>(
                name: "QuestionAnswerType",
                table: "Questions",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<long>(
                name: "ChatId",
                table: "ChatSessions",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AnswerUtcDateTime",
                table: "ChatSessionAnswers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSessions_Chats_ChatId",
                table: "ChatSessions",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSessions_Chats_ChatId",
                table: "ChatSessions");

            migrationBuilder.DropColumn(
                name: "QuestionAnswerType",
                table: "Questions");

            migrationBuilder.AlterColumn<long>(
                name: "ChatId",
                table: "ChatSessions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AnswerUtcDateTime",
                table: "ChatSessionAnswers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinalText",
                table: "Chats",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WelcomeText",
                table: "Chats",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSessions_Chats_ChatId",
                table: "ChatSessions",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
