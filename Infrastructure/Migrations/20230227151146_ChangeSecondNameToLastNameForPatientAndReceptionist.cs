using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSecondNameToLastNameForPatientAndReceptionist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Receptionists",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Patients",
                newName: "LastName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Receptionists",
                newName: "SecondName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Patients",
                newName: "SecondName");
        }
    }
}
