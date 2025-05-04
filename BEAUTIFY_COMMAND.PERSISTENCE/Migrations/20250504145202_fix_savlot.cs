using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class fix_savlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCertificate_Service_ServiceId",
                table: "DoctorCertificate");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "DoctorCertificate",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorCertificate_ServiceId",
                table: "DoctorCertificate",
                newName: "IX_DoctorCertificate_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCertificate_Category_CategoryId",
                table: "DoctorCertificate",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorCertificate_Category_CategoryId",
                table: "DoctorCertificate");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "DoctorCertificate",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorCertificate_CategoryId",
                table: "DoctorCertificate",
                newName: "IX_DoctorCertificate_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorCertificate_Service_ServiceId",
                table: "DoctorCertificate",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id");
        }
    }
}
