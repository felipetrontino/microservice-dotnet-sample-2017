using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Book.Tools.Migrations
{
    public partial class Book_1549624884 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "book");

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                schema: "book",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookCategory",
                schema: "book",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "book",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    AuthorName = table.Column<string>(nullable: true),
                    Language = table.Column<int>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_BookAuthor_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "book",
                        principalTable: "BookAuthor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookCategoryBook",
                schema: "book",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BookId = table.Column<Guid>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategoryBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookCategoryBook_Book_BookId",
                        column: x => x.BookId,
                        principalSchema: "book",
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookCategoryBook_BookCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "book",
                        principalTable: "BookCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                schema: "book",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_DeletedAt",
                schema: "book",
                table: "Book",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Book_InsertedAt",
                schema: "book",
                table: "Book",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Book_IsDeleted",
                schema: "book",
                table: "Book",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Tenant",
                schema: "book",
                table: "Book",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_Book_UpdatedAt",
                schema: "book",
                table: "Book",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_DeletedAt",
                schema: "book",
                table: "BookAuthor",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_InsertedAt",
                schema: "book",
                table: "BookAuthor",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_IsDeleted",
                schema: "book",
                table: "BookAuthor",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_Tenant",
                schema: "book",
                table: "BookAuthor",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_UpdatedAt",
                schema: "book",
                table: "BookAuthor",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_DeletedAt",
                schema: "book",
                table: "BookCategory",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_InsertedAt",
                schema: "book",
                table: "BookCategory",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_IsDeleted",
                schema: "book",
                table: "BookCategory",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_Tenant",
                schema: "book",
                table: "BookCategory",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_UpdatedAt",
                schema: "book",
                table: "BookCategory",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategoryBook_BookId",
                schema: "book",
                table: "BookCategoryBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategoryBook_CategoryId",
                schema: "book",
                table: "BookCategoryBook",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategoryBook_DeletedAt",
                schema: "book",
                table: "BookCategoryBook",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategoryBook_InsertedAt",
                schema: "book",
                table: "BookCategoryBook",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategoryBook_IsDeleted",
                schema: "book",
                table: "BookCategoryBook",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategoryBook_Tenant",
                schema: "book",
                table: "BookCategoryBook",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategoryBook_UpdatedAt",
                schema: "book",
                table: "BookCategoryBook",
                column: "UpdatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCategoryBook",
                schema: "book");

            migrationBuilder.DropTable(
                name: "Book",
                schema: "book");

            migrationBuilder.DropTable(
                name: "BookCategory",
                schema: "book");

            migrationBuilder.DropTable(
                name: "BookAuthor",
                schema: "book");
        }
    }
}
