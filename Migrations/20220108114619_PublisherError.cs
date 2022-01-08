using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class PublisherError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherID",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_PublisherID",
                table: "Movie",
                column: "PublisherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Publisher_PublisherID",
                table: "Movie",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Publisher_PublisherID",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropIndex(
                name: "IX_Movie_PublisherID",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "PublisherID",
                table: "Movie");
        }
    }
}
