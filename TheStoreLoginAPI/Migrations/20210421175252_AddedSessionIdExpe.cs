using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheStoreLoginAPI.Migrations
{
    public partial class AddedSessionIdExpe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SessionIdExpiresOn",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionIdExpiresOn",
                table: "Users");
        }
    }
}
