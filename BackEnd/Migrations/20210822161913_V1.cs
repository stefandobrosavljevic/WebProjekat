using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vakcina",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeVakcine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    BrojVakcinisanih = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vakcina", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Gradjanin",
                columns: table => new
                {
                    JMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BrojTelefona = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    VakcinaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradjanin", x => x.JMBG);
                    table.ForeignKey(
                        name: "FK_Gradjanin_Vakcina_VakcinaID",
                        column: x => x.VakcinaID,
                        principalTable: "Vakcina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gradjanin_VakcinaID",
                table: "Gradjanin",
                column: "VakcinaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gradjanin");

            migrationBuilder.DropTable(
                name: "Vakcina");
        }
    }
}
