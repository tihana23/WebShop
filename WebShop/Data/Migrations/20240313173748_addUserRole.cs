using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUserRole : Migration
    {
        const string userRoleGuid = "96409627-8420-4099-b5c9-73579f7c13d1";
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
        INSERT INTO AspNetRoles (Id, Name, NormalizedName)
        VALUES ('{userRoleGuid}', 'User', 'USER')
    ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
        }
    }
}
