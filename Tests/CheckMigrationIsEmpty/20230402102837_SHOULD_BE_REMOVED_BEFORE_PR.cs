using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomNS
{
    /// <inheritdoc />
    public partial class SHOULD_BE_REMOVED_BEFORE_PR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewColumn",
                table: "Posts",
                newName: "NewColumn2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewColumn2",
                table: "Posts",
                newName: "NewColumn");
        }
    }
}
