using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Big_Bang3_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adminRegisters",
                columns: table => new
                {
                    Admin_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Admin_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminRegisters", x => x.Admin_Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminPost",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    place_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    adminRegisterAdmin_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPost", x => x.id);
                    table.ForeignKey(
                        name: "FK_AdminPost_adminRegisters_adminRegisterAdmin_Id",
                        column: x => x.adminRegisterAdmin_Id,
                        principalTable: "adminRegisters",
                        principalColumn: "Admin_Id");
                });

            migrationBuilder.CreateTable(
                name: "agentRegisters",
                columns: table => new
                {
                    Agent_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Agent_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agent_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminRegisterAdmin_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agentRegisters", x => x.Agent_Id);
                    table.ForeignKey(
                        name: "FK_agentRegisters_adminRegisters_AdminRegisterAdmin_Id",
                        column: x => x.AdminRegisterAdmin_Id,
                        principalTable: "adminRegisters",
                        principalColumn: "Admin_Id");
                });

            migrationBuilder.CreateTable(
                name: "agencies",
                columns: table => new
                {
                    Agency_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Agency_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agency_Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agency_Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number_Of_Days = table.Column<int>(type: "int", nullable: false),
                    rate_for_day = table.Column<int>(type: "int", nullable: false),
                    Offer_For_Day = table.Column<int>(type: "int", nullable: false),
                    tour_place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    agentRegisterAgent_Id = table.Column<int>(type: "int", nullable: true),
                    adminPostid = table.Column<int>(type: "int", nullable: true),
                    AdminRegisterAdmin_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agencies", x => x.Agency_Id);
                    table.ForeignKey(
                        name: "FK_agencies_AdminPost_adminPostid",
                        column: x => x.adminPostid,
                        principalTable: "AdminPost",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_agencies_adminRegisters_AdminRegisterAdmin_Id",
                        column: x => x.AdminRegisterAdmin_Id,
                        principalTable: "adminRegisters",
                        principalColumn: "Admin_Id");
                    table.ForeignKey(
                        name: "FK_agencies_agentRegisters_agentRegisterAgent_Id",
                        column: x => x.agentRegisterAgent_Id,
                        principalTable: "agentRegisters",
                        principalColumn: "Agent_Id");
                });

            migrationBuilder.CreateTable(
                name: "accommodations",
                columns: table => new
                {
                    AccommodationDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hotel_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotelImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Food = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agency_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accommodations", x => x.AccommodationDetailId);
                    table.ForeignKey(
                        name: "FK_accommodations_agencies_Agency_Id",
                        column: x => x.Agency_Id,
                        principalTable: "agencies",
                        principalColumn: "Agency_Id");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Booking_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Date_Of_Booking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    booking_amount = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Agency_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Booking_Id);
                    table.ForeignKey(
                        name: "FK_Booking_agencies_Agency_Id",
                        column: x => x.Agency_Id,
                        principalTable: "agencies",
                        principalColumn: "Agency_Id");
                    table.ForeignKey(
                        name: "FK_Booking_users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "users",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "feedBacks",
                columns: table => new
                {
                    FeedBack_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedBack_area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedBack_rating = table.Column<int>(type: "int", nullable: false),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Agency_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedBacks", x => x.FeedBack_id);
                    table.ForeignKey(
                        name: "FK_feedBacks_agencies_Agency_Id",
                        column: x => x.Agency_Id,
                        principalTable: "agencies",
                        principalColumn: "Agency_Id");
                    table.ForeignKey(
                        name: "FK_feedBacks_users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "users",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_accommodations_Agency_Id",
                table: "accommodations",
                column: "Agency_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AdminPost_adminRegisterAdmin_Id",
                table: "AdminPost",
                column: "adminRegisterAdmin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_agencies_adminPostid",
                table: "agencies",
                column: "adminPostid");

            migrationBuilder.CreateIndex(
                name: "IX_agencies_AdminRegisterAdmin_Id",
                table: "agencies",
                column: "AdminRegisterAdmin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_agencies_agentRegisterAgent_Id",
                table: "agencies",
                column: "agentRegisterAgent_Id");

            migrationBuilder.CreateIndex(
                name: "IX_agentRegisters_AdminRegisterAdmin_Id",
                table: "agentRegisters",
                column: "AdminRegisterAdmin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Agency_Id",
                table: "Booking",
                column: "Agency_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_User_Id",
                table: "Booking",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_feedBacks_Agency_Id",
                table: "feedBacks",
                column: "Agency_Id");

            migrationBuilder.CreateIndex(
                name: "IX_feedBacks_User_Id",
                table: "feedBacks",
                column: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accommodations");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "feedBacks");

            migrationBuilder.DropTable(
                name: "agencies");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "AdminPost");

            migrationBuilder.DropTable(
                name: "agentRegisters");

            migrationBuilder.DropTable(
                name: "adminRegisters");
        }
    }
}
