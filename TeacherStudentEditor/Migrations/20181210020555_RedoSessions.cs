using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeacherStudentEditor.Migrations
{
    public partial class RedoSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileQuestionComment");

            migrationBuilder.DropTable(
                name: "FileQuestion");

            migrationBuilder.DropTable(
                name: "EditorFile");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: true),
                    Language = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_AspNetUsers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartLineNumber = table.Column<int>(nullable: false),
                    StartColumn = table.Column<int>(nullable: false),
                    EndLineNumber = table.Column<int>(nullable: false),
                    EndColumn = table.Column<int>(nullable: false),
                    AskedById = table.Column<Guid>(nullable: false),
                    Asked = table.Column<DateTime>(nullable: false),
                    EditorSessionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionQuestion_AspNetUsers_AskedById",
                        column: x => x.AskedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionQuestion_Sessions_EditorSessionId",
                        column: x => x.EditorSessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionQuestionComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Posted = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    SessionQuestionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionQuestionComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionQuestionComment_SessionQuestion_SessionQuestionId",
                        column: x => x.SessionQuestionId,
                        principalTable: "SessionQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionQuestionComment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionQuestion_AskedById",
                table: "SessionQuestion",
                column: "AskedById");

            migrationBuilder.CreateIndex(
                name: "IX_SessionQuestion_EditorSessionId",
                table: "SessionQuestion",
                column: "EditorSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionQuestionComment_SessionQuestionId",
                table: "SessionQuestionComment",
                column: "SessionQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionQuestionComment_UserId",
                table: "SessionQuestionComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TeacherId",
                table: "Sessions",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionQuestionComment");

            migrationBuilder.DropTable(
                name: "SessionQuestion");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.CreateTable(
                name: "EditorFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClassId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: false),
                    Language = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditorFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EditorFile_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Asked = table.Column<DateTime>(nullable: false),
                    AskedById = table.Column<Guid>(nullable: false),
                    EditorFileId = table.Column<Guid>(nullable: true),
                    EndColumn = table.Column<int>(nullable: false),
                    EndLineNumber = table.Column<int>(nullable: false),
                    StartColumn = table.Column<int>(nullable: false),
                    StartLineNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileQuestion_AspNetUsers_AskedById",
                        column: x => x.AskedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileQuestion_EditorFile_EditorFileId",
                        column: x => x.EditorFileId,
                        principalTable: "EditorFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileQuestionComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileQuestionId = table.Column<Guid>(nullable: true),
                    Posted = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileQuestionComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileQuestionComment_FileQuestion_FileQuestionId",
                        column: x => x.FileQuestionId,
                        principalTable: "FileQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileQuestionComment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditorFile_ClassId",
                table: "EditorFile",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_FileQuestion_AskedById",
                table: "FileQuestion",
                column: "AskedById");

            migrationBuilder.CreateIndex(
                name: "IX_FileQuestion_EditorFileId",
                table: "FileQuestion",
                column: "EditorFileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileQuestionComment_FileQuestionId",
                table: "FileQuestionComment",
                column: "FileQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FileQuestionComment_UserId",
                table: "FileQuestionComment",
                column: "UserId");
        }
    }
}
