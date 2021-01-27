using Microsoft.EntityFrameworkCore.Migrations;

namespace GMAPI.SqlServer.Migrations
{
    public partial class intialV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GMUserId",
                table: "PatientRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientRecords_GMUserId",
                table: "PatientRecords",
                column: "GMUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRecords_AspNetUsers_GMUserId",
                table: "PatientRecords",
                column: "GMUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRecords_AspNetUsers_GMUserId",
                table: "PatientRecords");

            migrationBuilder.DropIndex(
                name: "IX_PatientRecords_GMUserId",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "GMUserId",
                table: "PatientRecords");
        }
    }
}
