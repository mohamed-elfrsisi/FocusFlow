using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.DAL.Migrations;

/// <inheritdoc />
public partial class _20261126220201_init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ChatChannels",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TeamId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ChatChannels", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                Role = table.Column<int>(type: "int", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                EmailVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                Timezone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                ProfileVisibility = table.Column<int>(type: "int", nullable: false),
                AboutMe = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                WeekStart = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)1),
                DeletionRequestedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                ScheduledDeletionAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AuditEvents",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ActorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                TargetType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                TargetId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                Outcome = table.Column<int>(type: "int", nullable: false),
                IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                UserAgent = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                TeamId = table.Column<int>(type: "int", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuditEvents", x => x.Id);
                table.ForeignKey(
                    name: "FK_AuditEvents_Users_ActorId",
                    column: x => x.ActorId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "AuthTokens",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                TokenHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuthTokens", x => x.Id);
                table.ForeignKey(
                    name: "FK_AuthTokens_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "ChatMessages",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ChannelId = table.Column<int>(type: "int", nullable: false),
                SenderId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                Body = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DeletedById = table.Column<string>(type: "nvarchar(36)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ChatMessages", x => x.Id);
                table.ForeignKey(
                    name: "FK_ChatMessages_ChatChannels_ChannelId",
                    column: x => x.ChannelId,
                    principalTable: "ChatChannels",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ChatMessages_Users_DeletedById",
                    column: x => x.DeletedById,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
                table.ForeignKey(
                    name: "FK_ChatMessages_Users_SenderId",
                    column: x => x.SenderId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "UserSessions",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                RefreshHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Device = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastSeenAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserSessions", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserSessions_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AuditEvents_ActorId",
            table: "AuditEvents",
            column: "ActorId");

        migrationBuilder.CreateIndex(
            name: "IX_AuditEvents_CreatedAt",
            table: "AuditEvents",
            column: "CreatedAt");

        migrationBuilder.CreateIndex(
            name: "IX_AuditEvents_Outcome",
            table: "AuditEvents",
            column: "Outcome");

        migrationBuilder.CreateIndex(
            name: "IX_AuditEvents_TargetType_TargetId",
            table: "AuditEvents",
            columns: new[] { "TargetType", "TargetId" });

        migrationBuilder.CreateIndex(
            name: "IX_AuthTokens_ExpiresAt",
            table: "AuthTokens",
            column: "ExpiresAt");

        migrationBuilder.CreateIndex(
            name: "IX_AuthTokens_TokenHash",
            table: "AuthTokens",
            column: "TokenHash",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_AuthTokens_UserId_Type",
            table: "AuthTokens",
            columns: new[] { "UserId", "Type" });

        migrationBuilder.CreateIndex(
            name: "IX_ChatChannels_DeletedAt",
            table: "ChatChannels",
            column: "DeletedAt");

        migrationBuilder.CreateIndex(
            name: "IX_ChatChannels_TeamId",
            table: "ChatChannels",
            column: "TeamId",
            unique: true,
            filter: "[IsDefault] = 1 AND [DeletedAt] IS NULL");

        migrationBuilder.CreateIndex(
            name: "IX_ChatChannels_TeamId_Name",
            table: "ChatChannels",
            columns: new[] { "TeamId", "Name" },
            unique: true,
            filter: "[DeletedAt] IS NULL");

        migrationBuilder.CreateIndex(
            name: "IX_ChatMessages_ChannelId_CreatedAt",
            table: "ChatMessages",
            columns: new[] { "ChannelId", "CreatedAt" });

        migrationBuilder.CreateIndex(
            name: "IX_ChatMessages_DeletedAt",
            table: "ChatMessages",
            column: "DeletedAt");

        migrationBuilder.CreateIndex(
            name: "IX_ChatMessages_DeletedById",
            table: "ChatMessages",
            column: "DeletedById");

        migrationBuilder.CreateIndex(
            name: "IX_ChatMessages_SenderId",
            table: "ChatMessages",
            column: "SenderId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_DeletedAt",
            table: "Users",
            column: "DeletedAt");

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_ScheduledDeletionAt",
            table: "Users",
            column: "ScheduledDeletionAt");

        migrationBuilder.CreateIndex(
            name: "IX_Users_Status",
            table: "Users",
            column: "Status");

        migrationBuilder.CreateIndex(
            name: "IX_UserSessions_RefreshHash",
            table: "UserSessions",
            column: "RefreshHash",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserSessions_UserId",
            table: "UserSessions",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserSessions_UserId_RevokedAt",
            table: "UserSessions",
            columns: new[] { "UserId", "RevokedAt" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AuditEvents");

        migrationBuilder.DropTable(
            name: "AuthTokens");

        migrationBuilder.DropTable(
            name: "ChatMessages");

        migrationBuilder.DropTable(
            name: "UserSessions");

        migrationBuilder.DropTable(
            name: "ChatChannels");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
