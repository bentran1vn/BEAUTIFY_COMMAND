using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class fix_slotvadsv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    IsParent = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clinic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TaxCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BusinessLicenseUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OperatingLicenseUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OperatingLicenseExpiryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalApply = table.Column<int>(type: "int", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TotalBranches = table.Column<int>(type: "int", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    IsParent = table.Column<bool>(type: "bit", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clinic_Clinic_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Clinic",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LiveStreamDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinCount = table.Column<int>(type: "int", nullable: false),
                    MessageCount = table.Column<int>(type: "int", nullable: false),
                    ReactionCount = table.Column<int>(type: "int", nullable: false),
                    TotalActivities = table.Column<int>(type: "int", nullable: false),
                    TotalBooking = table.Column<int>(type: "int", nullable: false),
                    LivestreamRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveStreamDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccurredOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ProcessedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPackage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    LimitBranch = table.Column<int>(type: "int", nullable: false),
                    LimitLiveStream = table.Column<int>(type: "int", nullable: false),
                    EnhancedViewer = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPackage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TriggerOutboxs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerOutboxs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MaxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfCustomersUsed = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClinicOnBoardingRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    RejectReason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SendMailDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicOnBoardingRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicOnBoardingRequest_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivestreamRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<TimeOnly>(type: "time", nullable: true),
                    EndDate = table.Column<TimeOnly>(type: "time", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    TotalViewers = table.Column<int>(type: "int", nullable: true),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStreamDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStreamDetailId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivestreamRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivestreamRoom_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LivestreamRoom_LiveStreamDetail_LiveStreamDetailId1",
                        column: x => x.LiveStreamDetailId1,
                        principalTable: "LiveStreamDetail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staff_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClinicService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicService_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicVoucher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaximumDiscountPercent = table.Column<double>(type: "float", nullable: false),
                    MaximumDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumUsage = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    TotalUsage = table.Column<int>(type: "int", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicVoucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicVoucher_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicVoucher_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", maxLength: 500, nullable: false),
                    StepIndex = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedure_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceMediaType = table.Column<int>(type: "int", nullable: false),
                    IndexNumber = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceMedia_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsClinic = table.Column<bool>(type: "bit", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    LivestreamRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_LivestreamRoom_LivestreamRoomId",
                        column: x => x.LivestreamRoomId,
                        principalTable: "LivestreamRoom",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiscountPercent = table.Column<double>(type: "float", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    LivestreamRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotion_LivestreamRoom_LivestreamRoomId",
                        column: x => x.LivestreamRoomId,
                        principalTable: "LivestreamRoom",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Promotion_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoctorCertificate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CertificateUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CertificateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpiryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorCertificate_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DoctorCertificate_Staff_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorService_Staff_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClinic",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClinic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClinic_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClinic_Staff_UserId",
                        column: x => x.UserId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LivestreamRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyResponses_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserConversation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConversation_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserConversation_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserConversation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcedureMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexNumber = table.Column<int>(type: "int", nullable: false),
                    ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedureMedia_Procedure_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcedurePriceType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedurePriceType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedurePriceType_Procedure_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassificationRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClassificationLabel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassificationRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassificationRules_SurveyQuestions_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassificationRules_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestionOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Option = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyQuestionOption_SurveyQuestions_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransactionDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemTransaction_Clinic_ClinicId",
                        column: x => x.ClinicId,
                        principalTable: "Clinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemTransaction_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SystemTransaction_SubscriptionPackage_SubscriptionPackageId",
                        column: x => x.SubscriptionPackageId,
                        principalTable: "SubscriptionPackage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    ClinicVoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voucher_ClinicVoucher_ClinicVoucherId",
                        column: x => x.ClinicVoucherId,
                        principalTable: "ClinicVoucher",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Voucher_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SurveyAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveyQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SurveyResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_SurveyQuestions_SurveyQuestionId",
                        column: x => x.SurveyQuestionId,
                        principalTable: "SurveyQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SurveyAnswers_SurveyResponses_SurveyResponseId",
                        column: x => x.SurveyResponseId,
                        principalTable: "SurveyResponses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProcedurePriceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DoctorNote = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerSchedule_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerSchedule_ProcedurePriceType_ProcedurePriceTypeId",
                        column: x => x.ProcedurePriceTypeId,
                        principalTable: "ProcedurePriceType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerSchedule_Procedure_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedure",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerSchedule_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSchedule_UserClinic_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "UserClinic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSchedule_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcedurePriceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeedbackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FeedbackId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Feedback_FeedbackId1",
                        column: x => x.FeedbackId1,
                        principalTable: "Feedback",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_ProcedurePriceType_ProcedurePriceTypeId",
                        column: x => x.ProcedurePriceTypeId,
                        principalTable: "ProcedurePriceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkingSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorClinicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingSchedule_CustomerSchedule_CustomerScheduleId",
                        column: x => x.CustomerScheduleId,
                        principalTable: "CustomerSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkingSchedule_UserClinic_DoctorClinicId",
                        column: x => x.DoctorClinicId,
                        principalTable: "UserClinic",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedOnUtc", "Description", "IsDeleted", "IsParent", "ModifiedOnUtc", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật tạo hình tai", false, true, null, "Phẫu Thuật Tạo Hình Tai", null },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng mặt", false, true, null, "Phẫu Thuật Vùng Mặt", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng ngực", false, true, null, "Phẫu Thuật Vùng Ngực", null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng bụng", false, true, null, "Phẫu Thuật Vùng Bụng", null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng mông", false, true, null, "Phẫu Thuật Vùng Mông", null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng chân", false, true, null, "Phẫu Thuật Vùng Chân", null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật hỗ trợ giảm cân", false, true, null, "Phẫu Thuật Giảm Cân", null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật tạo hình cơ thể", false, true, null, "Phẫu Thuật Tạo Hình Cơ Thể", null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật tạo hình bộ phận sinh dục", false, true, null, "Phẫu Thuật Tạo Hình Bộ Phận Sinh Dục", null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật tạo hình da", false, true, null, "Phẫu Thuật Tạo Hình Da", null }
                });

            migrationBuilder.InsertData(
                table: "Clinic",
                columns: new[] { "Id", "Address", "BankAccountNumber", "BankName", "BusinessLicenseUrl", "City", "CreatedOnUtc", "District", "Email", "IsActivated", "IsDeleted", "IsParent", "ModifiedOnUtc", "Name", "Note", "OperatingLicenseExpiryDate", "OperatingLicenseUrl", "ParentId", "PhoneNumber", "ProfilePictureUrl", "Status", "TaxCode", "TotalApply", "TotalBranches", "Ward" },
                values: new object[,]
                {
                    { new Guid("78705cfa-7097-408f-93e2-70950fc886a3"), null, "1234567890123", "Vietcombank", "https://storage.googleapis.com/licenses/business-license-1.pdf", "Hồ Chí Minh", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "beautycenter.saigon@gmail.com", false, false, true, null, "Beauty Center Sài Gòn", null, null, "https://storage.googleapis.com/licenses/operating-license-1.pdf", null, "0283456789", "https://res.cloudinary.com/dmiueqpah/image/upload/v1744138052/1-1711946463238508154235_eakppa.jpg", 1, "12345678901", 0, 2, null },
                    { new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"), null, "2345678901234", "BIDV", "https://storage.googleapis.com/licenses/business-license-2.pdf", "Hà Nội", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "hanoi.beautyspa@gmail.com", false, false, true, null, "Hanoi Beauty Spa", null, null, "https://storage.googleapis.com/licenses/operating-license-2.pdf", null, "0243812345", null, 1, "23456789012", 0, 2, null },
                    { new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"), null, "3456789012345", "Agribank", "https://storage.googleapis.com/licenses/business-license-3.pdf", "Đà Nẵng", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "skincare.danang@gmail.com", false, false, true, null, "Skin Care Đà Nẵng", null, null, "https://storage.googleapis.com/licenses/operating-license-3.pdf", null, "0236789123", null, 1, "34567890123", 0, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedOnUtc", "IsDeleted", "ModifiedOnUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, "System Staff" },
                    { new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Clinic Staff" },
                    { new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, "System Admin" },
                    { new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Doctor" },
                    { new Guid("b5db3ea1-f81c-465e-a23b-da7d6d361930"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Customer" },
                    { new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Clinic Admin" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPackage",
                columns: new[] { "Id", "CreatedOnUtc", "Description", "Duration", "EnhancedViewer", "IsActivated", "IsDeleted", "LimitBranch", "LimitLiveStream", "ModifiedOnUtc", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Đồng", 30, 0, true, false, 1, 5, null, "Đồng", 3000m },
                    { new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dùng Thử", 30, 0, true, false, 0, 1, null, "Dùng Thử", 0m },
                    { new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Bạc", 30, 100, true, false, 5, 10, null, "Bạc", 5200m },
                    { new Guid("b5db3ea1-f81c-465e-a23b-da7d6d361930"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Vàng", 30, 200, true, false, 10, 20, null, "Vàng", 9000000m }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedOnUtc", "Description", "IsDeleted", "IsParent", "ModifiedOnUtc", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("12121212-1212-1212-1212-121212121212"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Điều chỉnh hình dáng mũi để cân đối với khuôn mặt", false, false, null, "Nâng Mũi (Rhinoplasty)", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("13131313-1313-1313-1313-131313131313"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Loại bỏ da thừa, mỡ thừa ở mí mắt, giúp mắt to và trẻ trung hơn", false, false, null, "Cắt Mí Mắt (Blepharoplasty)", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("14141414-1414-1414-1414-141414141414"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cải thiện vùng trán và cung mày, giảm nếp nhăn", false, false, null, "Nâng Cung Mày (Brow Lift)", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("15151515-1515-1515-1515-151515151515"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tạo hình cằm cân đối với khuôn mặt", false, false, null, "Độn Cằm (Chin Augmentation)", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("16161616-1616-1616-1616-161616161616"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Loại bỏ mỡ thừa ở vùng mặt như má, cằm", false, false, null, "Hút Mỡ Mặt", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("17171717-1717-1717-1717-171717171717"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Sử dụng túi độn hoặc mỡ tự thân để tăng kích thước ngực", false, false, null, "Nâng Ngực (Breast Augmentation)", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("18181818-1818-1818-1818-181818181818"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Giảm kích thước ngực quá lớn", false, false, null, "Thu Nhỏ Ngực (Breast Reduction)", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("19191919-1919-1919-1919-191919191919"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Loại bỏ mỡ thừa ở vùng bụng", false, false, null, "Hút Mỡ Bụng", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("20202020-2020-2020-2020-202020202020"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Loại bỏ da thừa và mỡ, làm săn chắc vùng bụng", false, false, null, "Căng Da Bụng", new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.InsertData(
                table: "Clinic",
                columns: new[] { "Id", "Address", "BankAccountNumber", "BankName", "BusinessLicenseUrl", "City", "CreatedOnUtc", "District", "Email", "IsActivated", "IsDeleted", "IsParent", "ModifiedOnUtc", "Name", "Note", "OperatingLicenseExpiryDate", "OperatingLicenseUrl", "ParentId", "PhoneNumber", "ProfilePictureUrl", "Status", "TaxCode", "TotalApply", "TotalBranches", "Ward" },
                values: new object[,]
                {
                    { new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"), null, "3456789012346", "Agribank", "https://storage.googleapis.com/licenses/business-license-3-1.pdf", "Đà Nẵng", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "skincare.haichau@gmail.com", false, false, false, null, "Skin Care Đà Nẵng - Chi nhánh Hải Châu", null, null, "https://storage.googleapis.com/licenses/operating-license-3-1.pdf", new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"), "0236789111", null, 1, "34567890124", 0, 0, null },
                    { new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"), null, "1234567890125", "Vietcombank", "https://storage.googleapis.com/licenses/business-license-1-2.pdf", "Hồ Chí Minh", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "beautycenter.q3@gmail.com", false, false, false, null, "Beauty Center Sài Gòn - Chi nhánh Quận 3", null, null, "https://storage.googleapis.com/licenses/operating-license-1-2.pdf", new Guid("78705cfa-7097-408f-93e2-70950fc886a3"), "0283456222", null, 1, "12345678903", 0, 0, null },
                    { new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"), null, "3456789012347", "Agribank", "https://storage.googleapis.com/licenses/business-license-3-2.pdf", "Đà Nẵng", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "skincare.sontra@gmail.com", false, false, false, null, "Skin Care Đà Nẵng - Chi nhánh Sơn Trà", null, null, "https://storage.googleapis.com/licenses/operating-license-3-2.pdf", new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"), "0236789222", null, 1, "34567890125", 0, 0, null },
                    { new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"), null, "1234567890124", "Vietcombank", "https://storage.googleapis.com/licenses/business-license-1-1.pdf", "Hồ Chí Minh", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "beautycenter.q1@gmail.com", false, false, false, null, "Beauty Center Sài Gòn - Chi nhánh Quận 1", null, null, "https://storage.googleapis.com/licenses/operating-license-1-1.pdf", new Guid("78705cfa-7097-408f-93e2-70950fc886a3"), "0283456111", null, 1, "12345678902", 0, 0, null },
                    { new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"), null, "2345678901236", "BIDV", "https://storage.googleapis.com/licenses/business-license-2-2.pdf", "Hà Nội", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "hanoi.caugiay@gmail.com", false, false, false, null, "Hanoi Beauty Spa - Chi nhánh Cầu Giấy", null, null, "https://storage.googleapis.com/licenses/operating-license-2-2.pdf", new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"), "0243812222", "https://res.cloudinary.com/dmiueqpah/image/upload/v1744138051/hinh-AA-Clinic-lgo-moi-1-1_smg56o.jpg", 1, "23456789014", 0, 0, null },
                    { new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"), null, "2345678901235", "BIDV", "https://storage.googleapis.com/licenses/business-license-2-1.pdf", "Hà Nội", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "hanoi.dongda@gmail.com", false, false, false, null, "Hanoi Beauty Spa - Chi nhánh Đống Đa", null, null, "https://storage.googleapis.com/licenses/operating-license-2-1.pdf", new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"), "0243812111", "https://res.cloudinary.com/dvadlh7ah/image/upload/v1744178257/ty7jok5ooenrha5aydid.jpg", 1, "23456789013", 0, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("0dfa7cef-e220-482b-9428-09e22c8bcbb5"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null },
                    { new Guid("2d8efd3b-71ed-423a-b7fd-acd34616b6d8"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null },
                    { new Guid("32e8cfbb-d8b4-4768-8695-81b6b7e63c63"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hoangminhtrang@gmail.com", "Hoàng Minh", false, "Trang", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "voanhquan@gmail.com", "Võ Anh", false, "Quân", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("6f8bb800-0594-4389-9749-f214ef855bdc"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nguyenvananh@gmail.com", "Nguyễn Văn", false, "Anh", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "lethikimhoa@gmail.com", "Lê Thị Kim", false, "Hoa", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("79f207d4-dbbf-488d-848d-fc74a7fdbb29"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "phanvankhoa@gmail.com", "Phan Văn", false, "Khoa", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("8b0f0b23-f07f-453d-b8bc-1acb26d03d87"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "doanthanhtien@gmail.com", "Đoàn Thanh", false, "Tiến", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("9dde4ec6-b02f-419a-900b-5c42f1a6c863"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "lethihuong@gmail.com", "Lê Thị", false, "Hương", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("a283eb13-8d68-46c9-8a1d-450e0cc7ad13"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "skincare.danang@gmail.com", "Skin Care Đà Nẵng", false, "Skin Care Đà Nẵng", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b"), 1, null },
                    { new Guid("a2b21279-5bbd-40c3-8981-6821c7f6b2ea"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "trandinhthientan@gmail.com", "Trần Đình Thiên", false, "Tân", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("a73d00ac-00c4-456e-ab2e-dd184f8681dd"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "phamthiha@gmail.com", "Phạm Thị", false, "Hà", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("ab23d158-44e2-44d4-b679-d7c568993702"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "phamphucnghi@gmail.com", "Phạm Phúc", false, "Nghị", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("b02a28f3-f1a7-4fd7-bcb1-53be587be9f9"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "caoanhdung@gmail.com", "Cao Anh", false, "Dũng", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("b9ab6eb6-5953-455e-8d53-5ec345f8649e"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "vuthithu@gmail.com", "Vũ Thị", false, "Thu", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("bd3c9480-7bca-43d7-94ed-58cea8b32733"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nguyenngocmaihuong@gmail.com", "Nguyễn Ngọc Mai", false, "Hương", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "tranthanhlong@gmail.com", "Trần Thanh", false, "Long", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("c5514d77-31b0-4c07-b1fe-bf3219e249db"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "beautycenter.saigon@gmail.com", "Beauty Center Sài Gòn", false, "Beauty Center Sài Gòn", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b"), 1, null },
                    { new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b32"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "dothanhsonquan1@gmail.com", "Đỗ Thanh", false, "Sơn Quận 1", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b33"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "dothanhsonquan3@gmail.com", "Đỗ Thanh", false, "Sơn Quận 3", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("da2d6a80-75cc-4757-8ed3-e0b508ffb080"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "trinhthuonglam@gmail.com", "Trịnh Thượng", false, "Lâm", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "phamtuanminh@gmail.com", "Phạm Tuấn", false, "Minh", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("f3850b25-56de-4e0d-8e66-d46617cc6f92"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hanoi.beautyspa@gmail.com", "Hanoi Beauty Spa", false, "Hanoi Beauty Spa", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b"), 1, null },
                    { new Guid("f76d8ab1-c9eb-4e29-a9f1-5302b543c283"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nguyenminhhieu@gmail.com", "Nguyễn Minh", false, "Hiếu", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null }
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "Id", "CategoryId", "CreatedOnUtc", "Description", "IsDeleted", "ModifiedOnUtc", "Name" },
                values: new object[] { new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("20202020-2020-2020-2020-202020202020"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(8385), new TimeSpan(0, 0, 0, 0, 0)), "Nhận biết loại da", false, null, "Khảo sát da" });

            migrationBuilder.InsertData(
                table: "UserClinic",
                columns: new[] { "Id", "ClinicId", "CreatedOnUtc", "IsDeleted", "ModifiedOnUtc", "UserId" },
                values: new object[,]
                {
                    { new Guid("01b2c3d4-e5f6-4a5b-8c7d-9e8f7a6b5c4d"), new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("a73d00ac-00c4-456e-ab2e-dd184f8681dd") },
                    { new Guid("06a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9b0c"), new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("ab23d158-44e2-44d4-b679-d7c568993702") },
                    { new Guid("07b8c9d0-e1f2-4a3b-4c5d-6e7f8a9b0c1d"), new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("a2b21279-5bbd-40c3-8981-6821c7f6b2ea") },
                    { new Guid("08c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e"), new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("a2b21279-5bbd-40c3-8981-6821c7f6b2ea") },
                    { new Guid("09d0e1f2-a3b4-4c5d-6e7f-8a9b0c1d2e3f"), new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("8b0f0b23-f07f-453d-b8bc-1acb26d03d87") },
                    { new Guid("16e7f8a9-b0c1-4d2e-3f4a-5b6c7d8e9f0a"), new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("79f207d4-dbbf-488d-848d-fc74a7fdbb29") },
                    { new Guid("17f8a9b0-c1d2-4e3f-4a5b-6c7d8e9f0a1b"), new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("32e8cfbb-d8b4-4768-8695-81b6b7e63c63") },
                    { new Guid("18a9b0c1-d2e3-4f4a-5b6c-7d8e9f0a1b2c"), new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f") },
                    { new Guid("19b0c1d2-e3f4-4a5b-6c7d-8e9f0a1b2c3d"), new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("bd3c9480-7bca-43d7-94ed-58cea8b32733") },
                    { new Guid("26c7d8e9-f0a1-4b2c-3d4e-5f6a7b8c9d0e"), new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c") },
                    { new Guid("27d8e9f0-a1b2-4c3d-4e5f-6a7b8c9d0e1f"), new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa") },
                    { new Guid("28e9f0a1-b2c3-4d4e-5f6a-7b8c9d0e1f2a"), new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("e8e3f18b-9179-48a6-94bb-1e5320fb8f30") },
                    { new Guid("29f0a1b2-c3d4-4e5f-6a7b-8c9d0e1f2a3b"), new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("f76d8ab1-c9eb-4e29-a9f1-5302b543c283") },
                    { new Guid("6c330a77-5168-49f3-98ad-b06a25a9c814"), new Guid("78705cfa-7097-408f-93e2-70950fc886a3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("c5514d77-31b0-4c07-b1fe-bf3219e249db") },
                    { new Guid("7f0c57c5-632a-4241-8425-95e8d1c5bd5a"), new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b33") },
                    { new Guid("b2e0cbc8-1f29-45a1-b0d6-1f67d83c0a7d"), new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b32") },
                    { new Guid("c7b4a5e6-d879-4b5a-9f3e-95e8d1c5bd5b"), new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("b9ab6eb6-5953-455e-8d53-5ec345f8649e") },
                    { new Guid("d58a7c2d-a9f2-4c9b-bd3e-32126a76f2a5"), new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("a283eb13-8d68-46c9-8a1d-450e0cc7ad13") },
                    { new Guid("e8d9c5a3-b4d7-4f5a-9e3c-1a2b3c4d5e6f"), new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("9dde4ec6-b02f-419a-900b-5c42f1a6c863") },
                    { new Guid("f3f9e5a7-d0b1-4f8a-8a45-b37a07178e4b"), new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("f3850b25-56de-4e0d-8e66-d46617cc6f92") },
                    { new Guid("f9e8d7c6-b5a4-4c3b-9d2e-1a2b3c4d5e6f"), new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, null, new Guid("6f8bb800-0594-4389-9749-f214ef855bdc") }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "CreatedOnUtc", "IsDeleted", "ModifiedOnUtc", "Question", "QuestionType", "SurveyId" },
                values: new object[,]
                {
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9164), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Sau khi rửa mặt (không bôi kem) da bạn thường cảm thấy thế nào?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9167), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Vào giữa ngày da bạn trông thế nào (nếu không thấm dầu)?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9168), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Tần suất bong tróc hoặc khô mảng?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9170), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Mức độ nhìn thấy lỗ chân lông?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9171), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Bạn có thường bị mụn hoặc tắc nghẽn lỗ chân lông?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9173), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Da bạn có khi nào vừa khô ở vài chỗ vừa dầu ở chỗ khác?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9174), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Phản ứng da khi dùng sản phẩm mới hoặc thời tiết thay đổi?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9175), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Nếu bỏ qua kem dưỡng một ngày da bạn thế nào?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9177), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Khi trang điểm lớp nền giữ trên da ra sao?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 309, DateTimeKind.Unspecified).AddTicks(9178), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Tổng quát, câu mô tả nào hợp nhất với da bạn?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") }
                });

            migrationBuilder.InsertData(
                table: "ClassificationRules",
                columns: new[] { "Id", "ClassificationLabel", "CreatedOnUtc", "IsDeleted", "ModifiedOnUtc", "OptionValue", "Points", "SurveyId", "SurveyQuestionId" },
                values: new object[,]
                {
                    { new Guid("33333333-1111-1111-1111-111111111111"), "Da khô", new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 302, DateTimeKind.Unspecified).AddTicks(4930), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("33333333-2222-1111-1111-111111111111"), "Da thường", new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 302, DateTimeKind.Unspecified).AddTicks(4948), new TimeSpan(0, 0, 0, 0, 0)), false, null, "B", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("33333333-3333-1111-1111-111111111111"), "Da hỗn hợp", new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 302, DateTimeKind.Unspecified).AddTicks(4950), new TimeSpan(0, 0, 0, 0, 0)), false, null, "C", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("33333333-4444-1111-1111-111111111111"), "Da dầu", new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 302, DateTimeKind.Unspecified).AddTicks(4951), new TimeSpan(0, 0, 0, 0, 0)), false, null, "D", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("33333333-5555-1111-1111-111111111111"), "Da nhạy cảm", new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 302, DateTimeKind.Unspecified).AddTicks(4953), new TimeSpan(0, 0, 0, 0, 0)), false, null, "E", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestionOption",
                columns: new[] { "Id", "CreatedOnUtc", "IsDeleted", "ModifiedOnUtc", "Option", "SurveyQuestionId" },
                values: new object[,]
                {
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(46), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất căng khô hoặc bong tróc; B) Khá cân bằng không quá khô hay dầu; C) Hơi bóng ở vùng chữ T; D) Bóng dầu toàn mặt; E) Đỏ hoặc châm chích", new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(51), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Không hầu như chỉ khô; B) Không khá đồng đều; C) Thường khô ở má nhưng dầu vùng chữ T; D) Oily toàn mặt; E) Thay đổi theo độ nhạy cảm", new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(53), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất nhỏ hoặc gần như không thấy; B) Thấy ở mức vừa phải; C) Rõ hơn ở vùng chữ T; D) To và dễ thấy toàn mặt; E) Rõ hơn khi da ửng đỏ hoặc kích ứng", new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(56), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất căng và khó chịu; B) Khá bình thường; C) T-zone bóng má bình thường; D) Rất bóng hoặc nhờn; E) Đỏ hoặc ngứa", new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(57), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Khô hơn hoặc bong tróc; B) Thích nghi khá ổn; C) Có vùng dầu vùng không; D) Tăng tiết dầu nổi mụn; E) Kích ứng ửng đỏ", new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(59), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Thường xuyên; B) Hầu như không bao giờ; C) Thỉnh thoảng ở một số vùng; D) Rất hiếm; E) Do nhạy cảm với sản phẩm hoặc thời tiết", new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(61), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Dễ bám vào vùng khô; B) Khá đều cần ít dặm lại; C) Xuống tông hoặc bóng ở chữ T; D) Trôi hoặc bóng dầu gần như toàn mặt; E) Kích ứng hoặc ửng đỏ", new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(64), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất hiếm; B) Đôi khi; C) Chủ yếu ở vùng chữ T; D) Thường xuyên hoặc toàn mặt; E) Phụ thuộc độ nhạy cảm với sản phẩm", new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(66), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất khô và hay căng; B) Cân bằng không quá khô dầu; C) Vừa dầu vừa khô da hỗn hợp; D) Dầu toàn mặt; E) Rất nhạy cảm hoặc dễ kích ứng", new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"), new DateTimeOffset(new DateTime(2025, 4, 10, 17, 52, 3, 310, DateTimeKind.Unspecified).AddTicks(68), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Vẫn khô hoặc căng; B) Khá cân bằng ít bóng; C) Có chút bóng ở vùng chữ T; D) Bóng dầu toàn khuôn mặt; E) Dễ kích ứng hoặc ửng đỏ", new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassificationRules_SurveyId",
                table: "ClassificationRules",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassificationRules_SurveyQuestionId",
                table: "ClassificationRules",
                column: "SurveyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_ParentId",
                table: "Clinic",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicOnBoardingRequest_ClinicId",
                table: "ClinicOnBoardingRequest",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicService_ClinicId",
                table: "ClinicService",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicService_ServiceId",
                table: "ClinicService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicVoucher_ClinicId",
                table: "ClinicVoucher",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicVoucher_ServiceId",
                table: "ClinicVoucher",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSchedule_CustomerId",
                table: "CustomerSchedule",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSchedule_DoctorId",
                table: "CustomerSchedule",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSchedule_OrderId",
                table: "CustomerSchedule",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSchedule_ProcedureId",
                table: "CustomerSchedule",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSchedule_ProcedurePriceTypeId",
                table: "CustomerSchedule",
                column: "ProcedurePriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSchedule_ServiceId",
                table: "CustomerSchedule",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCertificate_DoctorId",
                table: "DoctorCertificate",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorCertificate_ServiceId",
                table: "DoctorCertificate",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorService_DoctorId",
                table: "DoctorService",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorService_ServiceId",
                table: "DoctorService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_LivestreamRoom_ClinicId",
                table: "LivestreamRoom",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_LivestreamRoom_LiveStreamDetailId1",
                table: "LivestreamRoom",
                column: "LiveStreamDetailId1");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationId",
                table: "Message",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_LivestreamRoomId",
                table: "Message",
                column: "LivestreamRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ServiceId",
                table: "Order",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_FeedbackId1",
                table: "OrderDetail",
                column: "FeedbackId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProcedurePriceTypeId",
                table: "OrderDetail",
                column: "ProcedurePriceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ServiceId",
                table: "OrderDetail",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedure_ServiceId",
                table: "Procedure",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureMedia_ProcedureId",
                table: "ProcedureMedia",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedurePriceType_ProcedureId",
                table: "ProcedurePriceType",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_LivestreamRoomId",
                table: "Promotion",
                column: "LivestreamRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_ServiceId",
                table: "Promotion",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_CategoryId",
                table: "Service",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceMedia_ServiceId",
                table: "ServiceMedia",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_RoleId",
                table: "Staff",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_SurveyQuestionId",
                table: "SurveyAnswers",
                column: "SurveyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyAnswers_SurveyResponseId",
                table: "SurveyAnswers",
                column: "SurveyResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestionOption_SurveyQuestionId",
                table: "SurveyQuestionOption",
                column: "SurveyQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_CustomerId",
                table: "SurveyResponses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_SurveyId",
                table: "SurveyResponses",
                column: "SurveyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CategoryId",
                table: "Surveys",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTransaction_ClinicId",
                table: "SystemTransaction",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTransaction_OrderId",
                table: "SystemTransaction",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemTransaction_SubscriptionPackageId",
                table: "SystemTransaction",
                column: "SubscriptionPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClinic_ClinicId",
                table: "UserClinic",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClinic_UserId",
                table: "UserClinic",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversation_ClinicId",
                table: "UserConversation",
                column: "ClinicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversation_ConversationId",
                table: "UserConversation",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversation_UserId",
                table: "UserConversation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_ClinicVoucherId",
                table: "Voucher",
                column: "ClinicVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_OrderId",
                table: "Voucher",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingSchedule_CustomerScheduleId",
                table: "WorkingSchedule",
                column: "CustomerScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingSchedule_DoctorClinicId",
                table: "WorkingSchedule",
                column: "DoctorClinicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassificationRules");

            migrationBuilder.DropTable(
                name: "ClinicOnBoardingRequest");

            migrationBuilder.DropTable(
                name: "ClinicService");

            migrationBuilder.DropTable(
                name: "DoctorCertificate");

            migrationBuilder.DropTable(
                name: "DoctorService");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "ProcedureMedia");

            migrationBuilder.DropTable(
                name: "Promotion");

            migrationBuilder.DropTable(
                name: "ServiceMedia");

            migrationBuilder.DropTable(
                name: "SurveyAnswers");

            migrationBuilder.DropTable(
                name: "SurveyQuestionOption");

            migrationBuilder.DropTable(
                name: "SystemTransaction");

            migrationBuilder.DropTable(
                name: "TriggerOutboxs");

            migrationBuilder.DropTable(
                name: "UserConversation");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "WorkingSchedule");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "LivestreamRoom");

            migrationBuilder.DropTable(
                name: "SurveyResponses");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "SubscriptionPackage");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropTable(
                name: "ClinicVoucher");

            migrationBuilder.DropTable(
                name: "CustomerSchedule");

            migrationBuilder.DropTable(
                name: "LiveStreamDetail");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProcedurePriceType");

            migrationBuilder.DropTable(
                name: "UserClinic");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Procedure");

            migrationBuilder.DropTable(
                name: "Clinic");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
