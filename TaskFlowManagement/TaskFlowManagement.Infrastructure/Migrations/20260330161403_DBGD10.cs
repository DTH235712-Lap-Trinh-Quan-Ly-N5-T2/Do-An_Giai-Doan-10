using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlowManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBGD10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TaskItems_ActualHours",
                table: "TaskItems",
                sql: "[ActualHours] IS NULL OR [ActualHours] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TaskItems_EstimatedHours",
                table: "TaskItems",
                sql: "[EstimatedHours] IS NULL OR [EstimatedHours] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TaskItems_ProgressPercent",
                table: "TaskItems",
                sql: "[ProgressPercent] BETWEEN 0 AND 100");

            migrationBuilder.CreateIndex(
                name: "UQ_Statuses_DisplayOrder",
                table: "Statuses",
                column: "DisplayOrder",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Statuses_Name",
                table: "Statuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Projects_Budget",
                table: "Projects",
                sql: "[Budget] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Projects_Priority",
                table: "Projects",
                sql: "[Priority] BETWEEN 1 AND 4");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Projects_Status",
                table: "Projects",
                sql: "[Status] IN ('NotStarted','InProgress','OnHold','Completed','Cancelled')");

            migrationBuilder.CreateIndex(
                name: "UQ_Priorities_Level",
                table: "Priorities",
                column: "Level",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Priorities_Name",
                table: "Priorities",
                column: "Name",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Priorities_Level",
                table: "Priorities",
                sql: "[Level] BETWEEN 1 AND 4");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Expenses_Amount",
                table: "Expenses",
                sql: "[Amount] > 0");

            migrationBuilder.CreateIndex(
                name: "UQ_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TaskItems_ActualHours",
                table: "TaskItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TaskItems_EstimatedHours",
                table: "TaskItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TaskItems_ProgressPercent",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "UQ_Statuses_DisplayOrder",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "UQ_Statuses_Name",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "UQ_Roles_Name",
                table: "Roles");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Projects_Budget",
                table: "Projects");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Projects_Priority",
                table: "Projects");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Projects_Status",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "UQ_Priorities_Level",
                table: "Priorities");

            migrationBuilder.DropIndex(
                name: "UQ_Priorities_Name",
                table: "Priorities");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Priorities_Level",
                table: "Priorities");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Expenses_Amount",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "UQ_Categories_Name",
                table: "Categories");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
