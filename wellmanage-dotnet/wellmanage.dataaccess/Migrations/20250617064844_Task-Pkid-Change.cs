using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wellmanage.data.Migrations
{
    /// <inheritdoc />
    public partial class TaskPkidChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop PK constraint first
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTasks",
                table: "ProjectTasks");

            // Alter the column type
            migrationBuilder.AlterColumn<long>(
                name: "TaskId",
                table: "ProjectTasks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            // Recreate the PK constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTasks",
                table: "ProjectTasks",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop PK constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTasks",
                table: "ProjectTasks");

            // Revert the column type
            migrationBuilder.AlterColumn<int>(
                name: "TaskId",
                table: "ProjectTasks",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            // Recreate the PK constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTasks",
                table: "ProjectTasks",
                column: "TaskId");
        }

    }
}
