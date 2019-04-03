using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Tools.Migrations
{
    public partial class Bookstore_1549624721 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Bookstore");

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "Bookstore",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Language = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Bookstore",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DocumentId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Bookstore",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Bookstore",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "Bookstore",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BookId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Book_BookId",
                        column: x => x.BookId,
                        principalSchema: "Bookstore",
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Bookstore",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_DeletedAt",
                schema: "Bookstore",
                table: "Book",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Book_InsertedAt",
                schema: "Bookstore",
                table: "Book",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Book_IsDeleted",
                schema: "Bookstore",
                table: "Book",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Tenant",
                schema: "Bookstore",
                table: "Book",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_Book_UpdatedAt",
                schema: "Bookstore",
                table: "Book",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DeletedAt",
                schema: "Bookstore",
                table: "Customer",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_InsertedAt",
                schema: "Bookstore",
                table: "Customer",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IsDeleted",
                schema: "Bookstore",
                table: "Customer",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Tenant",
                schema: "Bookstore",
                table: "Customer",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UpdatedAt",
                schema: "Bookstore",
                table: "Customer",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                schema: "Bookstore",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DeletedAt",
                schema: "Bookstore",
                table: "Order",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Order_InsertedAt",
                schema: "Bookstore",
                table: "Order",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Order_IsDeleted",
                schema: "Bookstore",
                table: "Order",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Tenant",
                schema: "Bookstore",
                table: "Order",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UpdatedAt",
                schema: "Bookstore",
                table: "Order",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_BookId",
                schema: "Bookstore",
                table: "OrderItem",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_DeletedAt",
                schema: "Bookstore",
                table: "OrderItem",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_InsertedAt",
                schema: "Bookstore",
                table: "OrderItem",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_IsDeleted",
                schema: "Bookstore",
                table: "OrderItem",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Bookstore",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_Tenant",
                schema: "Bookstore",
                table: "OrderItem",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_UpdatedAt",
                schema: "Bookstore",
                table: "OrderItem",
                column: "UpdatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "Bookstore");

            migrationBuilder.DropTable(
                name: "Book",
                schema: "Bookstore");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Bookstore");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Bookstore");
        }
    }
}
