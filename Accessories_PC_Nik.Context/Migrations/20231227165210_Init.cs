using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accessories_PC_Nik.Context.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TAccessKey",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Types = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAccessKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TClient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TClient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TComponent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    TypeComponents = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    MaterialType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TComponent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TDelivery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TDelivery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Duration = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TWorker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Series = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TWorker_TClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TOrder_TClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TOrder_TComponent_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "TComponent",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TOrder_TDelivery_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "TDelivery",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TOrder_TService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "TService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessKey_Id",
                table: "TAccessKey",
                column: "Types",
                unique: true,
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Email",
                table: "TClient",
                column: "Email",
                unique: true,
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Phone",
                table: "TClient",
                column: "Phone",
                unique: true,
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_Component_MaterialType_Id",
                table: "TComponent",
                column: "MaterialType",
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_Component_TypeComponents_Id",
                table: "TComponent",
                column: "TypeComponents",
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_From",
                table: "TDelivery",
                column: "From",
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderTime_Id",
                table: "TOrder",
                column: "OrderTime",
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_TOrder_ClientId",
                table: "TOrder",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TOrder_ComponentId",
                table: "TOrder",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_TOrder_DeliveryId",
                table: "TOrder",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_TOrder_ServiceId",
                table: "TOrder",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Name",
                table: "TService",
                column: "Name",
                unique: true,
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_TWorker_ClientId",
                table: "TWorker",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Worker_Number",
                table: "TWorker",
                column: "Number",
                unique: true,
                filter: "DeletedAt is null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAccessKey");

            migrationBuilder.DropTable(
                name: "TOrder");

            migrationBuilder.DropTable(
                name: "TWorker");

            migrationBuilder.DropTable(
                name: "TComponent");

            migrationBuilder.DropTable(
                name: "TDelivery");

            migrationBuilder.DropTable(
                name: "TService");

            migrationBuilder.DropTable(
                name: "TClient");
        }
    }
}
