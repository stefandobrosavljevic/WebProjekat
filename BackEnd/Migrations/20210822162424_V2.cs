using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmbulantaID",
                table: "Vakcina",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmbulantaID",
                table: "Gradjanin",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ambulanta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Grad = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BrojPunktova = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambulanta", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vakcina_AmbulantaID",
                table: "Vakcina",
                column: "AmbulantaID");

            migrationBuilder.CreateIndex(
                name: "IX_Gradjanin_AmbulantaID",
                table: "Gradjanin",
                column: "AmbulantaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Gradjanin_Ambulanta_AmbulantaID",
                table: "Gradjanin",
                column: "AmbulantaID",
                principalTable: "Ambulanta",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vakcina_Ambulanta_AmbulantaID",
                table: "Vakcina",
                column: "AmbulantaID",
                principalTable: "Ambulanta",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gradjanin_Ambulanta_AmbulantaID",
                table: "Gradjanin");

            migrationBuilder.DropForeignKey(
                name: "FK_Vakcina_Ambulanta_AmbulantaID",
                table: "Vakcina");

            migrationBuilder.DropTable(
                name: "Ambulanta");

            migrationBuilder.DropIndex(
                name: "IX_Vakcina_AmbulantaID",
                table: "Vakcina");

            migrationBuilder.DropIndex(
                name: "IX_Gradjanin_AmbulantaID",
                table: "Gradjanin");

            migrationBuilder.DropColumn(
                name: "AmbulantaID",
                table: "Vakcina");

            migrationBuilder.DropColumn(
                name: "AmbulantaID",
                table: "Gradjanin");
        }
    }
}
