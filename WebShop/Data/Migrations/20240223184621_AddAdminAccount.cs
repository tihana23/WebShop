using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using WebShop.Models;

#nullable disable

namespace WebShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminAccount : Migration
    {
        const string addUserGuid = "87DE39DF-4284-4C4E-B290-21CB51500175";
        const string adminRoleGuid = "57aa9f55-7a5b-4cd4-b543-fd12015ebfec";

       
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var passwordHash = hasher.HashPassword(null, "123456");

            migrationBuilder.Sql($@"INSERT INTO AspNetUsers(
              Id,
              UserName,
              NormalizedUserName,
              Email,
              NormalizedEmail,
              EmailConfirmed,
              PasswordHash,
              SecurityStamp,
              ConcurrencyStamp,
              PhoneNumber,
              PhoneNumberConfirmed,
              TwoFactorEnabled,
              LockoutEnd,
            LockoutEnabled,
              AccessFailedCount,
              Address,
              FirstName,
              LastName
          ) VALUES(
              '{addUserGuid}',
              'admin@admin.com',
              'ADMIN@admin.COM',
              'admin@admin.com',
              'ADMIN@admin.COM',
              1,
              '{passwordHash}',
              'SECURITY_STAMP',
              'CONCURRENCY_STAMP',
              null, 
              0, 
              0, 
              null,
              1, 
              0, 
              'Some Address',
              'Admin',
              'Admin'
          )");
            migrationBuilder.Sql($@"
        INSERT INTO AspNetRoles (Id, Name, NormalizedName)
        VALUES ('{adminRoleGuid}', 'Admin', 'ADMIN')
    ");

            migrationBuilder.Sql($@"
        INSERT INTO AspNetUserRoles (UserId, RoleId)
        VALUES ('{addUserGuid}', '{adminRoleGuid}')
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
        DELETE FROM AspNetRoles
        WHERE Id = '{adminRoleGuid}'
    ");

          
            migrationBuilder.Sql($@"
        DELETE FROM AspNetUserRoles
        WHERE UserId = '{addUserGuid}'
    ");
            migrationBuilder.Sql($@"
        DELETE FROM AspNetUsers
        WHERE Id = '{addUserGuid}'
    ");
        }
    }
}
