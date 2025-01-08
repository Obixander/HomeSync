using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class customlistUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomLists_Families_FamilyId",
                table: "CustomLists");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "CustomLists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomLists_Families_FamilyId",
                table: "CustomLists",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomLists_Families_FamilyId",
                table: "CustomLists");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "CustomLists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomLists_Families_FamilyId",
                table: "CustomLists",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "FamilyId");
        }
    }
}
