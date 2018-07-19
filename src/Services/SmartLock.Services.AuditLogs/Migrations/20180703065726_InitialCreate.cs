using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartLock.Services.AuditLogs.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    AuditLogId = table.Column<Guid>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserNameFamily = table.Column<string>(nullable: false),
                    LockId = table.Column<Guid>(nullable: false),
                    LockName = table.Column<string>(nullable: false),
                    Command = table.Column<string>(maxLength: 10, nullable: false),
                    CommandResult = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.AuditLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");
        }
    }
}
