using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartLock.Services.AccessRight.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessRights",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LockId = table.Column<Guid>(nullable: false),
                    HasAccess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRights", x => new { x.UserId, x.LockId });
                    table.UniqueConstraint("AK_AccessRights_LockId_UserId", x => new { x.LockId, x.UserId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRights");
        }
    }
}
