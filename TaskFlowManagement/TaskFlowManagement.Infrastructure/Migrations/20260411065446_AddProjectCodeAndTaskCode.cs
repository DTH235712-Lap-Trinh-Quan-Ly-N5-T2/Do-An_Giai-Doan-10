using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlowManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectCodeAndTaskCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskCode",
                table: "TaskItems",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "Projects",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_TaskCode",
                table: "TaskItems",
                column: "TaskCode");

            migrationBuilder.CreateIndex(
                name: "UQ_Projects_ProjectCode",
                table: "Projects",
                column: "ProjectCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskItems_TaskCode",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "UQ_Projects_ProjectCode",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TaskCode",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "Projects");
        }
    }
}
