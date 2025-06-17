using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevHobby.GPTizza.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderPlaced = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "PizzaRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Steps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaRecipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPizzaOfTheWeek = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PizzaId = table.Column<int>(type: "int", nullable: true),
                    TicketStatus = table.Column<int>(type: "int", nullable: false),
                    TicketType = table.Column<int>(type: "int", nullable: false),
                    CustomerSentimentScore = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PizzaId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Pizzas_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSupportMessage = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketMessageSentiment = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketMessages_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "OrderPlaced" },
                values: new object[,]
                {
                    { 1, 172, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 25, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 198, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 7, new DateTime(2023, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 120, new DateTime(2023, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 42, new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 280, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 15, new DateTime(2023, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 99, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 210, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 55, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 180, new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 8, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 290, new DateTime(2023, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 33, new DateTime(2023, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 160, new DateTime(2023, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 70, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 240, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 11, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 130, new DateTime(2023, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 66, new DateTime(2023, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 205, new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, 3, new DateTime(2023, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, 145, new DateTime(2023, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 88, new DateTime(2023, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, 260, new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, 22, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, 175, new DateTime(2023, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, 101, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, 4, new DateTime(2023, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, 250, new DateTime(2023, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, 77, new DateTime(2023, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, 190, new DateTime(2023, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, 30, new DateTime(2023, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, 165, new DateTime(2023, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, 95, new DateTime(2023, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, 222, new DateTime(2023, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, 60, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, 140, new DateTime(2023, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, 5, new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, 270, new DateTime(2023, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, 18, new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, 110, new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, 82, new DateTime(2023, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, 200, new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, 1, new DateTime(2023, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, 230, new DateTime(2023, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, 75, new DateTime(2023, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, 150, new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, 35, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 51, 285, new DateTime(2023, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 52, 90, new DateTime(2023, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, 215, new DateTime(2023, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 54, 45, new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 55, 185, new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 56, 10, new DateTime(2023, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 57, 295, new DateTime(2023, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, 62, new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 59, 133, new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 60, 2, new DateTime(2023, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 61, 255, new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 62, 70, new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 63, 170, new DateTime(2023, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 64, 38, new DateTime(2023, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 65, 245, new DateTime(2023, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, 99, new DateTime(2023, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 67, 125, new DateTime(2023, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 68, 6, new DateTime(2023, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69, 210, new DateTime(2023, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 70, 50, new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 71, 160, new DateTime(2023, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72, 28, new DateTime(2023, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, 193, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 74, 72, new DateTime(2023, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 75, 265, new DateTime(2023, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 76, 115, new DateTime(2023, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77, 40, new DateTime(2023, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 78, 290, new DateTime(2023, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 79, 17, new DateTime(2023, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 80, 105, new DateTime(2023, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 81, 300, new DateTime(2023, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 82, 23, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 83, 220, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84, 58, new DateTime(2023, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 85, 142, new DateTime(2023, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 86, 85, new DateTime(2023, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 87, 275, new DateTime(2023, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 88, 195, new DateTime(2023, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 89, 48, new DateTime(2023, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 90, 167, new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 91, 92, new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 92, 208, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 93, 76, new DateTime(2023, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 94, 251, new DateTime(2023, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 95, 12, new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 96, 280, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 97, 15, new DateTime(2023, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 98, 180, new DateTime(2023, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 99, 99, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 100, 210, new DateTime(2023, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 101, 167, new DateTime(2024, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 102, 84, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 103, 299, new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 104, 12, new DateTime(2024, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 105, 251, new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 106, 105, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 107, 22, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 108, 193, new DateTime(2024, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 109, 76, new DateTime(2024, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 110, 288, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 111, 45, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 112, 118, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 113, 201, new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 114, 300, new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 115, 14, new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 116, 233, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 117, 66, new DateTime(2024, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 118, 175, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 119, 90, new DateTime(2024, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 120, 265, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 121, 58, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 122, 133, new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 123, 1, new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 124, 210, new DateTime(2024, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 125, 2, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 126, 280, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 127, 77, new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 128, 154, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 129, 244, new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 130, 10, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 131, 187, new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 132, 30, new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 133, 271, new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 134, 95, new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 135, 16, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 136, 255, new DateTime(2024, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 137, 70, new DateTime(2024, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 138, 140, new DateTime(2024, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 139, 220, new DateTime(2024, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 140, 38, new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 141, 190, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 142, 50, new DateTime(2024, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 143, 295, new DateTime(2024, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 144, 100, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 145, 10, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 146, 270, new DateTime(2024, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 147, 82, new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 148, 160, new DateTime(2024, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 149, 230, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 150, 3, new DateTime(2024, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 151, 205, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 152, 40, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 153, 285, new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 154, 110, new DateTime(2024, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 155, 20, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 156, 260, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 157, 75, new DateTime(2024, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 158, 145, new DateTime(2024, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 159, 215, new DateTime(2024, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 160, 35, new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 161, 195, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 162, 55, new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 163, 290, new DateTime(2024, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 164, 125, new DateTime(2024, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 165, 25, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 166, 275, new DateTime(2024, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 167, 88, new DateTime(2024, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 168, 165, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 169, 240, new DateTime(2024, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 170, 18, new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 171, 21, new DateTime(2024, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 172, 200, new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 173, 99, new DateTime(2024, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 174, 130, new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 175, 5, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 176, 279, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 177, 62, new DateTime(2024, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 178, 150, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 179, 225, new DateTime(2024, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 180, 49, new DateTime(2024, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 181, 180, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 182, 44, new DateTime(2024, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 183, 296, new DateTime(2024, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 184, 115, new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 185, 29, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 186, 263, new DateTime(2024, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 187, 80, new DateTime(2024, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 188, 170, new DateTime(2024, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 189, 235, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 190, 10, new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 191, 208, new DateTime(2024, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 192, 41, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 193, 291, new DateTime(2024, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 194, 120, new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 195, 30, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 196, 277, new DateTime(2024, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 197, 85, new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 198, 155, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 199, 248, new DateTime(2024, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 200, 15, new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PizzaRecipes",
                columns: new[] { "Id", "Ingredients", "Name", "Steps" },
                values: new object[,]
                {
                    { 1, "1 kula ciasta na pizzę (ok. 250g), 3 łyżki sosu pomidorowego, 125g mozzarelli kulki, świeża bazylia, oliwa z oliwek extra virgin", "Pizza Margherita Klasyczna", "1. Rozgrzej piekarnik do 250°C z kamieniem do pizzy lub blachą. 2. Na rozwałkowanym cieście rozsmaruj 3 łyżki sosu pomidorowego, zostawiając brzegi. 3. Rozłóż równomiernie 125g mozzarelli. 4. Piecz pizzę przez 8-12 minut, aż brzegi będą złociste, a ser roztopiony i lekko przypieczony. 5. Po upieczeniu udekoruj świeżymi liśćmi bazylii i skrop oliwą z oliwek." },
                    { 2, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 50g salami pepperoni, 100g mozzarelli, 1/2 czerwonej papryczki chili (świeżej lub marynowanej), 1/4 czerwonej cebuli", "Pizza Diavola Piekielna", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 50g salami pepperoni, 1/2 pokrojonej czerwonej papryczki chili i 1/4 pokrojonej cebuli. 4. Posyp 100g mozzarelli. 5. Piecz przez 10-14 minut, aż brzegi będą chrupiące, a składniki apetycznie zrumienione." },
                    { 3, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 70g szynki gotowanej (lub prosciutto cotto), 50g pieczarek świeżych (pokrojonych), 4 kawałki karczochów w zalewie (pokrojone), 8-10 czarnych oliwek", "Pizza Capricciosa Królewska", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 70g szynki gotowanej, 50g pieczarek, 4 kawałki karczochów i 8-10 czarnych oliwek. 4. Posyp 120g mozzarelli. 5. Piecz przez 10-15 minut, aż składniki będą gorące, a ser pięknie roztopiony." },
                    { 4, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 1/2 papryki (czerwonej/żółtej), 1/4 czerwonej cebuli, 50g pieczarek, 6-8 pomidorków koktajlowych", "Pizza Vegetariana Ogrodowa", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 1/2 pokrojonej papryki (kolorowej), 1/4 pokrojonej czerwonej cebuli, 50g pieczarek, kilka pomidorków koktajlowych przekrojonych na pół. 4. Posyp 120g mozzarelli. 5. Piecz przez 10-14 minut, aż warzywa będą miękkie, a ser złotobrązowy." },
                    { 5, "1 kula ciasta na pizzę, 2 łyżki śmietany kremówki (30%), 80g boczku wędzonego (lub guanciale), 100g startego sera Pecorino Romano, 1 jajko, świeżo mielony czarny pieprz", "Pizza Carbonara Kremowa", "1. Rozgrzej piekarnik do 230°C. 2. Boczek pokrój w kostkę i podsmaż na patelni, aż będzie chrupiący. Odstaw na bok. 3. Na cieście rozsmaruj cienką warstwę sosu śmietanowego. 4. Posyp 100g startego Pecorino Romano. 5. Rozłóż podsmażony boczek. 6. Wbij 1 jajko na środek pizzy (lub roztrzep je i polej pizzę). 7. Piecz przez 10-13 minut, aż brzegi będą złociste, a jajko zetnie się (jeśli używasz całości). 8. Po upieczeniu posyp świeżo mielonym pieprzem." },
                    { 6, "1 kula ciasta na pizzę, 60g mozzarelli, 30g gorgonzoli, 30g provolone, 30g parmezanu (startego), (opcjonalnie: 2 łyżki sosu pomidorowego)", "Pizza Quattro Formaggi Bogini", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj cienką warstwę sosu pomidorowego (opcjonalnie, można pominąć dla białej pizzy). 3. Rozłóż równomiernie 60g mozzarelli, 30g gorgonzoli, 30g provolone i 30g parmezanu. 4. Piecz przez 9-12 minut, aż sery się roztopią i lekko zrumienią, tworząc kremową powierzchnię." },
                    { 7, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 70g pieczarek, 60g prosciutto cotto (lub szynki gotowanej)", "Pizza Prosciutto e Funghi Leśna", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 100g mozzarelli. 4. Rozłóż 70g pieczarek (świeżych, pokrojonych w plastry) i 60g prosciutto cotto (lub innej gotowanej szynki). 5. Piecz przez 10-14 minut, aż brzegi będą złociste, a składniki apetycznie zrumienione." },
                    { 8, "1 kula ciasta na pizzę, 100g piersi z kurczaka (ugotowanej/grillowanej), 3 łyżki sosu BBQ, 120g mozzarelli, 1/4 czerwonej cebuli, świeża kolendra do dekoracji", "Pizza Pollo BBQ Dymna", "1. Rozgrzej piekarnik do 240°C. 2. Kurczaka pokrój w kostkę i wymieszaj z 2 łyżkami sosu BBQ. 3. Na cieście rozsmaruj 3 łyżki sosu BBQ. 4. Rozłóż 120g mozzarelli. 5. Rozłóż kawałki kurczaka i 1/4 pokrojonej czerwonej cebuli. 6. Piecz przez 10-15 minut, aż ser się roztopi, a kurczak będzie gorący. 7. Po upieczeniu posyp świeżą kolendrą." },
                    { 9, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 80g tuńczyka w oliwie (odsączonego), 1/4 czerwonej cebuli, 1 łyżka kaparów, świeża natka pietruszki", "Pizza Tonno Oceaniczna", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 100g mozzarelli. 4. Rozłóż 80g tuńczyka z puszki (odsączonego), 1/4 pokrojonej czerwonej cebuli i 1 łyżkę kaparów. 5. Piecz przez 9-12 minut, aż ser się roztopi, a składniki będą gorące. 6. Po upieczeniu posyp świeżą natką pietruszki." },
                    { 10, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 100g mieszanki owoców morza (mrożonej lub świeżej), 1 ząbek czosnku, oliwa z oliwek", "Pizza Frutti di Mare Morska", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego z 1 ząbkiem czosnku (posiekanym). 3. Rozłóż 100g mozzarelli. 4. Rozłóż 100g mieszanki owoców morza (krewetki, kalmary, małże). 5. Piecz przez 12-16 minut, aż owoce morza będą ugotowane, a ser złocisty. 6. Po upieczeniu skrop oliwą z oliwek." },
                    { 11, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 70g świeżego szpinaku, 1 jajko, (opcjonalnie: starty parmezan do posypania)", "Pizza Spinaci e Uovo Zielona", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 100g mozzarelli i 70g świeżego szpinaku (blanszowanego i odciśniętego). 4. Na środku pizzy wbij 1 jajko. 5. Piecz przez 10-14 minut, aż brzegi będą złociste, a jajko zetnie się do preferowanego stopnia." },
                    { 12, "1 kula ciasta na pizzę, 100g świeżej kiełbasy salsiccia, 80g friarielli (lub brokułów rabe), 120g mozzarelli, (opcjonalnie: 2 łyżki sosu pomidorowego, 1 ząbek czosnku)", "Pizza Salsiccia e Friarielli Neapolitańska", "1. Rozgrzej piekarnik do 250°C. 2. Kiełbasę salsiccia (lub inną świeżą kiełbasę) wyciśnij z osłonki i podsmaż na patelni, rozdrabniając. 3. Na cieście rozsmaruj cienką warstwę sosu pomidorowego (lub oliwy z oliwek). 4. Rozłóż 120g mozzarelli. 5. Rozłóż podsmażoną kiełbasę i 80g friarielli (lub brokułów rabe, lekko podsmażonych z czosnkiem). 6. Piecz przez 10-15 minut, aż brzegi będą chrupiące, a składniki gorące." },
                    { 13, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 50g pieczarek, 50g szynki gotowanej, 4 kawałki karczochów w zalewie, 6-8 czarnych oliwek", "Pizza Quattro Stagioni Cztery Pory Roku", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego i rozłóż 120g mozzarelli. 3. Podziel pizzę na cztery części. W pierwszej części ułóż 50g pieczarek, w drugiej 50g szynki gotowanej, w trzeciej 4 kawałki karczochów, a w czwartej 6-8 czarnych oliwek. 4. Piecz przez 10-15 minut, aż brzegi będą złociste, a ser roztopiony." },
                    { 14, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 50g bresaoli, garść rukoli, 30g płatków Grana Padano, oliwa z oliwek extra virgin", "Pizza Bresaola Rucola e Grana Wykwintna", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego i rozłóż 100g mozzarelli. 3. Piecz przez 8-10 minut, aż brzegi będą złociste, a ser roztopiony. 4. Po wyjęciu z piekarnika rozłóż 50g cienkich plasterków bresaoli, garść świeżej rukoli i posyp 30g płatków Grana Padano. Skrop oliwą." },
                    { 15, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 30g suszonych borowików (przed namoczeniem), (opcjonalnie: świeża natka pietruszki)", "Pizza Funghi Porcini Borowikowa", "1. Rozgrzej piekarnik do 250°C. 2. Suszone borowiki namocz w ciepłej wodzie przez 20 minut, następnie odcedź i pokrój. 3. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 4. Rozłóż 120g mozzarelli i 70g namoczonych borowików. 5. Piecz przez 10-15 minut, aż brzegi będą chrupiące, a grzyby upieczone." },
                    { 16, "1 kula ciasta na pizzę, 2 łyżki sera ricotta, 100g mozzarelli, 50g prosciutto crudo, 2-3 świeże figi, glazura balsamiczna", "Pizza Crudo e Fichi Figowa", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj cienką warstwę sera ricotta. 3. Rozłóż 100g mozzarelli. 4. Piecz przez 8-10 minut, aż brzegi będą złociste. 5. Po wyjęciu z piekarnika rozłóż 50g prosciutto crudo i 2-3 świeże figi (pokrojone w plastry). Skrop octem balsamicznym." },
                    { 17, "1 kula ciasta na pizzę, 1 średni ziemniak, 100g mozzarelli, 1 ząbek czosnku, świeży rozmaryn, oliwa z oliwek extra virgin", "Pizza Patate e Rosmarino Ziemniaczana", "1. Rozgrzej piekarnik do 250°C. 2. Ziemniaki obierz, ugotuj al dente i pokrój w cienkie plastry. 3. Na cieście rozsmaruj cienką warstwę oliwy z oliwek i posyp 1 ząbkiem posiekanego czosnku. 4. Rozłóż 100g mozzarelli. 5. Ułóż plastry ziemniaków i posyp świeżym rozmarynem. 6. Piecz przez 12-16 minut, aż brzegi będą chrupiące, a ziemniaki lekko zrumienione." },
                    { 18, "1 kula ciasta na pizzę, 3 łyżki sosu z pomidorów San Marzano, 125g mozzarelli di bufala, świeża bazylia, oliwa z oliwek extra virgin", "Pizza Napoletana Prawdziwa", "1. Rozgrzej piekarnik do 280-300°C (najwyższa możliwa temperatura). 2. Na cieście rozsmaruj 3 łyżki sosu z pomidorów San Marzano. 3. Rozłóż 125g mozzarelli di bufala (porwanej na kawałki). 4. Piecz przez 5-8 minut, aż brzegi będą mocno wypieczone i puszyste, a ser roztopiony. 5. Po upieczeniu udekoruj świeżą bazylią i skrop oliwą." },
                    { 19, "1 kula ciasta na pizzę, 100g świeżych krewetek (obranych), 1 mała cukinia, 100g mozzarelli, 1 ząbek czosnku, (opcjonalnie: 2 łyżki sosu pomidorowego lub oliwa)", "Pizza Gamberi e Zucchine Krewetkowa", "1. Rozgrzej piekarnik do 240°C. 2. Krewetki podsmaż krótko z czosnkiem. Cukinię pokrój w cienkie plastry lub zetrzyj na mandolinie. 3. Na cieście rozsmaruj cienką warstwę sosu pomidorowego lub oliwy. 4. Rozłóż 100g mozzarelli. 5. Ułóż krewetki i plastry cukinii. 6. Piecz przez 10-14 minut, aż brzegi będą złociste, a krewetki ugotowane." },
                    { 20, "1 kula ciasta na pizzę, 2 łyżki sera stracchino (lub ricotty), 120g mozzarelli, 80g porchetty (cienkie plastry), 20g pistacji (niesolonych, posiekanych), oliwa z oliwek", "Pizza Porchetta i Pistacje Rzymska", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj cienką warstwę sera stracchino (lub ricotty). 3. Rozłóż 120g mozzarelli. 4. Rozłóż 80g cienkich plasterków porchetty. 5. Piecz przez 10-14 minut, aż brzegi będą chrupiące, a ser roztopiony. 6. Po upieczeniu posyp posiekanymi pistacjami i skrop oliwą." },
                    { 21, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 60g specku, 80g sera brie", "Pizza Speck e Brie Tyrolska", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 100g mozzarelli. 4. Rozłóż 60g specku (cienkie plastry) i 80g sera brie (pokrojonego w kawałki). 5. Piecz przez 10-14 minut, aż brzegi będą złociste, a ser brie delikatnie się roztopi." },
                    { 22, "1 kula ciasta na pizzę, 100g mozzarelli, 70g gorgonzoli, 30g orzechów włoskich, oliwa z oliwek extra virgin, (opcjonalnie: miód)", "Pizza Gorgonzola e Noci Orzechowa", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj cienką warstwę oliwy z oliwek. 3. Rozłóż 100g mozzarelli i 70g sera gorgonzola (pokruszonego). 4. Posyp 30g posiekanych orzechów włoskich. 5. Piecz przez 10-12 minut, aż brzegi będą złociste, a sery roztopione. 6. Po upieczeniu skrop miodem (opcjonalnie)." },
                    { 23, "1 kula ciasta na pizzę, 120g mozzarelli, 80g mortadeli (cienkie plastry), 20g pistacji (posiekanych), (opcjonalnie: 2 łyżki sosu pomidorowego lub oliwa)", "Pizza Mortadella e Pistacchio Bolońska", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj cienką warstwę sosu pomidorowego (opcjonalnie) lub oliwy. 3. Rozłóż 120g mozzarelli. 4. Piecz przez 8-10 minut, aż brzegi będą złociste, a ser roztopiony. 5. Po wyjęciu z piekarnika ułóż 80g cienkich plasterków mortadeli i posyp 20g posiekanych pistacji." },
                    { 24, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 1/2 czerwonej papryki, 1/2 żółtej papryki, 1/2 zielonej papryki, oliwa z oliwek", "Pizza ai Peperoni Colorata", "1. Rozgrzej piekarnik do 250°C. 2. Papryki pokrój w paski i krótko podsmaż na patelni z odrobiną oliwy. 3. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 4. Rozłóż 120g mozzarelli. 5. Rozłóż podsmażone paski papryki (czerwonej, żółtej, zielonej). 6. Piecz przez 10-14 minut, aż brzegi będą chrupiące, a warzywa miękkie." },
                    { 25, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 80g pancetty, 1 średnia cebula", "Pizza Pancetta e Cipolla Włoska Wieś", "1. Rozgrzej piekarnik do 240°C. 2. Pancettę pokrój w kostkę i podsmaż na patelni, aż będzie chrupiąca. Cebulę pokrój w piórka i zeszklij. 3. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 4. Rozłóż 100g mozzarelli. 5. Rozłóż podsmażoną pancettę i zeszkloną cebulę. 6. Piecz przez 10-14 minut, aż brzegi będą złociste, a składniki apetycznie zrumienione." },
                    { 26, "1 kula ciasta na pizzę, 100g mozzarelli, 70g gorgonzoli, 1/2 gruszki (twardej), 30g orzechów włoskich, oliwa z oliwek", "Pizza Pera e Gorgonzola Słodko-Słona", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj cienką warstwę oliwy z oliwek. 3. Rozłóż 100g mozzarelli i 70g sera gorgonzola (pokruszonego). 4. Ułóż cienkie plastry 1/2 gruszki. 5. Piecz przez 10-12 minut, aż brzegi będą złociste, a sery roztopione. 6. Po upieczeniu posyp posiekanymi orzechami włoskimi." },
                    { 27, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 1/2 cukinii, 1/2 papryki (dowolny kolor), 1/4 bakłażana, oliwa z oliwek", "Pizza con Verdure Grigliate Grillowana", "1. Rozgrzej piekarnik do 240°C. 2. Warzywa (cukinię, paprykę, bakłażan) pokrój w plastry i zgrilluj na patelni grillowej. 3. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 4. Rozłóż 120g mozzarelli. 5. Ułóż grillowane warzywa. 6. Piecz przez 10-14 minut, aż brzegi będą złociste, a warzywa miękkie." },
                    { 28, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 100g mieszanki świeżych grzybów, 1 ząbek czosnku, oliwa z oliwek", "Pizza ai Funghi Misti Grzybowa", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 120g mozzarelli. 4. Rozłóż 100g mieszanki grzybów (np. pieczarki, boczniaki, shiitake), podsmażonych krótko z czosnkiem. 5. Piecz przez 10-15 minut, aż brzegi będą chrupiące, a grzyby upieczone." },
                    { 29, "1 kula ciasta na pizzę, 1 burrata (125g), oliwa truflowa, (opcjonalnie: świeża trufla, szczypta soli morskiej)", "Pizza con Burrata i Trufla Luksusowa", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj cienką warstwę oliwy z oliwek. 3. Piecz przez 8-10 minut, aż brzegi będą złociste. 4. Po wyjęciu z piekarnika ułóż na środku całą kulę burraty (rozerwaną na kawałki). 5. Skrop oliwą truflową i posyp płatkami świeżej trufli (jeśli dostępna) lub posiekanym szczypiorkiem." },
                    { 30, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 50g anchois w oliwie, 10-12 czarnych oliwek, suszone oregano", "Pizza Romana Chrupiąca", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 100g mozzarelli i 50g anchois (pokrojone na kawałki). 4. Rozłóż 10-12 czarnych oliwek i posyp odrobiną oregano. 5. Piecz przez 10-14 minut, aż brzegi będą chrupiące, a ser roztopiony." },
                    { 31, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 50g zielonego groszku, 50g szparagów, 6-8 pomidorków koktajlowych, garść rukoli", "Pizza Primavera Wiosenna", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 120g mozzarelli. 4. Ułóż 50g zielonego groszku (świeżego lub mrożonego), 50g szparagów (pokrojonych na kawałki), kilka pomidorków koktajlowych i garść rukoli. 5. Piecz przez 10-14 minut, aż brzegi będą złociste, a warzywa al dente." },
                    { 32, "1 kula ciasta na pizzę, 2 łyżki serka śmietankowego (np. Philadelphia), 100g mozzarelli, 80g wędzonego łososia, świeży koperek, oliwa z oliwek", "Pizza Al Salmone i Koperkowa", "1. Rozgrzej piekarnik do 230°C. 2. Na cieście rozsmaruj cienką warstwę serka śmietankowego lub kremowego sera. 3. Rozłóż 100g mozzarelli. 4. Piecz przez 8-10 minut, aż brzegi będą złociste. 5. Po wyjęciu z piekarnika ułóż 80g wędzonego łososia i posyp świeżym koperkiem. Skrop oliwą." },
                    { 33, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 70g pikantnego salami calabrese, 1/4 czerwonej cebuli", "Pizza Calabrese Salami Piccante", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 120g mozzarelli. 4. Ułóż 70g pikantnego salami calabrese i 1/4 pokrojonej czerwonej cebuli. 5. Piecz przez 10-14 minut, aż brzegi będą chrupiące, a składniki apetycznie zrumienione." },
                    { 34, "1 kula ciasta na pizzę, 1 średni ziemniak, 100g świeżej kiełbasy salsiccia, 100g mozzarelli, 1 ząbek czosnku, oliwa z oliwek extra virgin", "Pizza Bianca Patata e Salsiccia Rustykalna", "1. Rozgrzej piekarnik do 240°C. 2. Ziemniaki obierz, pokrój w cienkie plastry i podgotuj al dente. Świeżą kiełbasę wyciśnij z osłonki i podsmaż. 3. Na cieście rozsmaruj cienką warstwę oliwy z oliwek i posyp 1 ząbkiem posiekanego czosnku. 4. Rozłóż 100g mozzarelli. 5. Ułóż plastry ziemniaków i podsmażoną kiełbasę. 6. Piecz przez 12-16 minut, aż brzegi będą chrupiące, a składniki ładnie zarumienione." },
                    { 35, "1 kula ciasta na pizzę, 100g mozzarelli, 60g specku, 1/2 gruszki (twardej), 30g orzechów włoskich, oliwa z oliwek", "Pizza Speck, Pere e Noci Góralska", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj cienką warstwę oliwy z oliwek. 3. Rozłóż 100g mozzarelli, 60g specku (cienkie plastry) i cienkie plastry 1/2 gruszki. 4. Posyp 30g orzechów włoskich. 5. Piecz przez 10-14 minut, aż brzegi będą złociste, a składniki apetycznie zapieczone." },
                    { 36, "1 kula ciasta na pizzę, 3 łyżki pesto genueńskiego, 120g mozzarelli, 6-8 pomidorków koktajlowych, płatki parmezanu", "Pizza al Pesto Genueńska", "1. Rozgrzej piekarnik do 230°C. 2. Na cieście rozsmaruj 3 łyżki gotowego pesto genueńskiego. 3. Rozłóż 120g mozzarelli. 4. Piecz przez 9-12 minut, aż brzegi będą złociste, a ser roztopiony. 5. Po upieczeniu posyp świeżymi pomidorkami koktajlowymi przekrojonymi na pół i płatkami parmezanu." },
                    { 37, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 8 szparagów, 60g pancetty", "Pizza Asparagi e Pancetta Szparagowa", "1. Rozgrzej piekarnik do 240°C. 2. Szparagi (około 8 sztuk) ugotuj al dente i pokrój na mniejsze kawałki. Pancettę pokrój w kostkę i podsmaż. 3. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 4. Rozłóż 120g mozzarelli. 5. Ułóż szparagi i podsmażoną pancettę. 6. Piecz przez 10-14 minut, aż brzegi będą złociste, a składniki gorące." },
                    { 38, "1 kula ciasta na pizzę, 100g mozzarelli, 8-10 świeżych kwiatów cukinii, 6-8 filetów anchois, 1 ząbek czosnku, oliwa z oliwek", "Pizza ai Fiori di Zucca i Anchois Letnia", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj cienką warstwę oliwy z oliwek i posyp 1 ząbkiem posiekanego czosnku. 3. Rozłóż 100g mozzarelli. 4. Ułóż 8-10 kwiatów cukinii i 6-8 filetów anchois (pokrojone). 5. Piecz przez 9-12 minut, aż brzegi będą złociste, a składniki apetycznie zapieczone." },
                    { 39, "1 kula ciasta na pizzę, 2 łyżki śmietany kremówki (30%), 80g boczku wędzonego, 50g grzybów leśnych, 100g startego sera Pecorino Romano, 1 ząbek czosnku, świeżo mielony czarny pieprz", "Pizza Carbonara z Grzybami Leśnymi", "1. Rozgrzej piekarnik do 230°C. 2. Boczek pokrój w kostkę i podsmaż. Grzyby leśne (świeże lub suszone i namoczone) podsmaż z czosnkiem. 3. Na cieście rozsmaruj cienką warstwę sosu śmietanowego. 4. Posyp 100g startego Pecorino Romano. 5. Rozłóż podsmażony boczek i grzyby. 6. Piecz przez 10-14 minut, aż brzegi będą złociste, a składniki gorące. 7. Po upieczeniu posyp świeżo mielonym pieprzem." },
                    { 40, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 80g tuńczyka w oliwie (odsączonego), 10-12 pomidorków koktajlowych, świeża natka pietruszki", "Pizza al Tonno i Pomodorini Letnia", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 100g mozzarelli. 4. Ułóż 80g tuńczyka z puszki (odsączonego) i 10-12 pomidorków koktajlowych przekrojonych na pół. 5. Piecz przez 9-12 minut, aż brzegi będą złociste, a składniki gorące. 6. Po upieczeniu udekoruj świeżą natką pietruszki." },
                    { 41, "1 kula ciasta na pizzę, 1 średnia cukinia, 100g krewetek (małych, obranych), 100g mozzarelli, 1 ząbek czosnku, oliwa z oliwek", "Pizza Zucchine e Gamberetti Delikatna", "1. Rozgrzej piekarnik do 240°C. 2. Cukinię pokrój w cienkie plastry. Krewetki podsmaż krótko z czosnkiem. 3. Na cieście rozsmaruj cienką warstwę oliwy z oliwek. 4. Rozłóż 100g mozzarelli. 5. Ułóż plastry cukinii i podsmażone krewetki. 6. Piecz przez 10-14 minut, aż brzegi będą złociste, a krewetki ugotowane." },
                    { 42, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 60g specku, 70g pieczarek", "Pizza Speck e Funghi Mieszana", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 120g mozzarelli. 4. Ułóż 60g specku (cienkie plastry) i 70g pieczarek (pokrojonych). 5. Piecz przez 10-14 minut, aż brzegi będą złociste, a składniki apetycznie zrumienione." },
                    { 43, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 50g salami, 50g szynki, 50g pieczarek, 50g karczochów", "Pizza ai Quattro Gusti Cztery Smaki", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego i rozłóż 120g mozzarelli. 3. Podziel pizzę na cztery części. W pierwszej ułóż 50g salami, w drugiej 50g szynki, w trzeciej 50g pieczarek, a w czwartej 50g karczochów. 4. Piecz przez 10-15 minut, aż brzegi będą złociste, a ser roztopiony." },
                    { 44, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 1 średni bakłażan, 30g startego parmezanu, oliwa z oliwek", "Pizza Melanzane e Parmigiano Bakłażanowa", "1. Rozgrzej piekarnik do 240°C. 2. Bakłażan pokrój w plastry i zgrilluj lub podsmaż na oliwie, aż będzie miękki. 3. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 4. Rozłóż 120g mozzarelli. 5. Ułóż grillowane plastry bakłażana i posyp 30g startego parmezanu. 6. Piecz przez 10-15 minut, aż brzegi będą złociste, a składniki apetycznie zapieczone." },
                    { 45, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 100g świeżej kiełbasy salsiccia, 70g pieczarek", "Pizza Funghi e Salsiccia Grzybowa z Kiełbasą", "1. Rozgrzej piekarnik do 250°C. 2. Świeżą kiełbasę wyciśnij z osłonki i podsmaż. Pieczarki pokrój i podsmaż. 3. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 4. Rozłóż 120g mozzarelli. 5. Rozłóż podsmażoną kiełbasę i pieczarki. 6. Piecz przez 10-15 minut, aż brzegi będą chrupiące, a składniki gorące." },
                    { 46, "1 kula ciasta na pizzę, 3 łyżki sosu z pomidorów San Marzano, 125g mozzarelli di bufala, 10-12 pomidorków koktajlowych, świeża bazylia, oliwa z oliwek extra virgin", "Pizza Bufala e Pomodorini Świeża", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu z pomidorów San Marzano. 3. Rozłóż 125g mozzarelli di bufala (porwanej na kawałki) i 10-12 pomidorków koktajlowych przekrojonych na pół. 4. Piecz przez 8-12 minut, aż brzegi będą puszyste, a ser roztopiony. 5. Po upieczeniu udekoruj świeżą bazylią i skrop oliwą." },
                    { 47, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 2-3 parówki (würstel), 100g mrożonych frytek", "Pizza Würstel i Frytki Niezwykła", "1. Rozgrzej piekarnik do 240°C. 2. Parówki (würstel) pokrój w plasterki. Frytki usmaż lub upiecz zgodnie z instrukcją. 3. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 4. Rozłóż 120g mozzarelli. 5. Ułóż pokrojone parówki i frytki. 6. Piecz przez 10-15 minut, aż brzegi będą złociste, a frytki chrupiące." },
                    { 48, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 100g mozzarelli, 80g tuńczyka w oliwie (odsączonego), 1/2 czerwonej cebuli", "Pizza Tonno e Cipolla Klasyczna", "1. Rozgrzej piekarnik do 250°C. 2. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 3. Rozłóż 100g mozzarelli. 4. Rozłóż 80g tuńczyka z puszki (odsączonego) i 1/2 pokrojonej czerwonej cebuli. 5. Piecz przez 9-12 minut, aż brzegi będą złociste, a składniki gorące." },
                    { 49, "1 kula ciasta na pizzę, 3 łyżki sosu pomidorowego, 120g mozzarelli, 2 hot dogi, 3-4 plasterki korniszonów", "Pizza Americana z Hot Dogami", "1. Rozgrzej piekarnik do 240°C. 2. Hot dogi pokrój w plasterki. 3. Na cieście rozsmaruj 3 łyżki sosu pomidorowego. 4. Rozłóż 120g mozzarelli. 5. Ułóż pokrojone hot dogi i kilka plasterków korniszonów. 6. Piecz przez 10-14 minut, aż brzegi będą złociste, a ser roztopiony." },
                    { 50, "1 kula ciasta na pizzę, 60g mozzarelli, 30g gorgonzoli dolce, 30g ricotty, 30g sera mascarpone, 1 łyżka miodu, 20g orzechów włoskich, oliwa z oliwek", "Pizza Quattro Formaggi Dolce Słodka", "1. Rozgrzej piekarnik do 240°C. 2. Na cieście rozsmaruj cienką warstwę oliwy z oliwek. 3. Rozłóż równomiernie 60g mozzarelli, 30g gorgonzoli dolce (słodka), 30g ricotty i 30g sera mascarpone. 4. Piecz przez 9-12 minut, aż sery się roztopią i lekko zrumienią, tworząc kremową powierzchnię. 5. Po upieczeniu skrop miodem i posyp posiekanymi orzechami włoskimi." }
                });

            migrationBuilder.InsertData(
                table: "Pizzas",
                columns: new[] { "Id", "AltText", "ImageUrl", "IsPizzaOfTheWeek", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { 1, "", "images/pizzas/ClassicMargheritaPizza.jpg", false, "Poczuj smak prawdziwej Italii z naszą Pizzą Margherita Klasyczną. Delikatny sos z pomidorów San Marzano, kremowa  mozzarella i świeża bazylia tworzą niezapomniane połączenie, które zadowoli każdego miłośnika pizzy.", "Pizza Margherita Klasyczna", 28.50m, "Włoska klasyka z pomidorami San Marzano, mozzarellą i świeżą bazylią." },
                    { 2, "", "images/pizzas/FieryPizzaInferno.jpg", true, "Dla fanów ostrych wrażeń! Nasza Pizza Diavola Piekielna to połączenie wyrazistego salami pepperoni, ognistych papryczek jalapeńo i starannie dobranej mieszanki przypraw, która rozpali Twoje kubki smakowe.", "Pizza Diavola Piekielna", 34.00m, "Pikantna pizza z salami pepperoni, papryczkami jalapeño i nutą ostrości." },
                    { 3, "", "images/pizzas/GourmetPizzaCapricciosa.jpg", false, "Prawdziwie królewska uczta! Pizza Capricciosa Królewska to hojnie obłożona kompozycja najwyższej jakości szynki, świeżych pieczarek, delikatnych karczochów i aromatycznych czarnych oliwek. Idealna dla tych, którzy cenią sobie różnorodność.", "Pizza Capricciosa Królewska", 37.50m, "Bogactwo smaków: szynka, pieczarki, karczochy i czarne oliwki." },
                    { 4, "", "images/pizzas/RusticGardenPizza.jpg", false, "Lekka i pełna smaku! Nasza Pizza Vegetariana Ogrodowa to celebracja świeżości. Chrupiąca papryka, słodka cebula, aromatyczne pieczarki i soczyste pomidorki koktajlowe tworzą idealną harmonię dla wegetariańskiego podniebienia.", "Pizza Vegetariana Ogrodowa", 32.00m, "Świeże warzywa z ogrodu: papryka, cebula, pieczarki i pomidorki koktajlowe." },
                    { 5, "", "images/pizzas/RomanCarbonaraPizza.jpg", true, "Przenieś się do Rzymu z naszą Pizzą Carbonara Kremowa! To innowacyjne połączenie chrupiącego boczku, aksamitnego sosu na bazie jajka i sera pecorino, posypane świeżo mielonym pieprzem. Smak, który zaskakuje i zachwyca.", "Pizza Carbonara Kremowa", 39.00m, "Inspirowana włoskim klasykiem: boczek, jajko, ser pecorino i pieprz." },
                    { 6, "", "images/pizzas/ExquisiteCheeseSymphony.jpg", false, "Dla prawdziwych smakoszy sera! Nasza Pizza Quattro Formaggi Bogini to symfonia smaków, gdzie delikatna mozzarella, wyrazista gorgonzola, łagodne provolone i pikantny parmezan łączą się w kremową i niezapomnianą całość.", "Pizza Quattro Formaggi Bogini", 36.00m, "Harmonia czterech serów: mozzarella, gorgonzola, provolone i parmezan." },
                    { 7, "", "images/pizzas/WoodlandPizzaDelight.jpg", true, "Odkryj leśny aromat w naszej Pizzzy Prosciutto e Funghi Leśnej. Słona szynka dojrzewająca idealnie komponuje się z soczystymi pieczarkami, tworząc pizzę o głębokim i satysfakcjonującym smaku.", "Pizza Prosciutto e Funghi Leśna", 35.50m, "Klasyczne połączenie szynki dojrzewającej i świeżych pieczarek." },
                    { 8, "", "images/pizzas/BBQChickenPizzaDelight.jpg", false, "Poczuj dymny aromat naszej Pizzzy Pollo BBQ Dymnej. Delikatny kurczak w pikantnym sosie BBQ, słodka czerwona cebula i świeża kolendra to idealna propozycja dla miłośników amerykańskich smaków.", "Pizza Pollo BBQ Dymna", 38.00m, "Soczysty kurczak w sosie BBQ, czerwona cebula i kolendra." },
                    { 9, "", "images/pizzas/GourmetTunaPizzaDelight.jpg", false, "Zanurz się w smaku oceanu z naszą Pizzą Tonno Oceaniczną. Soczysty tuńczyk, chrupiąca czerwona cebula, słone kapary i świeża natka pietruszki tworzą orzeźwiającą i wyjątkową kompozycję.", "Pizza Tonno Oceaniczna", 33.00m, "Tuńczyk, czerwona cebula, kapary i świeża natka pietruszki." },
                    { 10, "", "images/pizzas/MediterraneanSeafoodPizza.jpg", false, "Prawdziwa gratka dla miłośników morskich przysmaków! Pizza Frutti di Mare Morska to luksusowe połączenie świeżych krewetek, delikatnych kalmarów, aromatycznych małży i wyrazistego czosnku, które przeniesie Cię na śródziemnomorskie wybrzeże.", "Pizza Frutti di Mare Morska", 45.00m, "Bogactwo owoców morza: krewetki, kalmary, małże i czosnek." },
                    { 11, "", "images/pizzas/TropicalHawaiianPizza.jpg", true, "Poczuj tropikalny powiew z naszą Pizzą Hawai Tropikalną! Kontrowersyjne, ale uwielbiane połączenie soczystej szynki i słodkiego ananasa na klasycznym sosie pomidorowym, które przeniesie Cię na słoneczną wyspę.", "Pizza Hawai Tropikalna", 31.00m, "Słodko-słona harmonia: szynka, ananas i sos pomidorowy." },
                    { 12, "", "images/pizzas/GreenThemedGourmetPizza.jpg", false, "Świeżość i lekkość w każdym kawałku! Pizza Spinaci e Ricotta Zielona to połączenie aksamitnego szpinaku, delikatnej ricotty i aromatycznego czosnku. Idealna propozycja dla tych, którzy cenią sobie zdrowe i pyszne jedzenie.", "Pizza Spinaci e Ricotta Zielona", 34.50m, "Delikatny szpinak, kremowa ricotta i czosnek." },
                    { 13, "", "images/pizzas/FieryCalabresePizzaDelight.jpg", false, "Dla odważnych podniebień! Pizza Calabrese Pikantna to eksplozja smaku dzięki wyrazistemu salami z Kalabrii, chrupiącej czerwonej cebuli i ognistym papryczkom chili. Poczuj południowy temperament w każdym kęsie.", "Pizza Calabrese Pikantna", 39.00m, "Ostry salami z Kalabrii, czerwona cebula i papryczki chili." },
                    { 14, "", "images/pizzas/GourmetMushroomPizzaElegance.jpg", false, "Odkryj elegancję w prostocie! Pizza Bianca Grzybowa to wyjątkowa propozycja bez sosu pomidorowego. Delikatny sos śmietanowy, aromatyczny mix grzybów leśnych i subtelna nuta trufli tworzą wyrafinowane danie dla prawdziwych koneserów.", "Pizza Bianca Grzybowa", 36.50m, "Bez sosu pomidorowego: śmietanowy sos, mix grzybów leśnych i trufla." },
                    { 15, "", "images/pizzas/RusticCountryPizzaDelight.jpg", true, "Smak prosto z wiejskiego stołu! Pizza Rustica Wiejska to sycące połączenie pikantnej kiełbasy, chrupiącego boczku, słodkiej cebuli i aromatycznych pieczarek. Idealna dla tych, którzy cenią sobie tradycyjne i konkretne smaki.", "Pizza Rustica Wiejska", 38.00m, "Kiełbasa, boczek, cebula i pieczarki." }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "Amount", "OrderId", "PizzaId" },
                values: new object[,]
                {
                    { 1, 3, 1, 10 },
                    { 2, 2, 1, 1 },
                    { 3, 4, 1, 14 },
                    { 4, 1, 2, 7 },
                    { 5, 3, 2, 3 },
                    { 6, 2, 2, 11 },
                    { 7, 1, 2, 5 },
                    { 8, 4, 3, 15 },
                    { 9, 1, 3, 8 },
                    { 10, 3, 3, 2 },
                    { 11, 2, 3, 12 },
                    { 12, 1, 4, 4 },
                    { 13, 3, 4, 9 },
                    { 14, 2, 5, 6 },
                    { 15, 1, 5, 13 },
                    { 16, 4, 5, 1 },
                    { 17, 3, 6, 11 },
                    { 18, 1, 6, 7 },
                    { 19, 2, 7, 10 },
                    { 20, 4, 7, 15 },
                    { 21, 1, 7, 3 },
                    { 22, 3, 7, 8 },
                    { 23, 2, 8, 14 },
                    { 24, 1, 8, 2 },
                    { 25, 4, 9, 12 },
                    { 26, 3, 9, 6 },
                    { 27, 1, 9, 9 },
                    { 28, 2, 10, 1 },
                    { 29, 4, 10, 4 },
                    { 30, 1, 10, 13 },
                    { 31, 3, 10, 7 },
                    { 32, 2, 10, 11 },
                    { 33, 1, 11, 5 },
                    { 34, 4, 11, 10 },
                    { 35, 3, 12, 15 },
                    { 36, 1, 12, 2 },
                    { 37, 2, 12, 8 },
                    { 38, 4, 13, 12 },
                    { 39, 1, 13, 3 },
                    { 40, 2, 14, 9 },
                    { 41, 4, 14, 6 },
                    { 42, 1, 14, 14 },
                    { 43, 3, 14, 1 },
                    { 44, 2, 15, 13 },
                    { 45, 1, 15, 5 },
                    { 46, 4, 15, 10 },
                    { 47, 3, 15, 11 },
                    { 48, 2, 16, 4 },
                    { 49, 1, 17, 8 },
                    { 50, 3, 17, 15 },
                    { 51, 2, 18, 2 },
                    { 52, 1, 18, 12 },
                    { 53, 4, 18, 7 },
                    { 54, 3, 19, 1 },
                    { 55, 1, 19, 6 },
                    { 56, 2, 19, 9 },
                    { 57, 4, 19, 14 },
                    { 58, 1, 20, 10 },
                    { 59, 3, 21, 13 },
                    { 60, 2, 21, 5 },
                    { 61, 1, 22, 11 },
                    { 62, 4, 22, 3 },
                    { 63, 3, 22, 8 },
                    { 64, 2, 23, 15 },
                    { 65, 1, 23, 1 },
                    { 66, 4, 24, 7 },
                    { 67, 1, 24, 14 },
                    { 68, 3, 24, 2 },
                    { 69, 2, 25, 12 },
                    { 70, 4, 25, 6 },
                    { 71, 1, 25, 9 },
                    { 72, 3, 26, 4 },
                    { 73, 2, 26, 13 },
                    { 74, 1, 26, 10 },
                    { 75, 4, 27, 5 },
                    { 76, 1, 27, 11 },
                    { 77, 3, 27, 3 },
                    { 78, 2, 27, 8 },
                    { 79, 1, 28, 15 },
                    { 80, 4, 28, 14 },
                    { 81, 3, 29, 1 },
                    { 82, 2, 29, 7 },
                    { 83, 1, 29, 12 },
                    { 84, 4, 30, 6 },
                    { 85, 1, 30, 9 },
                    { 86, 3, 30, 2 },
                    { 87, 2, 30, 13 },
                    { 88, 1, 31, 10 },
                    { 89, 4, 31, 4 },
                    { 90, 3, 31, 11 },
                    { 91, 2, 32, 5 },
                    { 92, 1, 32, 15 },
                    { 93, 4, 33, 8 },
                    { 94, 1, 33, 14 },
                    { 95, 3, 33, 1 },
                    { 96, 2, 34, 7 },
                    { 97, 1, 34, 12 },
                    { 98, 4, 35, 3 },
                    { 99, 1, 35, 6 },
                    { 100, 3, 35, 9 },
                    { 101, 2, 35, 10 },
                    { 102, 1, 36, 13 },
                    { 103, 4, 36, 2 },
                    { 104, 3, 37, 11 },
                    { 105, 1, 37, 5 },
                    { 106, 2, 38, 15 },
                    { 107, 4, 38, 8 },
                    { 108, 1, 38, 14 },
                    { 109, 3, 39, 12 },
                    { 110, 2, 39, 1 },
                    { 111, 1, 40, 4 },
                    { 112, 4, 40, 7 },
                    { 113, 3, 40, 10 },
                    { 114, 2, 40, 6 },
                    { 115, 1, 41, 9 },
                    { 116, 4, 41, 13 },
                    { 117, 3, 42, 2 },
                    { 118, 1, 42, 15 },
                    { 119, 2, 42, 11 },
                    { 120, 4, 43, 8 },
                    { 121, 1, 43, 5 },
                    { 122, 3, 43, 14 },
                    { 123, 2, 44, 12 },
                    { 124, 4, 44, 1 },
                    { 125, 1, 44, 3 },
                    { 126, 3, 44, 6 },
                    { 127, 2, 45, 9 },
                    { 128, 1, 45, 7 },
                    { 129, 4, 46, 10 },
                    { 130, 1, 46, 11 },
                    { 131, 3, 46, 15 },
                    { 132, 2, 47, 2 },
                    { 133, 1, 47, 14 },
                    { 134, 4, 47, 4 },
                    { 135, 3, 48, 13 },
                    { 136, 2, 48, 8 },
                    { 137, 1, 48, 5 },
                    { 138, 4, 49, 6 },
                    { 139, 1, 49, 12 },
                    { 140, 3, 49, 3 },
                    { 141, 2, 49, 7 },
                    { 142, 1, 50, 9 },
                    { 143, 4, 51, 1 },
                    { 144, 2, 51, 10 },
                    { 145, 1, 51, 15 },
                    { 146, 3, 52, 11 },
                    { 147, 2, 52, 14 },
                    { 148, 1, 53, 13 },
                    { 149, 4, 53, 2 },
                    { 150, 1, 53, 8 },
                    { 151, 3, 53, 5 },
                    { 152, 2, 54, 6 },
                    { 153, 1, 54, 9 },
                    { 154, 4, 55, 4 },
                    { 155, 3, 55, 10 },
                    { 156, 2, 56, 11 },
                    { 157, 1, 56, 15 },
                    { 158, 4, 56, 1 },
                    { 159, 3, 57, 7 },
                    { 160, 2, 57, 14 },
                    { 161, 1, 57, 3 },
                    { 162, 4, 58, 12 },
                    { 163, 1, 58, 8 },
                    { 164, 3, 59, 5 },
                    { 165, 2, 59, 6 },
                    { 166, 1, 59, 9 },
                    { 167, 4, 60, 4 },
                    { 168, 1, 60, 10 },
                    { 169, 3, 60, 13 },
                    { 170, 2, 60, 2 },
                    { 171, 1, 61, 15 },
                    { 172, 4, 61, 14 },
                    { 173, 3, 62, 11 },
                    { 174, 2, 62, 1 },
                    { 175, 1, 62, 7 },
                    { 176, 4, 63, 8 },
                    { 177, 1, 63, 12 },
                    { 178, 3, 64, 3 },
                    { 179, 2, 64, 6 },
                    { 180, 1, 65, 9 },
                    { 181, 4, 65, 5 },
                    { 182, 1, 65, 10 },
                    { 183, 3, 66, 13 },
                    { 184, 2, 66, 15 },
                    { 185, 1, 66, 14 },
                    { 186, 4, 67, 2 },
                    { 187, 1, 67, 4 },
                    { 188, 3, 68, 11 },
                    { 189, 2, 68, 7 },
                    { 190, 1, 68, 8 },
                    { 191, 4, 69, 12 },
                    { 192, 1, 69, 1 },
                    { 193, 3, 69, 6 },
                    { 194, 2, 70, 9 },
                    { 195, 4, 70, 10 },
                    { 196, 1, 70, 13 },
                    { 197, 3, 70, 5 },
                    { 198, 2, 71, 15 },
                    { 199, 1, 71, 14 },
                    { 200, 4, 72, 2 },
                    { 201, 1, 72, 4 },
                    { 202, 3, 72, 11 },
                    { 203, 2, 73, 7 },
                    { 204, 4, 73, 8 },
                    { 205, 1, 73, 12 },
                    { 206, 3, 73, 3 },
                    { 207, 2, 74, 6 },
                    { 208, 1, 74, 9 },
                    { 209, 4, 75, 10 },
                    { 210, 1, 75, 13 },
                    { 211, 3, 75, 1 },
                    { 212, 2, 76, 15 },
                    { 213, 4, 76, 14 },
                    { 214, 1, 77, 2 },
                    { 215, 3, 77, 4 },
                    { 216, 2, 77, 11 },
                    { 217, 1, 78, 8 },
                    { 218, 4, 79, 5 },
                    { 219, 1, 79, 7 },
                    { 220, 3, 79, 12 },
                    { 221, 2, 79, 3 },
                    { 222, 1, 80, 6 },
                    { 223, 4, 80, 9 },
                    { 224, 3, 81, 10 },
                    { 225, 2, 81, 13 },
                    { 226, 1, 81, 1 },
                    { 227, 4, 81, 15 },
                    { 228, 3, 82, 14 },
                    { 229, 1, 82, 2 },
                    { 230, 2, 83, 4 },
                    { 231, 4, 83, 11 },
                    { 232, 1, 83, 7 },
                    { 233, 3, 84, 8 },
                    { 234, 2, 84, 12 },
                    { 235, 1, 84, 3 },
                    { 236, 4, 85, 6 },
                    { 237, 1, 85, 9 },
                    { 238, 3, 85, 10 },
                    { 239, 2, 86, 13 },
                    { 240, 4, 86, 15 },
                    { 241, 1, 86, 14 },
                    { 242, 3, 86, 1 },
                    { 243, 2, 87, 2 },
                    { 244, 1, 87, 4 },
                    { 245, 4, 87, 11 },
                    { 246, 3, 88, 7 },
                    { 247, 2, 88, 8 },
                    { 248, 1, 89, 12 },
                    { 249, 4, 89, 3 },
                    { 250, 1, 89, 6 },
                    { 251, 3, 90, 9 },
                    { 252, 2, 90, 10 },
                    { 253, 1, 91, 13 },
                    { 254, 4, 91, 15 },
                    { 255, 1, 91, 14 },
                    { 256, 3, 92, 1 },
                    { 257, 2, 92, 2 },
                    { 258, 1, 93, 4 },
                    { 259, 4, 93, 11 },
                    { 260, 1, 93, 7 },
                    { 261, 3, 94, 8 },
                    { 262, 2, 94, 12 },
                    { 263, 1, 94, 3 },
                    { 264, 4, 95, 6 },
                    { 265, 1, 95, 9 },
                    { 266, 3, 96, 10 },
                    { 267, 2, 96, 13 },
                    { 268, 1, 96, 1 },
                    { 269, 4, 96, 15 },
                    { 270, 3, 97, 14 },
                    { 271, 1, 97, 2 },
                    { 272, 2, 97, 4 },
                    { 273, 4, 98, 11 },
                    { 274, 1, 98, 7 },
                    { 275, 3, 98, 8 },
                    { 276, 2, 99, 12 },
                    { 277, 4, 99, 3 },
                    { 278, 1, 100, 6 },
                    { 279, 3, 100, 9 },
                    { 280, 2, 100, 5 },
                    { 281, 3, 101, 10 },
                    { 282, 2, 101, 1 },
                    { 283, 4, 101, 14 },
                    { 284, 1, 102, 7 },
                    { 285, 3, 102, 3 },
                    { 286, 2, 102, 11 },
                    { 287, 1, 102, 5 },
                    { 288, 4, 103, 15 },
                    { 289, 1, 103, 8 },
                    { 290, 3, 103, 2 },
                    { 291, 2, 103, 12 },
                    { 292, 1, 104, 4 },
                    { 293, 3, 104, 9 },
                    { 294, 2, 105, 6 },
                    { 295, 1, 105, 13 },
                    { 296, 4, 105, 1 },
                    { 297, 3, 106, 11 },
                    { 298, 1, 106, 7 },
                    { 299, 2, 107, 10 },
                    { 300, 4, 107, 15 },
                    { 301, 1, 107, 3 },
                    { 302, 3, 107, 8 },
                    { 303, 2, 108, 14 },
                    { 304, 1, 108, 2 },
                    { 305, 4, 109, 12 },
                    { 306, 3, 109, 6 },
                    { 307, 1, 109, 9 },
                    { 308, 2, 110, 1 },
                    { 309, 4, 110, 4 },
                    { 310, 1, 110, 13 },
                    { 311, 3, 110, 7 },
                    { 312, 2, 110, 11 },
                    { 313, 1, 111, 5 },
                    { 314, 4, 111, 10 },
                    { 315, 3, 112, 15 },
                    { 316, 1, 112, 2 },
                    { 317, 2, 112, 8 },
                    { 318, 4, 113, 12 },
                    { 319, 1, 113, 3 },
                    { 320, 2, 114, 9 },
                    { 321, 4, 114, 6 },
                    { 322, 1, 114, 14 },
                    { 323, 3, 114, 1 },
                    { 324, 2, 115, 13 },
                    { 325, 1, 115, 5 },
                    { 326, 4, 115, 10 },
                    { 327, 3, 115, 11 },
                    { 328, 2, 116, 4 },
                    { 329, 1, 117, 8 },
                    { 330, 3, 117, 15 },
                    { 331, 2, 118, 2 },
                    { 332, 1, 118, 12 },
                    { 333, 4, 118, 7 },
                    { 334, 3, 119, 1 },
                    { 335, 1, 119, 6 },
                    { 336, 2, 119, 9 },
                    { 337, 4, 119, 14 },
                    { 338, 1, 120, 10 },
                    { 339, 3, 121, 13 },
                    { 340, 2, 121, 5 },
                    { 341, 1, 122, 11 },
                    { 342, 4, 122, 3 },
                    { 343, 3, 122, 8 },
                    { 344, 2, 123, 15 },
                    { 345, 1, 123, 1 },
                    { 346, 4, 124, 7 },
                    { 347, 1, 124, 14 },
                    { 348, 3, 124, 2 },
                    { 349, 2, 125, 12 },
                    { 350, 4, 125, 6 },
                    { 351, 1, 125, 9 },
                    { 352, 3, 126, 4 },
                    { 353, 2, 126, 13 },
                    { 354, 1, 126, 10 },
                    { 355, 4, 127, 5 },
                    { 356, 1, 127, 11 },
                    { 357, 3, 127, 3 },
                    { 358, 2, 127, 8 },
                    { 359, 1, 128, 15 },
                    { 360, 4, 128, 14 },
                    { 361, 3, 129, 1 },
                    { 362, 2, 129, 7 },
                    { 363, 1, 129, 12 },
                    { 364, 4, 130, 6 },
                    { 365, 1, 130, 9 },
                    { 366, 3, 130, 2 },
                    { 367, 2, 130, 13 },
                    { 368, 1, 131, 10 },
                    { 369, 4, 131, 4 },
                    { 370, 3, 131, 11 },
                    { 371, 2, 132, 5 },
                    { 372, 1, 132, 15 },
                    { 373, 4, 133, 8 },
                    { 374, 1, 133, 14 },
                    { 375, 3, 133, 1 },
                    { 376, 2, 134, 7 },
                    { 377, 1, 134, 12 },
                    { 378, 4, 135, 3 },
                    { 379, 1, 135, 6 },
                    { 380, 3, 135, 9 },
                    { 381, 2, 135, 10 },
                    { 382, 1, 136, 13 },
                    { 383, 4, 136, 2 },
                    { 384, 3, 137, 11 },
                    { 385, 1, 137, 5 },
                    { 386, 2, 138, 15 },
                    { 387, 4, 138, 8 },
                    { 388, 1, 138, 14 },
                    { 389, 3, 139, 12 },
                    { 390, 2, 139, 1 },
                    { 391, 1, 140, 4 },
                    { 392, 4, 140, 7 },
                    { 393, 3, 140, 10 },
                    { 394, 2, 140, 6 },
                    { 395, 1, 141, 9 },
                    { 396, 4, 141, 13 },
                    { 397, 3, 142, 2 },
                    { 398, 1, 142, 15 },
                    { 399, 2, 142, 11 },
                    { 400, 4, 143, 8 },
                    { 401, 1, 143, 5 },
                    { 402, 3, 143, 14 },
                    { 403, 2, 144, 12 },
                    { 404, 4, 144, 1 },
                    { 405, 1, 144, 3 },
                    { 406, 3, 144, 6 },
                    { 407, 2, 145, 9 },
                    { 408, 1, 145, 7 },
                    { 409, 4, 146, 10 },
                    { 410, 1, 146, 11 },
                    { 411, 3, 146, 15 },
                    { 412, 2, 147, 2 },
                    { 413, 1, 147, 14 },
                    { 414, 4, 147, 4 },
                    { 415, 3, 148, 13 },
                    { 416, 2, 148, 8 },
                    { 417, 1, 148, 5 },
                    { 418, 4, 149, 6 },
                    { 419, 1, 149, 12 },
                    { 420, 3, 149, 3 },
                    { 421, 2, 149, 7 },
                    { 422, 1, 150, 9 },
                    { 423, 4, 151, 1 },
                    { 424, 2, 151, 10 },
                    { 425, 1, 151, 15 },
                    { 426, 3, 152, 11 },
                    { 427, 2, 152, 14 },
                    { 428, 1, 153, 13 },
                    { 429, 4, 153, 2 },
                    { 430, 1, 153, 8 },
                    { 431, 3, 153, 5 },
                    { 432, 2, 154, 6 },
                    { 433, 1, 154, 9 },
                    { 434, 4, 155, 4 },
                    { 435, 3, 155, 10 },
                    { 436, 2, 156, 11 },
                    { 437, 1, 156, 15 },
                    { 438, 4, 156, 1 },
                    { 439, 3, 157, 7 },
                    { 440, 2, 157, 14 },
                    { 441, 1, 157, 3 },
                    { 442, 4, 158, 12 },
                    { 443, 1, 158, 8 },
                    { 444, 3, 159, 5 },
                    { 445, 2, 159, 6 },
                    { 446, 1, 159, 9 },
                    { 447, 4, 160, 4 },
                    { 448, 1, 160, 10 },
                    { 449, 3, 160, 13 },
                    { 450, 2, 160, 2 },
                    { 451, 1, 161, 15 },
                    { 452, 4, 161, 14 },
                    { 453, 3, 162, 11 },
                    { 454, 2, 162, 1 },
                    { 455, 1, 162, 7 },
                    { 456, 4, 163, 8 },
                    { 457, 1, 163, 12 },
                    { 458, 3, 164, 3 },
                    { 459, 2, 164, 6 },
                    { 460, 1, 165, 9 },
                    { 461, 4, 165, 5 },
                    { 462, 1, 165, 10 },
                    { 463, 3, 166, 13 },
                    { 464, 2, 166, 15 },
                    { 465, 1, 166, 14 },
                    { 466, 4, 167, 2 },
                    { 467, 1, 167, 4 },
                    { 468, 3, 168, 11 },
                    { 469, 2, 168, 7 },
                    { 470, 1, 168, 8 },
                    { 471, 4, 169, 12 },
                    { 472, 1, 169, 1 },
                    { 473, 3, 169, 6 },
                    { 474, 2, 170, 9 },
                    { 475, 4, 170, 10 },
                    { 476, 1, 170, 13 },
                    { 477, 3, 170, 5 },
                    { 478, 2, 171, 15 },
                    { 479, 1, 171, 14 },
                    { 480, 4, 172, 2 },
                    { 481, 1, 172, 4 },
                    { 482, 3, 172, 11 },
                    { 483, 2, 173, 7 },
                    { 484, 4, 173, 8 },
                    { 485, 1, 173, 12 },
                    { 486, 3, 173, 3 },
                    { 487, 2, 174, 6 },
                    { 488, 1, 174, 9 },
                    { 489, 4, 175, 10 },
                    { 490, 1, 175, 13 },
                    { 491, 3, 175, 1 },
                    { 492, 2, 176, 15 },
                    { 493, 4, 176, 14 },
                    { 494, 1, 177, 2 },
                    { 495, 3, 177, 4 },
                    { 496, 2, 177, 11 },
                    { 497, 1, 178, 8 },
                    { 498, 4, 179, 5 },
                    { 499, 1, 179, 7 },
                    { 500, 3, 179, 12 },
                    { 501, 2, 179, 3 },
                    { 502, 1, 180, 6 },
                    { 503, 4, 180, 9 },
                    { 504, 3, 181, 10 },
                    { 505, 2, 181, 13 },
                    { 506, 1, 181, 1 },
                    { 507, 4, 181, 15 },
                    { 508, 3, 182, 14 },
                    { 509, 1, 182, 2 },
                    { 510, 2, 183, 4 },
                    { 511, 4, 183, 11 },
                    { 512, 1, 183, 7 },
                    { 513, 3, 184, 8 },
                    { 514, 2, 184, 12 },
                    { 515, 1, 184, 3 },
                    { 516, 4, 185, 6 },
                    { 517, 1, 185, 9 },
                    { 518, 3, 185, 10 },
                    { 519, 2, 186, 13 },
                    { 520, 4, 186, 15 },
                    { 521, 1, 186, 14 },
                    { 522, 3, 186, 1 },
                    { 523, 2, 187, 2 },
                    { 524, 1, 187, 4 },
                    { 525, 4, 187, 11 },
                    { 526, 3, 188, 7 },
                    { 527, 2, 188, 8 },
                    { 528, 1, 189, 12 },
                    { 529, 4, 189, 3 },
                    { 530, 1, 189, 6 },
                    { 531, 3, 190, 9 },
                    { 532, 2, 190, 10 },
                    { 533, 1, 191, 13 },
                    { 534, 4, 191, 15 },
                    { 535, 1, 191, 14 },
                    { 536, 3, 192, 1 },
                    { 537, 2, 192, 2 },
                    { 538, 1, 193, 4 },
                    { 539, 4, 193, 11 },
                    { 540, 1, 193, 7 },
                    { 541, 3, 194, 8 },
                    { 542, 2, 194, 12 },
                    { 543, 1, 194, 3 },
                    { 544, 4, 195, 6 },
                    { 545, 1, 195, 9 },
                    { 546, 3, 196, 10 },
                    { 547, 2, 196, 13 },
                    { 548, 1, 196, 1 },
                    { 549, 4, 196, 15 },
                    { 550, 3, 197, 14 },
                    { 551, 1, 197, 2 },
                    { 552, 2, 197, 4 },
                    { 553, 4, 198, 11 },
                    { 554, 1, 198, 7 },
                    { 555, 3, 198, 8 },
                    { 556, 2, 199, 12 },
                    { 557, 4, 199, 3 },
                    { 558, 1, 200, 6 },
                    { 559, 3, 200, 9 },
                    { 560, 2, 200, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PizzaId",
                table: "OrderDetails",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketMessages_TicketId",
                table: "TicketMessages",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PizzaRecipes");

            migrationBuilder.DropTable(
                name: "TicketMessages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pizzas");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
