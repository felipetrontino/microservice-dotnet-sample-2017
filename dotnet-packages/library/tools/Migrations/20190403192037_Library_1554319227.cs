using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Tools.Migrations
{
    public partial class Library_1554319227 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "library");

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "library",
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
                name: "Member",
                schema: "library",
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
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Copy",
                schema: "library",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BookId = table.Column<Guid>(nullable: true),
                    Number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Copy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Copy_Book_BookId",
                        column: x => x.BookId,
                        principalSchema: "library",
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                schema: "library",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    MemberId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Member_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "library",
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                schema: "library",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InsertedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Tenant = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: true),
                    BookId = table.Column<Guid>(nullable: true),
                    CopyId = table.Column<Guid>(nullable: true),
                    ReservationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loan_Book_BookId",
                        column: x => x.BookId,
                        principalSchema: "library",
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loan_Copy_CopyId",
                        column: x => x.CopyId,
                        principalSchema: "library",
                        principalTable: "Copy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loan_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "library",
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_DeletedAt",
                schema: "library",
                table: "Book",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Book_InsertedAt",
                schema: "library",
                table: "Book",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Book_IsDeleted",
                schema: "library",
                table: "Book",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Tenant",
                schema: "library",
                table: "Book",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_Book_UpdatedAt",
                schema: "library",
                table: "Book",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Copy_BookId",
                schema: "library",
                table: "Copy",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Copy_DeletedAt",
                schema: "library",
                table: "Copy",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Copy_InsertedAt",
                schema: "library",
                table: "Copy",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Copy_IsDeleted",
                schema: "library",
                table: "Copy",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Copy_Tenant",
                schema: "library",
                table: "Copy",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_Copy_UpdatedAt",
                schema: "library",
                table: "Copy",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_BookId",
                schema: "library",
                table: "Loan",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_CopyId",
                schema: "library",
                table: "Loan",
                column: "CopyId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_DeletedAt",
                schema: "library",
                table: "Loan",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_InsertedAt",
                schema: "library",
                table: "Loan",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_IsDeleted",
                schema: "library",
                table: "Loan",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_ReservationId",
                schema: "library",
                table: "Loan",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_Tenant",
                schema: "library",
                table: "Loan",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_UpdatedAt",
                schema: "library",
                table: "Loan",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Member_DeletedAt",
                schema: "library",
                table: "Member",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Member_InsertedAt",
                schema: "library",
                table: "Member",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Member_IsDeleted",
                schema: "library",
                table: "Member",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Tenant",
                schema: "library",
                table: "Member",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_Member_UpdatedAt",
                schema: "library",
                table: "Member",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_DeletedAt",
                schema: "library",
                table: "Reservation",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_InsertedAt",
                schema: "library",
                table: "Reservation",
                column: "InsertedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IsDeleted",
                schema: "library",
                table: "Reservation",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_MemberId",
                schema: "library",
                table: "Reservation",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_Tenant",
                schema: "library",
                table: "Reservation",
                column: "Tenant");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UpdatedAt",
                schema: "library",
                table: "Reservation",
                column: "UpdatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loan",
                schema: "library");

            migrationBuilder.DropTable(
                name: "Copy",
                schema: "library");

            migrationBuilder.DropTable(
                name: "Reservation",
                schema: "library");

            migrationBuilder.DropTable(
                name: "Book",
                schema: "library");

            migrationBuilder.DropTable(
                name: "Member",
                schema: "library");
        }
    }
}
