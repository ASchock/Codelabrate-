using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherStudentEditor.Migrations
{
    public partial class FixSessionQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestion_AspNetUsers_AskedById",
                table: "SessionQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestion_Sessions_EditorSessionId",
                table: "SessionQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestionComment_SessionQuestion_SessionQuestionId",
                table: "SessionQuestionComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionQuestion",
                table: "SessionQuestion");

            migrationBuilder.RenameTable(
                name: "SessionQuestion",
                newName: "SessionQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestion_EditorSessionId",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_EditorSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestion_AskedById",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_AskedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionQuestions",
                table: "SessionQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestionComment_SessionQuestions_SessionQuestionId",
                table: "SessionQuestionComment",
                column: "SessionQuestionId",
                principalTable: "SessionQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_AspNetUsers_AskedById",
                table: "SessionQuestions",
                column: "AskedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Sessions_EditorSessionId",
                table: "SessionQuestions",
                column: "EditorSessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestionComment_SessionQuestions_SessionQuestionId",
                table: "SessionQuestionComment");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_AspNetUsers_AskedById",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Sessions_EditorSessionId",
                table: "SessionQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionQuestions",
                table: "SessionQuestions");

            migrationBuilder.RenameTable(
                name: "SessionQuestions",
                newName: "SessionQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_EditorSessionId",
                table: "SessionQuestion",
                newName: "IX_SessionQuestion_EditorSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_AskedById",
                table: "SessionQuestion",
                newName: "IX_SessionQuestion_AskedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionQuestion",
                table: "SessionQuestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestion_AspNetUsers_AskedById",
                table: "SessionQuestion",
                column: "AskedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestion_Sessions_EditorSessionId",
                table: "SessionQuestion",
                column: "EditorSessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestionComment_SessionQuestion_SessionQuestionId",
                table: "SessionQuestionComment",
                column: "SessionQuestionId",
                principalTable: "SessionQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
