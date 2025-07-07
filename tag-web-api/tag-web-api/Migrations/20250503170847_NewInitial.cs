using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tag_web_api.Migrations
{
    /// <inheritdoc />
    public partial class NewInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressLine1 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AddressLine2 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    AddressLine3 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    AddressLine4 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Region = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OperationHours = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "ArtCategories",
                columns: table => new
                {
                    ArtCategoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CategoryKey = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Tags = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtCategories", x => x.ArtCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "BannedReasons",
                columns: table => new
                {
                    BannedReasonID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BannedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PublicReasonForBan = table.Column<string>(type: "text", nullable: false),
                    ReasonForBan = table.Column<string>(type: "text", nullable: false),
                    SupportingDocsURL = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedReasons", x => x.BannedReasonID);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    CompetitionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Period = table.Column<string>(type: "text", nullable: false),
                    Prompt = table.Column<string>(type: "text", nullable: false),
                    TotalVotePoints = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.CompetitionID);
                });

            migrationBuilder.CreateTable(
                name: "DigitalDeliverySpecs",
                columns: table => new
                {
                    DigitalDeliverySpecsID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeliveryURL = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<decimal>(type: "numeric", nullable: true),
                    LeadTime = table.Column<string>(type: "text", nullable: false),
                    PromiseDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalDeliverySpecs", x => x.DigitalDeliverySpecsID);
                });

            migrationBuilder.CreateTable(
                name: "ExternalLinks",
                columns: table => new
                {
                    ExternalLinkID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    URL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Handle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ServiceName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalLinks", x => x.ExternalLinkID);
                });

            migrationBuilder.CreateTable(
                name: "Forms_Metadata",
                columns: table => new
                {
                    Forms_MetadataID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    APIURLpostfix = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FormBody = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FormStyle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    H1 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    H2 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    H3 = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    RequestType = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SegmentationType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms_Metadata", x => x.Forms_MetadataID);
                });

            migrationBuilder.CreateTable(
                name: "PhoneContacts",
                columns: table => new
                {
                    PhoneContactID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneContacts", x => x.PhoneContactID);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AltText = table.Column<string>(type: "text", nullable: true),
                    ArtistID = table.Column<int>(type: "integer", nullable: true),
                    Context = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Description = table.Column<string>(type: "text", nullable: true),
                    EmbedURL = table.Column<string>(type: "text", nullable: true),
                    Height = table.Column<int>(type: "integer", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    ThumbnailHeight = table.Column<int>(type: "integer", nullable: true),
                    ThumbnailURL = table.Column<string>(type: "text", nullable: true),
                    ThumbnailWidth = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    URL = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<int>(type: "integer", nullable: true),
                    Width = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.PictureID);
                });

            migrationBuilder.CreateTable(
                name: "Resolutions",
                columns: table => new
                {
                    ResolutionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Body = table.Column<string>(type: "text", nullable: false),
                    CanceledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CanceledReason = table.Column<string>(type: "text", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Executed = table.Column<bool>(type: "boolean", nullable: false),
                    ExecutedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MultipleChoice = table.Column<bool>(type: "boolean", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolutions", x => x.ResolutionID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingSpecs",
                columns: table => new
                {
                    ShippingSpecsID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarrierAccount = table.Column<string>(type: "text", nullable: false),
                    DistanceUnit = table.Column<string>(type: "text", nullable: false),
                    HazardousShipping = table.Column<bool>(type: "boolean", nullable: false),
                    Height = table.Column<decimal>(type: "numeric", nullable: false),
                    MassUnit = table.Column<string>(type: "text", nullable: false),
                    PackageType = table.Column<string>(type: "text", nullable: false),
                    ShippingNotes = table.Column<string>(type: "text", nullable: false),
                    ShippingWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    ShippoObjectID = table.Column<string>(type: "text", nullable: false),
                    ShipWeight = table.Column<decimal>(type: "numeric", nullable: false),
                    Units = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingSpecs", x => x.ShippingSpecsID);
                });

            migrationBuilder.CreateTable(
                name: "StaffRoles",
                columns: table => new
                {
                    StaffRoleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Context = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffRoles", x => x.StaffRoleID);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    TicketTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.TicketTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmailOne = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EmailTwo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    FamName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ArtistInGoodStanding = table.Column<bool>(type: "boolean", nullable: false),
                    ArtistMember = table.Column<bool>(type: "boolean", nullable: false),
                    BannedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BannedReason = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BoardValidated = table.Column<bool>(type: "boolean", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CompanyTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Joined = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    MembershipBanned = table.Column<bool>(type: "boolean", nullable: false),
                    Moderator = table.Column<bool>(type: "boolean", nullable: false),
                    Nationality = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PreferredName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    HideFromPublic = table.Column<bool>(type: "boolean", nullable: false),
                    DeathDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    ContractExpires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LinkToContract = table.Column<string>(type: "text", nullable: false),
                    LinkToMSA = table.Column<string>(type: "text", nullable: false),
                    MSA_Executed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NotesOnContracts = table.Column<string>(type: "text", nullable: false),
                    NotesOnVendors = table.Column<string>(type: "text", nullable: false),
                    POCEmail = table.Column<string>(type: "text", nullable: false),
                    POCName = table.Column<string>(type: "text", nullable: false),
                    POCPhone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorID);
                });

            migrationBuilder.CreateTable(
                name: "Forms_Fields",
                columns: table => new
                {
                    Forms_FieldID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Autocomplete = table.Column<bool>(type: "boolean", nullable: true),
                    Autofocus = table.Column<bool>(type: "boolean", nullable: true),
                    ClassName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DefaultValue = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Disabled = table.Column<bool>(type: "boolean", nullable: true),
                    FieldOrder = table.Column<int>(type: "integer", nullable: true),
                    Height = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Hidden = table.Column<bool>(type: "boolean", nullable: true),
                    Label = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Max = table.Column<int>(type: "integer", nullable: true),
                    Maxlength = table.Column<int>(type: "integer", nullable: true),
                    Min = table.Column<int>(type: "integer", nullable: true),
                    Minlength = table.Column<int>(type: "integer", nullable: true),
                    Placeholder = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsReadOnly = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    Type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    RegexValidationPattern = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Width = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Forms_MetadataID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms_Fields", x => x.Forms_FieldID);
                    table.ForeignKey(
                        name: "FK_Forms_Fields_Forms_Metadata_Forms_MetadataID",
                        column: x => x.Forms_MetadataID,
                        principalTable: "Forms_Metadata",
                        principalColumn: "Forms_MetadataID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    AddressID = table.Column<int>(type: "integer", nullable: false),
                    ExternalLinkID = table.Column<int>(type: "integer", nullable: false),
                    PhoneContactID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueID);
                    table.ForeignKey(
                        name: "FK_Venues_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venues_ExternalLinks_ExternalLinkID",
                        column: x => x.ExternalLinkID,
                        principalTable: "ExternalLinks",
                        principalColumn: "ExternalLinkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venues_PhoneContacts_PhoneContactID",
                        column: x => x.PhoneContactID,
                        principalTable: "PhoneContacts",
                        principalColumn: "PhoneContactID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    ArtistID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Applied = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Biography = table.Column<string>(type: "text", nullable: true),
                    Byline = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SEOTags = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Since = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Statement = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Title = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CoverPicID = table.Column<int>(type: "integer", nullable: true),
                    FocusCategoryID = table.Column<int>(type: "integer", nullable: true),
                    ProfilePicID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.ArtistID);
                    table.ForeignKey(
                        name: "FK_Artists_ArtCategories_FocusCategoryID",
                        column: x => x.FocusCategoryID,
                        principalTable: "ArtCategories",
                        principalColumn: "ArtCategoryID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Artists_Pictures_CoverPicID",
                        column: x => x.CoverPicID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Artists_Pictures_ProfilePicID",
                        column: x => x.ProfilePicID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttendeeName = table.Column<string>(type: "text", nullable: false),
                    SoldTimestamp = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    TicketTypeID = table.Column<int>(type: "integer", nullable: false),
                    UniqueHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketTypes_TicketTypeID",
                        column: x => x.TicketTypeID,
                        principalTable: "TicketTypes",
                        principalColumn: "TicketTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BannedLists",
                columns: table => new
                {
                    BannedListID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BannedReasonID = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    IPAddress = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<int>(type: "integer", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannedLists", x => x.BannedListID);
                    table.ForeignKey(
                        name: "FK_BannedLists_BannedReasons_BannedReasonID",
                        column: x => x.BannedReasonID,
                        principalTable: "BannedReasons",
                        principalColumn: "BannedReasonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BannedLists_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Body_Plaintext = table.Column<string>(type: "text", nullable: true),
                    Byline = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogID);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DirMsg = table.Column<string>(type: "text", nullable: false),
                    Edited = table.Column<bool>(type: "boolean", nullable: false),
                    FromUserID = table.Column<int>(type: "integer", nullable: false),
                    PictureID = table.Column<int>(type: "integer", nullable: true),
                    Sent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ToUserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Messages_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID");
                    table.ForeignKey(
                        name: "FK_Messages_Users_FromUserID",
                        column: x => x.FromUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ToUserID",
                        column: x => x.ToUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    LeaveDate = table.Column<DateTime>(type: "timestamptz", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    StaffRoleID = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK_Staffs_StaffRoles_StaffRoleID",
                        column: x => x.StaffRoleID,
                        principalTable: "StaffRoles",
                        principalColumn: "StaffRoleID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Staffs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtistID = table.Column<int>(type: "integer", nullable: false),
                    DeliveryMethod = table.Column<string>(type: "text", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    RefundID = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<string>(type: "text", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    TaxesWithheld = table.Column<decimal>(type: "numeric", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    TrackingInfo = table.Column<string>(type: "text", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    UserPreferenceID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MetricOrImperial = table.Column<string>(type: "text", nullable: false),
                    ThemePreference = table.Column<string>(type: "text", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.UserPreferenceID);
                    table.ForeignKey(
                        name: "FK_UserPreferences_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPrivacies",
                columns: table => new
                {
                    UserPrivacyID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HideProfileFromPublic = table.Column<bool>(type: "boolean", nullable: false),
                    HidingFrom_NameAndDescription = table.Column<string>(type: "text", nullable: false),
                    HidingFromAbuser = table.Column<bool>(type: "boolean", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrivacies", x => x.UserPrivacyID);
                    table.ForeignKey(
                        name: "FK_UserPrivacies_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    UserSettingsID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.UserSettingsID);
                    table.ForeignKey(
                        name: "FK_UserSettings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistPermissions",
                columns: table => new
                {
                    ArtistPermissionsID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerRole = table.Column<bool>(type: "boolean", nullable: false),
                    POS_Authorized = table.Column<bool>(type: "boolean", nullable: false),
                    ArtistID = table.Column<int>(type: "integer", nullable: false),
                    ArtistID1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistPermissions", x => x.ArtistPermissionsID);
                    table.ForeignKey(
                        name: "FK_ArtistPermissions_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistPermissions_Artists_ArtistID1",
                        column: x => x.ArtistID1,
                        principalTable: "Artists",
                        principalColumn: "ArtistID");
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtistID = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Doors = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxOccupancy = table.Column<int>(type: "integer", nullable: false),
                    MinimumAge = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    PointOfContact = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    VenueID = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "VenueID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Linker_UserAndArtistToContacts",
                columns: table => new
                {
                    Linker_UserAndArtistToContactID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressID = table.Column<int>(type: "integer", nullable: true),
                    ArtistID = table.Column<int>(type: "integer", nullable: true),
                    ExternalLinkID = table.Column<int>(type: "integer", nullable: true),
                    PhoneContactID = table.Column<int>(type: "integer", nullable: true),
                    UserID = table.Column<int>(type: "integer", nullable: true),
                    Label = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    MakePublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linker_UserAndArtistToContacts", x => x.Linker_UserAndArtistToContactID);
                    table.ForeignKey(
                        name: "FK_Linker_UserAndArtistToContacts_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK_Linker_UserAndArtistToContacts_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID");
                    table.ForeignKey(
                        name: "FK_Linker_UserAndArtistToContacts_ExternalLinks_ExternalLinkID",
                        column: x => x.ExternalLinkID,
                        principalTable: "ExternalLinks",
                        principalColumn: "ExternalLinkID");
                    table.ForeignKey(
                        name: "FK_Linker_UserAndArtistToContacts_PhoneContacts_PhoneContactID",
                        column: x => x.PhoneContactID,
                        principalTable: "PhoneContacts",
                        principalColumn: "PhoneContactID");
                    table.ForeignKey(
                        name: "FK_Linker_UserAndArtistToContacts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Linker_UserToArtist",
                columns: table => new
                {
                    Linker_UserToArtistID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    ArtistID = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linker_UserToArtist", x => x.Linker_UserToArtistID);
                    table.ForeignKey(
                        name: "FK_Linker_UserToArtist_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Linker_UserToArtist_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Linker_VendorToUsers",
                columns: table => new
                {
                    Linker_VendorToUserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmergencyManager = table.Column<bool>(type: "boolean", nullable: false),
                    MemberID = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    VendorID = table.Column<int>(type: "integer", nullable: false),
                    UserArtistID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linker_VendorToUsers", x => x.Linker_VendorToUserID);
                    table.ForeignKey(
                        name: "FK_Linker_VendorToUsers_Artists_UserArtistID",
                        column: x => x.UserArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Linker_VendorToUsers_Vendors_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendors",
                        principalColumn: "VendorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Listings",
                columns: table => new
                {
                    ListingID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CatalogueID = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CommissionInquiryWelcome = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Credits = table.Column<string>(type: "text", nullable: true),
                    Culture = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Date = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Locale = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Locus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Medium = table.Column<string>(type: "text", nullable: true),
                    Period = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    Repository = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Rights = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TaxJurisdiction = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Work_BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Work_CompletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ArtCategoryID = table.Column<int>(type: "integer", nullable: false),
                    ArtistID = table.Column<int>(type: "integer", nullable: false),
                    ProfilePicID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listings", x => x.ListingID);
                    table.ForeignKey(
                        name: "FK_Listings_ArtCategories_ArtCategoryID",
                        column: x => x.ArtCategoryID,
                        principalTable: "ArtCategories",
                        principalColumn: "ArtCategoryID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Listings_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Listings_Pictures_ProfilePicID",
                        column: x => x.ProfilePicID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    VoteID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    ResolutionID = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VoteChoiceMultiple = table.Column<string>(type: "text", nullable: true),
                    VoteChoiceTF = table.Column<bool>(type: "boolean", nullable: true),
                    VoterID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteID);
                    table.ForeignKey(
                        name: "FK_Votes_Artists_VoterID",
                        column: x => x.VoterID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Resolutions_ResolutionID",
                        column: x => x.ResolutionID,
                        principalTable: "Resolutions",
                        principalColumn: "ResolutionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Linker_TicketToEvents",
                columns: table => new
                {
                    Linker_TicketToEventID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventID = table.Column<int>(type: "integer", nullable: false),
                    TicketID = table.Column<int>(type: "integer", nullable: false),
                    TransactionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linker_TicketToEvents", x => x.Linker_TicketToEventID);
                    table.ForeignKey(
                        name: "FK_Linker_TicketToEvents_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Linker_TicketToEvents_Tickets_TicketID",
                        column: x => x.TicketID,
                        principalTable: "Tickets",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Linker_TicketToEvents_Transactions_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "Transactions",
                        principalColumn: "TransactionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionListings",
                columns: table => new
                {
                    CompetitionListingID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetitionID = table.Column<int>(type: "integer", nullable: false),
                    ListingID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionListings", x => x.CompetitionListingID);
                    table.ForeignKey(
                        name: "FK_CompetitionListings_Competitions_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionListings_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Linker_TransactionLineItems",
                columns: table => new
                {
                    Linker_TransactionLineItemID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountReason = table.Column<string>(type: "text", nullable: false),
                    FinalSalesPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    LineItemSubtotal = table.Column<decimal>(type: "numeric", nullable: false),
                    ListingID = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Tax = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linker_TransactionLineItems", x => x.Linker_TransactionLineItemID);
                    table.ForeignKey(
                        name: "FK_Linker_TransactionLineItems_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Linker_TransactionLineItems_Transactions_TransactionID",
                        column: x => x.TransactionID,
                        principalTable: "Transactions",
                        principalColumn: "TransactionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Linker_UserFavorites",
                columns: table => new
                {
                    Linker_UserFavoriteID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtistID = table.Column<int>(type: "integer", nullable: false),
                    FavoriteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ListingID = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linker_UserFavorites", x => x.Linker_UserFavoriteID);
                    table.ForeignKey(
                        name: "FK_Linker_UserFavorites_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Linker_UserFavorites_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Linker_UserFavorites_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListingSalesItem",
                columns: table => new
                {
                    ListingSalesItemNum = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventoryRemaining = table.Column<int>(type: "integer", nullable: true),
                    QuantitySold = table.Column<int>(type: "integer", nullable: true),
                    DigitalDeliverySpecsID = table.Column<int>(type: "integer", nullable: true),
                    ShippingSpecsID = table.Column<int>(type: "integer", nullable: true),
                    TicketTypeID = table.Column<int>(type: "integer", nullable: true),
                    ListingID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingSalesItem", x => x.ListingSalesItemNum);
                    table.ForeignKey(
                        name: "FK_ListingSalesItem_DigitalDeliverySpecs_DigitalDeliverySpecsID",
                        column: x => x.DigitalDeliverySpecsID,
                        principalTable: "DigitalDeliverySpecs",
                        principalColumn: "DigitalDeliverySpecsID");
                    table.ForeignKey(
                        name: "FK_ListingSalesItem_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ListingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingSalesItem_ShippingSpecs_ShippingSpecsID",
                        column: x => x.ShippingSpecsID,
                        principalTable: "ShippingSpecs",
                        principalColumn: "ShippingSpecsID");
                    table.ForeignKey(
                        name: "FK_ListingSalesItem_TicketTypes_TicketTypeID",
                        column: x => x.TicketTypeID,
                        principalTable: "TicketTypes",
                        principalColumn: "TicketTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtistID = table.Column<int>(type: "integer", nullable: true),
                    Critical = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ListingID = table.Column<int>(type: "integer", nullable: true),
                    LoggedData = table.Column<string>(type: "text", nullable: true),
                    LogTimestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    LongText = table.Column<string>(type: "text", nullable: true),
                    ShortText = table.Column<string>(type: "text", nullable: false),
                    StaffID = table.Column<int>(type: "integer", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_Logs_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID");
                    table.ForeignKey(
                        name: "FK_Logs_Listings_ListingID",
                        column: x => x.ListingID,
                        principalTable: "Listings",
                        principalColumn: "ListingID");
                    table.ForeignKey(
                        name: "FK_Logs_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID");
                    table.ForeignKey(
                        name: "FK_Logs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "CompetitionVoteLists",
                columns: table => new
                {
                    CompetitionVoteListID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    CompeitionListingID = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    VoterID = table.Column<int>(type: "integer", nullable: false),
                    CompetitionListingID = table.Column<int>(type: "integer", nullable: false),
                    CompetitionID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionVoteLists", x => x.CompetitionVoteListID);
                    table.ForeignKey(
                        name: "FK_CompetitionVoteLists_CompetitionListings_CompetitionListing~",
                        column: x => x.CompetitionListingID,
                        principalTable: "CompetitionListings",
                        principalColumn: "CompetitionListingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionVoteLists_Competitions_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionID");
                });

            migrationBuilder.CreateTable(
                name: "CompetitionWinnerLists",
                columns: table => new
                {
                    CompetitionWinnerListID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Place = table.Column<int>(type: "integer", nullable: false),
                    TopTenPercentListingID = table.Column<int>(type: "integer", nullable: false),
                    VotesForListing = table.Column<int>(type: "integer", nullable: false),
                    CompetitionListingID = table.Column<int>(type: "integer", nullable: false),
                    CompetitionID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionWinnerLists", x => x.CompetitionWinnerListID);
                    table.ForeignKey(
                        name: "FK_CompetitionWinnerLists_CompetitionListings_CompetitionListi~",
                        column: x => x.CompetitionListingID,
                        principalTable: "CompetitionListings",
                        principalColumn: "CompetitionListingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionWinnerLists_Competitions_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competitions",
                        principalColumn: "CompetitionID");
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressID", "AddressLine1", "AddressLine2", "AddressLine3", "AddressLine4", "City", "Country", "OperationHours", "Region", "State", "ZipCode" },
                values: new object[,]
                {
                    { 1, "123 Peach St", null, null, null, "Atlanta", "USA", "Mon-Fri 9am-5pm", null, "GA", "30301" },
                    { 2, "456 Oak Dr", null, null, null, "Savannah", "USA", "Mon-Sat 10am-6pm", null, "GA", "31401" },
                    { 3, "789 Pine Ln", null, null, null, "Augusta", "USA", "Mon-Fri 9am-5pm", null, "GA", "30901" },
                    { 4, "123 Trade St", null, null, null, "Charlotte", "USA", "Mon-Fri: 10am-6pm, Sat: 11am-5pm, Sun: Closed", null, "NC", "28202" },
                    { 5, "202 Cedar St", null, null, null, "Raleigh", "USA", "Mon-Fri 9am-5pm", null, "NC", "27601" },
                    { 6, "303 Birch Blvd", null, null, null, "Greensboro", "USA", "Mon-Fri 9am-5pm", null, "NC", "27401" },
                    { 7, "404 Elm St", null, null, null, "Charleston", "USA", "Mon-Fri 9am-5pm", null, "SC", "29401" },
                    { 8, "505 Spruce Dr", null, null, null, "Columbia", "USA", "Mon-Fri 9am-5pm", null, "SC", "29201" },
                    { 9, "606 Willow Way", null, null, null, "Greenville", "USA", "Mon-Fri 8am-4pm", null, "SC", "29601" },
                    { 10, "583 Aerial Heights Lane", null, null, null, "Charlotte", "USA", "Mon-Fri 9am-5pm", null, "North Carolina", "28202" }
                });

            migrationBuilder.InsertData(
                table: "ArtCategories",
                columns: new[] { "ArtCategoryID", "Category", "CategoryKey", "Description", "Tags" },
                values: new object[,]
                {
                    { 1, "Painting", "painting", "An expressive medium using color, brush, and canvas to capture emotion and imagery.", "physicalart, painting" },
                    { 2, "Sculpture", "sculpture", "A three-dimensional art form that shapes materials like clay, stone, or metal into enduring forms.", "physicalart, sculpture" },
                    { 3, "Tie Dye", "tie_dye", "A vibrant textile art form that employs dye techniques to create unique patterns on fabric.", "physicalart, tiedye" },
                    { 4, "Fiber Arts", "fiber_arts", "An intricate craft that uses fibers, yarns, and textiles to produce tactile, woven artworks.", "physicalart, fiberarts" },
                    { 5, "Sketch", "sketch", "A swift and expressive drawing that captures the essence of a subject using minimal lines.", "physicalart, sketch" },
                    { 6, "Drawing", "drawing", "A detailed and meticulous rendering using pen, ink, or charcoal to express form and emotion.", "physicalart, drawing" },
                    { 7, "Digital Art", "digital_art", "Art created through digital tools and software, merging technology with creative expression.", "digitalart, digitalart" },
                    { 8, "Website Creation", "website_creation", "The craft of designing and building engaging, interactive digital spaces using code and design principles.", "digitalart, websitecreation" },
                    { 9, "Dance", "dance", "A dynamic performance emphasizing movement, rhythm, and expression through choreographed motion.", "performanceart, dance" },
                    { 10, "Circus Arts", "circus_arts", "A thrilling live performance that blends acrobatics, juggling, and theatrical artistry.", "performanceart, circusarts" },
                    { 11, "Band", "band", "A musical performance that unites instruments and vocals to create an immersive live experience.", "performanceart, band" },
                    { 12, "Disk Jockey (DJ)", "disk_jockey", "An innovative performance where curated music is mixed live using turntables and other tools.", "performanceart, dj" },
                    { 13, "Master of Ceremony (MC)", "master_of_ceremony", "A live host and entertainer, engaging the audience and guiding events with charisma.", "performanceart, mc" },
                    { 14, "Music Producers", "music_producers", "Artists creating music by mixing, composing, or arranging audio. This form is not currently supported.", "digitalart, musicproducers" },
                    { 15, "Photography", "photography", "The art of capturing and preserving moments through compelling composition and effective lighting.", "physicalart, photography" },
                    { 16, "Printmaking", "printmaking", "A traditional art form employing carving, etching, or screen printing techniques to produce reproducible images.", "physicalart, printmaking" },
                    { 17, "Mixed Media", "mixed_media", "Artwork that combines a variety of materials and techniques, resulting in layered, textured expressions.", "physicalart, mixedmedia" },
                    { 18, "Installation Art", "installation_art", "Large-scale and immersive art installations designed to transform and engage physical spaces.", "physicalart, installationart" },
                    { 19, "Ceramics", "ceramics", "The craft of molding, firing, and glazing clay to produce both functional and decorative art objects.", "physicalart, ceramics" },
                    { 20, "Collage", "collage", "The creative assembly of various materials to form a unified, imaginative artwork.", "physicalart, collage" },
                    { 21, "Mosaic", "mosaic", "A detailed art form that assembles small pieces of tile, glass, or stone into intricate designs.", "physicalart, mosaic" },
                    { 22, "Street Art", "street_art", "Urban creativity expressed through murals, graffiti, and public installations that transform cityscapes.", "physicalart, streetart" },
                    { 23, "Conceptual Art", "conceptual_art", "An art form in which the concept or idea behind the work is more significant than its physical execution.", "physicalart, conceptualart" },
                    { 24, "Animation", "animation", "The process of creating moving images through sequential frames, blending artistic storytelling with technology.", "digitalart, animation" },
                    { 25, "Architecture", "architecture", "The creative discipline of designing and constructing functional, aesthetically pleasing physical spaces.", "physicalart, architecture" },
                    { 26, "Calligraphy", "calligraphy", "The art of beautiful handwriting, focusing on the elegance, form, and fluidity of written script.", "physicalart, calligraphy" },
                    { 27, "Fashion Design", "fashion_design", "A creative endeavor that merges aesthetic vision with practical design to produce wearable art.", "physicalart, fashiondesign" },
                    { 28, "Video Art", "video_art", "A contemporary medium that uses video technology to explore narrative structures and abstract concepts.", "digitalart, videoart" },
                    { 29, "Interactive Art", "interactive_art", "Innovative art that actively engages the audience through digital interfaces and immersive experiences.", "digitalart, interactiveart" },
                    { 30, "Merchandise", "merchandise", "Creative products designed for sale, often featuring artistic designs or branding.", "physicalart, merchandise" }
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistID", "Biography", "Byline", "CoverPicID", "FocusCategoryID", "Path", "ProfilePicID", "SEOTags", "Statement", "Title" },
                values: new object[,]
                {
                    { 2, null, "Acrylic Paintings", null, null, "ArtByEm", null, "moon, acrylic", "yes", "Art by Em" },
                    { 3, null, "Fire flow performance", null, null, "QC_Cirque", null, "fire flow, performance art", "Queen City Cirque is comprised of...", "Queen City Cirque" },
                    { 5, null, "Amazing DJ services", null, null, "djKandy", null, "DJ, house", "soooo good", "DJ Kandy" },
                    { 6, null, "“Saltwater Slide is leading the way for the Texas reggae scene by not only promoting conscious messages through their music, but following through by their actions” — Topshelf Music", null, null, "saltwaterslide", null, "reggea", "Saltwater Slide is a San Antonio-based reggae/rock band that has quickly become a staple in both local and regional circles. Those who are familiar with their music are accustomed to their positive, relatable lyrics, high energy live performances, and active contribution to their local community. You can catch the guys at their annual Reggae Beach Cleanup in Corpus Christi, their band-managed Adopt-A-Spot on Mulberry road in San Antonio, or at venues all throughout Texas and beyond. Saltwater Slide has been inspired by acts like Fortunate Youth, Passafire, The Expanders, Arise Roots, Iya Terra, Tribal Seeds, Dubbest, Roots of a Rebellion, Pepper, The Skints, Rebelution, and more, although their unique style is hard to miss and harder to forget.", "Saltwater Slide" },
                    { 7, null, "Fire Flow Performance Artist", null, null, "CampfireCirque", null, "fire, poi", "Long statement about how qualified i am!", "Campfire Cirque" }
                });

            migrationBuilder.InsertData(
                table: "ExternalLinks",
                columns: new[] { "ExternalLinkID", "Description", "Handle", "ServiceName", "URL" },
                values: new object[,]
                {
                    { 1, "Atlanta Office Reddit", "atlanta_office", "Reddit", "https://www.reddit.com/user/atlanta_office" },
                    { 2, "Savannah Office Facebook", "savannah_office", "Facebook", "https://www.facebook.com/savannah_office" },
                    { 3, "Augusta Office Instagram", "augusta_office", "Instagram", "https://www.instagram.com/augusta_office" },
                    { 4, "Charlotte Office Reddit", "charlotte_office", "Reddit", "https://www.reddit.com/user/charlotte_office" },
                    { 5, "Studio Satarah Official Website", "satarahpresents", "Website", "https://satarahpresents.com/" },
                    { 6, "Greensboro Office Instagram", "greensboro_office", "Instagram", "https://www.instagram.com/greensboro_office" },
                    { 7, "Charleston Office Reddit", "charleston_office", "Reddit", "https://www.reddit.com/user/charleston_office" },
                    { 8, "Columbia Office Facebook", "columbia_office", "Facebook", "https://www.facebook.com/columbia_office" },
                    { 9, "Greenville Office Instagram", "greenville_office", "Instagram", "https://www.instagram.com/greenville_office" }
                });

            migrationBuilder.InsertData(
                table: "Forms_Metadata",
                columns: new[] { "Forms_MetadataID", "APIURLpostfix", "FormBody", "FormStyle", "H1", "H2", "H3", "Name", "RequestType", "SegmentationType" },
                values: new object[,]
                {
                    { 1, "broken_on_purpose", "Entery data here:", null, "h1", "h2", "h3", "testform", "add", null },
                    { 2, "picture", "Please provide details about your image:", "formstyle test", "Picture Upload", "Upload your pictures", "Adding a new picture", "UploadPictureForm1", "add", null },
                    { 3, "artist", "Fill out the information below to create your artist profile. The title and slug are required.", "formstyle test", "Artist Profile", "Create or Update Your Artist Profile", "Tell us about yourself or your group", "ArtistForm1", "add/update", null },
                    { 5, "forms_metadata", "Use this form to configure form metadata:", "formstyle test DB", "Form Metadata Editor", "Create or Update Form Metadata", "Define form properties", "UpdateFormsMeta", "update", null },
                    { 6, "forms_fields", "Use this form to configure form fields:", "formstyle test DB", "Form Fields Editor", "Create or Update Form Fields", "Define form field properties", "UpdateFormsFields", "update", null },
                    { 7, "event", "Enter the details for your event. Title, description, start/end times, doors time, capacity and minimum age are required.", "formstyle test", "Event Information", "Create or Update Event", "Tell us about your event", "EventForm1", "add/update", null },
                    { 8, "blog", "Enter the details for your blog post. Title, body, and byline are required.", "formstyle test", "Blog Post", "Create or Update Blog Post", "Share your thoughts", "BlogForm1", "add/update", null },
                    { 9, "user", "Enter your user information. Username and email are required.", "formstyle test", "User Profile", "Create or Update User Information", "Personal Details", "UserForm1", "add/update", null },
                    { 10, "listingByID", "Enter the details for your listing. Title is required, and a path will be generated automatically.", "formstyle test", "Listing Information", "Create or Update Listing", "Tell us about your art", "ListingForm1", "add/update", null }
                });

            migrationBuilder.InsertData(
                table: "PhoneContacts",
                columns: new[] { "PhoneContactID", "Description", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Atlanta Office", "404-123-4567" },
                    { 2, "Savannah Office", "912-234-5678" },
                    { 3, "Augusta Office", "706-345-6789" },
                    { 4, "Charlotte Office", "704-456-7890" },
                    { 5, "Raleigh Office", "919-567-8901" },
                    { 6, "Greensboro Office", "336-678-9012" },
                    { 7, "Charleston Office", "843-789-0123" },
                    { 8, "Columbia Office", "803-890-1234" },
                    { 9, "Greenville Office", "864-901-2345" }
                });

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "PictureID", "AltText", "ArtistID", "Context", "Created", "Description", "EmbedURL", "Height", "Path", "ThumbnailHeight", "ThumbnailURL", "ThumbnailWidth", "Title", "URL", "UserID", "Width" },
                values: new object[,]
                {
                    { 1, "Graphic", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, 80, "satarah", null, null, null, "Satarah 1", "https://tagpictures.blob.core.windows.net/satarah/cropped-saratah-logo-for-web-1-1.png", null, 150 },
                    { 2, "Forest", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satarah 2", "https://tagpictures.blob.core.windows.net/satarah/forest_resized.jpg", null, null },
                    { 3, "Satarah Duo", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satarah 3", "https://tagpictures.blob.core.windows.net/satarah/satarah1-5.jpg", null, null },
                    { 4, "Sarah", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satarah 4", "https://tagpictures.blob.core.windows.net/satarah/sarah_1.jpg", null, null },
                    { 5, "Logo", 1, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tie dye", null, null, "twistedpassions", null, null, null, "Logo", "https://tagpictures.blob.core.windows.net/twistedpassions/TP%20Logo.jpg", null, null },
                    { 6, "Logo 2", 1, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tie dye", null, 80, "twistedpassions", null, null, null, "Logo 2", "https://tagpictures.blob.core.windows.net/twistedpassions/TP%20logo%202.jpg", null, 150 },
                    { 7, "modeled by Bobb", 1, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tie dye", null, null, "twistedpassions", null, null, null, "Bobb Shields", "https://tagpictures.blob.core.windows.net/twistedpassions/TP%20Bobb%20Model.jpg", null, null },
                    { 8, "modeled by Russo", 1, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tie dye", null, null, "twistedpassions", null, null, null, "Josh Russo", "https://tagpictures.blob.core.windows.net/twistedpassions/TP%20Josh%20Model.jpg", null, null },
                    { 9, "Thumbs Up", 1, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tie dye", null, null, "twistedpassions", null, null, null, "Thumbs Up!", "https://tagpictures.blob.core.windows.net/twistedpassions/TP%20thumbs%20up.jpg", null, null },
                    { 10, "TP Cover photo", 1, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "tie dye", null, null, "twistedpassions", null, null, null, "Twisted Passions Cover", "https://tagpictures.blob.core.windows.net/twistedpassions/TP%20Cover.jpg", null, null },
                    { 11, "Satarah performance photo", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satarah Performance", "https://tagpictures.blob.core.windows.net/satarah/satarah_2.jpg", null, null },
                    { 12, "Satarah aerial performance", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satarah Aerial", "https://tagpictures.blob.core.windows.net/satarah/satarah1-1.webp", null, null },
                    { 13, "Satarah fire performance", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satarah Fire Dance", "https://tagpictures.blob.core.windows.net/satarah/satarh_3.webp", null, null },
                    { 14, "Satya performance image", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satya Performance", "https://tagpictures.blob.core.windows.net/satarah/satya_4.jpg", null, null },
                    { 15, "Satya portrait", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satya", "https://tagpictures.blob.core.windows.net/satarah/satya.jpg", null, null },
                    { 16, "Graphic", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satarah 1", "https://tagpictures.blob.core.windows.net/satarah/satarah_cover.png", null, null }
                });

            migrationBuilder.InsertData(
                table: "StaffRoles",
                columns: new[] { "StaffRoleID", "Context", "RoleName" },
                values: new object[,]
                {
                    { 1, "tag", "Executive Director" },
                    { 2, "tag", "Director" },
                    { 3, "tag", "Manager" },
                    { 4, "tag", "Moderator" },
                    { 5, "tag", "FTE" },
                    { 6, "tag", "PTE" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "TicketTypeID", "Description", "Type" },
                values: new object[,]
                {
                    { 1, "Gallery Showing", "gallery showing" },
                    { 2, "Live Band", "live band" },
                    { 3, "DJ Event", "DJ event" },
                    { 4, "Circus Arts", "circus arts" },
                    { 5, "Aerial Arts", "aerial arts" },
                    { 6, "Festival", "a festival" },
                    { 7, "Class", "a class" },
                    { 8, "Open Studio Period", "an open studio period" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "ArtistInGoodStanding", "ArtistMember", "BannedDate", "BannedReason", "BirthDate", "BoardValidated", "CompanyName", "CompanyTitle", "DeathDate", "EmailOne", "EmailTwo", "FamName", "FirstName", "Gender", "HideFromPublic", "Joined", "MembershipBanned", "Moderator", "Nationality", "PreferredName", "Username" },
                values: new object[,]
                {
                    { 1, false, false, null, null, null, false, "Twisted Artists Guild", "Executive Chair", null, "bobbshields@gmail.com", null, "Shields", "Bobb", null, false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, false, "American", null, "spacewizardalpha" },
                    { 2, false, false, null, null, null, false, "Art by Em", "Artist", null, "em@bar.com", null, "Barfield", "Emily", null, false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, false, "American", null, "cottonkandy" },
                    { 3, false, false, null, null, null, false, "Satarah", "Dir: Social Media", null, "satarah@nowhere.com", null, "Rothweiler", "Katie", null, false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, false, "American", null, "satya" },
                    { 4, false, false, null, null, null, false, "Saltwater Slide", "Marketing and Keyboardist", null, "sws_vince@nowhere.com", null, "Ashley", "Vincent", null, false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, false, "American", null, "vinny" },
                    { 5, false, false, null, null, null, false, "Queen City Cirque", "Dir: Artist Relations", null, "kathleenMcnamara@maybe.com", null, "McNamara", "Kathleen", null, false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, false, "American", null, "kathleenbug" },
                    { 6, false, false, null, null, null, false, "Queen City Cirque", "Dir2: Artist Relations", null, "sarah@maybe.com", null, "Yes", "Sarah", null, false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, false, "American", null, "QCC_Sarah" }
                });

            migrationBuilder.InsertData(
                table: "ArtistPermissions",
                columns: new[] { "ArtistPermissionsID", "ArtistID", "ArtistID1", "OwnerRole", "POS_Authorized" },
                values: new object[,]
                {
                    { 2, 2, null, true, true },
                    { 3, 3, null, false, false },
                    { 5, 5, null, false, false },
                    { 6, 6, null, false, false },
                    { 7, 7, null, false, false }
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistID", "Biography", "Byline", "CoverPicID", "FocusCategoryID", "Path", "ProfilePicID", "SEOTags", "Statement", "Title" },
                values: new object[,]
                {
                    { 1, null, "Tie Dye Artisan", 10, null, "TwistedPassions", 6, "tie dye, art, tshirts", "The best damn tie dye, like EVER.", "Twisted Passions" },
                    { 4, null, "To learn more about pricing and schedule your time with Satarah Productions please us contact today.", 16, null, "satarah", 1, "dance, bellydancer, aerialist, fire performer, duo", "Satarah is the lovechild of Satya and Sarah Hahn, two passionate and talented professional bellydancers, aerialists and fire performers that have come together to produce fantastic events and entertain the world. Between the two of them, they have been professionally performing for over 20 years, bringing a dynamic and exciting experience wherever they may go. Currently calling Charlotte home, this duo travels near and far to produce and perform at events and teach workshops. They have also recently opened studio Satarah, hosting all types of events in Charlotte.", "Satarah" }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "BlogID", "Body", "Body_Plaintext", "Byline", "Path", "Title", "UserID" },
                values: new object[,]
                {
                    { 1, "The Twisted Artists Guild will unite artists and customers in new and innovative ways.", null, "The vision will be shown in the Alpha build", "tag-vision-outline", "TAG Vision Outline", 1 },
                    { 2, "Stuff and things. Theyre interesting. Someone come read me, im lonely", null, "What the website will look like in early stages", "initial-development-plan", "Initial Development Plan", 1 },
                    { 3, "I really dont know what to say here. I can keep typing, but its not really quality stuff now is it. I wonder how hard it will be to make updates to these ramblings!?", null, "Its gonna be amazing", "long-term-plan", "Long Term Plan", 1 }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 1, null, null, null, "default value test", null, 1, 1, "40px", null, null, null, null, null, null, "username", "Placeholder Test", null, "text", "100%" },
                    { 2, null, null, null, "default value test", null, 2, 1, "80px", null, null, null, null, null, null, "password", "Placeholder Test", null, "textarea", "100%" },
                    { 3, null, null, "styles.input", "", null, null, 2, null, null, "Context", null, null, null, null, "context", "Artist, listing, event, etc", null, "text", null },
                    { 4, null, null, "styles.input", "", null, null, 2, null, null, "Slug", null, null, null, null, "path", "Slug", "validate_string", "text", null },
                    { 5, null, null, "styles.input", "", null, null, 2, null, null, "Description", null, null, null, null, "description", "Description", null, "text", null },
                    { 6, null, null, "styles.input", "", null, null, 2, null, null, "Title", null, null, null, null, "title", "Title", "validate_string", "text", null },
                    { 7, null, null, "styles.input", "", null, null, 2, null, null, "Alt Text", null, null, null, null, "alttext", "Alt Text", null, "text", null },
                    { 8, null, null, "styles.input", "", null, null, 2, null, null, "URL", null, null, null, null, "url", "URL", "validate_string", "text", null },
                    { 9, null, null, "styles.input", "", null, null, 2, null, null, "Created", null, null, null, null, "created", "Created", null, "text", null },
                    { 10, null, null, "styles.input", "0", null, null, 2, null, null, "Height", null, null, null, null, "height", "Height", null, "text", null },
                    { 11, null, null, "styles.input", "0", null, null, 2, null, null, "Width", null, null, null, null, "width", "Width", null, "text", null },
                    { 12, null, null, "styles.input", "", null, null, 2, null, null, "Thumbnail URL", null, null, null, null, "thumbnailurl", "Thumbnail URL", null, "text", null },
                    { 13, null, null, "styles.input", "0", null, null, 2, null, null, "Thumbnail Height", null, null, null, null, "thumbnailheight", "Thumbnail Height", null, "text", null },
                    { 14, null, null, "styles.input", "0", null, null, 2, null, null, "Thumbnail Width", null, null, null, null, "thumbnailwidth", "Thumbnail Width", null, "text", null }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsReadOnly", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[] { 101, null, null, "styles.input", "", null, null, 3, null, null, true, "Slug", null, null, null, null, "path", "Slug (auto-generated from title)", "validate_string", "text", null });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsRequired", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[] { 102, null, null, "styles.input", "", null, null, 3, null, null, true, "Title", null, null, null, null, "title", "Artist or Group Name", "validate_string", "text", null });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 103, null, null, "styles.input", "", null, null, 3, "3", null, "Byline", null, null, null, null, "byline", "Short description (appears under title)", null, "textarea", "100%" },
                    { 104, null, null, "styles.input", "", null, null, 3, "5", null, "Statement", null, null, null, null, "statement", "Detailed artist statement", null, "textarea", "100%" },
                    { 105, null, null, "styles.input", "", null, null, 3, null, null, "SEO Search Terms", null, null, null, null, "seotags", "Keywords separated by commas", null, "text", null },
                    { 107, null, null, "styles.input", "", null, null, 3, null, null, "Since", null, null, null, null, "since", "Since", null, "datetime", null }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsRequired", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 201, null, null, "styles.input", "", null, null, 7, null, null, true, "Title", null, null, null, null, "title", "Event Title", "validate_string", "text", null },
                    { 202, null, null, "styles.input", "", null, null, 7, "10", null, true, "Description", null, null, null, null, "description", "Event Description", "validate_string", "textarea", "100%" },
                    { 203, null, null, "styles.input", "", null, null, 7, null, null, true, "Start Time", null, null, null, null, "starttime", "Start Time", "validate_datetime", "datetime-local", null },
                    { 204, null, null, "styles.input", "", null, null, 7, null, null, true, "End Time", null, null, null, null, "endtime", "End Time", "validate_datetime", "datetime-local", null },
                    { 205, null, null, "styles.input", "", null, null, 7, null, null, true, "Doors", null, null, null, null, "doors", "Doors", "validate_datetime", "datetime-local", null }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 206, null, null, "styles.input", "", null, null, 7, null, null, "Point of Contact", null, null, null, null, "pointofcontact", "Name and contact details", "validate_string", "text", null },
                    { 207, null, null, "styles.input", "", null, null, 7, "3", null, "Notes", null, null, null, null, "note", "Additional notes", "validate_string", "textarea", "100%" },
                    { 208, null, null, "styles.input", "0.00", null, null, 7, null, null, "Cost", null, null, null, null, "cost", "Cost", "validate_number", "number", null }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsRequired", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 209, null, null, "styles.input", "0", null, null, 7, null, null, true, "Max Occupancy", null, null, null, null, "maxoccupancy", "Max Occupancy", "validate_number", "number", null },
                    { 210, null, null, "styles.input", "0", null, null, 7, null, null, true, "Minimum Age", null, null, null, null, "minimumage", "Minimum Age", "validate_number", "number", null }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsReadOnly", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[] { 211, null, null, "styles.input", "", null, null, 7, null, null, true, "Path", null, null, null, null, "path", "Path (auto-generated from title)", "validate_string", "text", null });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsRequired", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 301, null, null, "styles.input", "", null, null, 8, null, null, true, "Title", null, null, null, null, "title", "Blog Title", "validate_string", "text", null },
                    { 302, null, null, "styles.input", "", null, null, 8, "15", null, true, "Content", null, null, null, null, "body", "Blog Content", "validate_string", "textarea", "100%" },
                    { 303, null, null, "styles.input", "", null, null, 8, "3", null, true, "Byline", null, null, null, null, "byline", "Short preview or summary", "validate_string", "textarea", "100%" }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsReadOnly", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[] { 304, null, null, "styles.input", "", null, null, 8, null, null, true, "Path", null, null, null, null, "path", "Path (auto-generated from title)", "validate_string", "text", null });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 401, null, null, "styles.input", "default value", null, null, 9, null, null, "Username", null, null, null, null, "username", "Username", "validate_string", "text", null },
                    { 402, null, null, "styles.input", "default value", null, null, 9, null, null, "Email", null, null, null, null, "emailone", "Email", "validate_email", "email", null },
                    { 403, null, null, "styles.input", "", null, null, 9, null, null, "Email Two", null, null, null, null, "emailtwo", "Email Two", "validate_email", "email", null },
                    { 404, null, null, "styles.input", "", null, null, 9, null, null, "First Name", null, null, null, null, "firstname", "First Name", "validate_string", "text", null },
                    { 405, null, null, "styles.input", "", null, null, 9, null, null, "Family Name", null, null, null, null, "famname", "Family Name", "validate_string", "text", null },
                    { 406, null, null, "styles.input", "", null, null, 9, null, null, "Company Name", null, null, null, null, "companyname", "Company Name", "validate_string", "text", null },
                    { 407, null, null, "styles.input", "", null, null, 9, null, null, "Company Title", null, null, null, null, "companytitle", "Company Title", "validate_string", "text", null },
                    { 408, null, null, "styles.input", "", null, null, 9, null, null, "Nationality", null, null, null, null, "nationality", "Nationality", "validate_string", "text", null },
                    { 409, null, null, "styles.input", "", null, null, 9, null, null, "Preferred Name", null, null, null, null, "preferredname", "Preferred Name", "validate_string", "text", null },
                    { 410, null, null, "styles.input", "", null, null, 9, null, null, "Gender", null, null, null, null, "gender", "Gender", "validate_string", "text", null },
                    { 411, null, null, "styles.input", "", null, null, 9, null, null, "Banned Reason", null, null, null, null, "bannedreason", "Banned Reason", "validate_string", "text", null }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsRequired", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[] { 501, null, null, "styles.input", "", null, null, 10, null, null, true, "Title", null, null, null, null, "title", "Listing Title", "validate_string", "text", null });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 502, null, null, "styles.input", "", null, null, 10, "5", null, "Description", null, null, null, null, "description", "Detailed description", null, "textarea", "100%" },
                    { 503, null, null, "styles.input", "", null, null, 10, null, null, "Catalogue ID", null, null, null, null, "catalogueid", "Catalogue ID", null, "text", null },
                    { 504, null, null, "styles.input", "", null, null, 10, "3", null, "Credits", null, null, null, null, "credits", "Credits for contributors", null, "textarea", "100%" },
                    { 505, null, null, "styles.input", "", null, null, 10, null, null, "Culture", null, null, null, null, "culture", "Cultural origin", null, "text", null },
                    { 506, null, null, "styles.input", "", null, null, 10, null, null, "Date", null, null, null, null, "date", "Creation date", null, "text", null },
                    { 507, null, null, "styles.input", "", null, null, 10, null, null, "Department", null, null, null, null, "department", "Department", null, "text", null },
                    { 508, null, null, "styles.input", "", null, null, 10, null, null, "Locale", null, null, null, null, "locale", "Geographic location", null, "text", null },
                    { 509, null, null, "styles.input", "", null, null, 10, null, null, "Locus", null, null, null, null, "locus", "Specific location", null, "text", null },
                    { 510, null, null, "styles.input", "", null, null, 10, "3", null, "Medium", null, null, null, null, "medium", "Materials and techniques", null, "textarea", "100%" },
                    { 511, null, null, "styles.input", "", null, null, 10, null, null, "Period", null, null, null, null, "period", "Historical period", null, "text", null },
                    { 512, null, null, "styles.input", "", null, null, 10, null, null, "Repository", null, null, null, null, "repository", "Storage location", null, "text", null },
                    { 513, null, null, "styles.input", "", null, null, 10, null, null, "Rights", null, null, null, null, "rights", "Copyright information", null, "text", null },
                    { 514, null, null, "styles.input", "", null, null, 10, null, null, "Tax Jurisdiction", null, null, null, null, "taxjurisdiction", "Tax Jurisdiction", null, "text", null }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsReadOnly", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[] { 515, null, null, "styles.input", "", null, null, 10, null, null, true, "Path", null, null, null, null, "path", "Path (auto-generated from title)", "validate_string", "text", null });

            migrationBuilder.InsertData(
                table: "Linker_UserToArtist",
                columns: new[] { "Linker_UserToArtistID", "ArtistID", "Role", "UserID" },
                values: new object[,]
                {
                    { 2, 2, "Member", 2 },
                    { 3, 3, "Member", 3 },
                    { 4, 3, "Member", 5 },
                    { 5, 3, "Member", 6 },
                    { 8, 5, "Member", 2 },
                    { 9, 6, "Member", 4 },
                    { 10, 7, "Member", 1 }
                });

            migrationBuilder.InsertData(
                table: "Logs",
                columns: new[] { "LogID", "ArtistID", "Critical", "ListingID", "LogTimestamp", "LoggedData", "LongText", "ShortText", "StaffID", "Tags", "UserID" },
                values: new object[] { 1, null, true, null, new DateTime(2025, 5, 3, 12, 0, 0, 0, DateTimeKind.Utc), "Initial log entry", null, "Database substantiated", null, "initial,log", 1 });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "StaffID", "Active", "LeaveDate", "Note", "StaffRoleID", "StartDate", "UserID" },
                values: new object[] { 1, true, null, null, 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 });

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueID", "AddressID", "ExternalLinkID", "Name", "PhoneContactID" },
                values: new object[,]
                {
                    { 1, 1, 1, "Coffee House", 1 },
                    { 2, 2, 2, "Large Venue", 2 },
                    { 3, 3, 3, "Art Studio", 3 },
                    { 4, 4, 4, "Classroom", 4 },
                    { 5, 10, 5, "Satarah Studio", 5 }
                });

            migrationBuilder.InsertData(
                table: "ArtistPermissions",
                columns: new[] { "ArtistPermissionsID", "ArtistID", "ArtistID1", "OwnerRole", "POS_Authorized" },
                values: new object[,]
                {
                    { 1, 1, null, true, true },
                    { 4, 4, null, false, false }
                });

            migrationBuilder.InsertData(
                table: "Linker_UserToArtist",
                columns: new[] { "Linker_UserToArtistID", "ArtistID", "Role", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "Member", 1 },
                    { 6, 4, "Member", 3 },
                    { 7, 4, "Member", 6 }
                });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingID", "ArtCategoryID", "ArtistID", "CatalogueID", "CommissionInquiryWelcome", "Credits", "Culture", "Date", "Department", "Description", "Locale", "Locus", "Medium", "Path", "Period", "Price", "ProfilePicID", "Repository", "Rights", "TaxJurisdiction", "Title", "Work_BeginDate", "Work_CompletionDate" },
                values: new object[,]
                {
                    { 1, 30, 4, null, false, null, null, null, null, "Funny when they kiss!", null, null, null, "booty-shorts", null, 199.99m, 9, null, null, null, "Studio Satarah Booty Shorts", null, null },
                    { 2, 3, 1, null, false, null, null, null, null, "A gorgeous custom tie dye", null, null, null, "tiedye1", null, 19.99m, 9, null, null, null, "Tie Dye Shirt", null, null },
                    { 3, 3, 1, null, false, null, null, null, null, "A gorgeous custom tie dye", null, null, null, "tiedye2", null, 29.99m, 9, null, null, null, "Tie Dye Shirt2", null, null },
                    { 4, 3, 1, null, false, null, null, null, null, "A gorgeous custom tie dye", null, null, null, "tiedye3", null, 39.99m, 9, null, null, null, "Tie Dye Shirt3", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtCategories_CategoryKey",
                table: "ArtCategories",
                column: "CategoryKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArtistPermissions_ArtistID",
                table: "ArtistPermissions",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistPermissions_ArtistID1",
                table: "ArtistPermissions",
                column: "ArtistID1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_CoverPicID",
                table: "Artists",
                column: "CoverPicID");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_FocusCategoryID",
                table: "Artists",
                column: "FocusCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_Path",
                table: "Artists",
                column: "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_ProfilePicID",
                table: "Artists",
                column: "ProfilePicID");

            migrationBuilder.CreateIndex(
                name: "IX_BannedLists_BannedReasonID",
                table: "BannedLists",
                column: "BannedReasonID");

            migrationBuilder.CreateIndex(
                name: "IX_BannedLists_UserID",
                table: "BannedLists",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Path",
                table: "Blogs",
                column: "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserID",
                table: "Blogs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionListings_CompetitionID",
                table: "CompetitionListings",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionListings_ListingID",
                table: "CompetitionListings",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionVoteLists_CompetitionID",
                table: "CompetitionVoteLists",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionVoteLists_CompetitionListingID",
                table: "CompetitionVoteLists",
                column: "CompetitionListingID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionWinnerLists_CompetitionID",
                table: "CompetitionWinnerLists",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionWinnerLists_CompetitionListingID",
                table: "CompetitionWinnerLists",
                column: "CompetitionListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ArtistID",
                table: "Events",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueID",
                table: "Events",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_Fields_Forms_MetadataID",
                table: "Forms_Fields",
                column: "Forms_MetadataID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_TicketToEvents_EventID",
                table: "Linker_TicketToEvents",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_TicketToEvents_TicketID",
                table: "Linker_TicketToEvents",
                column: "TicketID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_TicketToEvents_TransactionID",
                table: "Linker_TicketToEvents",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_TransactionLineItems_ListingID",
                table: "Linker_TransactionLineItems",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_TransactionLineItems_TransactionID",
                table: "Linker_TransactionLineItems",
                column: "TransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserAndArtistToContacts_AddressID",
                table: "Linker_UserAndArtistToContacts",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserAndArtistToContacts_ArtistID",
                table: "Linker_UserAndArtistToContacts",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserAndArtistToContacts_ExternalLinkID",
                table: "Linker_UserAndArtistToContacts",
                column: "ExternalLinkID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserAndArtistToContacts_PhoneContactID",
                table: "Linker_UserAndArtistToContacts",
                column: "PhoneContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserAndArtistToContacts_UserID",
                table: "Linker_UserAndArtistToContacts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserFavorites_ArtistID",
                table: "Linker_UserFavorites",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserFavorites_ListingID",
                table: "Linker_UserFavorites",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserFavorites_UserID",
                table: "Linker_UserFavorites",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserToArtist_ArtistID",
                table: "Linker_UserToArtist",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_UserToArtist_UserID",
                table: "Linker_UserToArtist",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_VendorToUsers_UserArtistID",
                table: "Linker_VendorToUsers",
                column: "UserArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_VendorToUsers_VendorID",
                table: "Linker_VendorToUsers",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_Listing_ArtistID_Path",
                table: "Listings",
                columns: new[] { "ArtistID", "Path" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ArtCategoryID",
                table: "Listings",
                column: "ArtCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_ProfilePicID",
                table: "Listings",
                column: "ProfilePicID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingSalesItem_DigitalDeliverySpecsID",
                table: "ListingSalesItem",
                column: "DigitalDeliverySpecsID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingSalesItem_ListingID",
                table: "ListingSalesItem",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingSalesItem_ShippingSpecsID",
                table: "ListingSalesItem",
                column: "ShippingSpecsID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingSalesItem_TicketTypeID",
                table: "ListingSalesItem",
                column: "TicketTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_ArtistID",
                table: "Logs",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_ListingID",
                table: "Logs",
                column: "ListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_StaffID",
                table: "Logs",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UserID",
                table: "Logs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserID",
                table: "Messages",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PictureID",
                table: "Messages",
                column: "PictureID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToUserID",
                table: "Messages",
                column: "ToUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_StaffRoleID",
                table: "Staffs",
                column: "StaffRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_UserID",
                table: "Staffs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeID",
                table: "Tickets",
                column: "TicketTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserID",
                table: "Transactions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_UserID",
                table: "UserPreferences",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPrivacies_UserID",
                table: "UserPrivacies",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_UserID",
                table: "UserSettings",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_AddressID",
                table: "Venues",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_ExternalLinkID",
                table: "Venues",
                column: "ExternalLinkID");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_PhoneContactID",
                table: "Venues",
                column: "PhoneContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_ResolutionID",
                table: "Votes",
                column: "ResolutionID");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoterID",
                table: "Votes",
                column: "VoterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistPermissions");

            migrationBuilder.DropTable(
                name: "BannedLists");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "CompetitionVoteLists");

            migrationBuilder.DropTable(
                name: "CompetitionWinnerLists");

            migrationBuilder.DropTable(
                name: "Forms_Fields");

            migrationBuilder.DropTable(
                name: "Linker_TicketToEvents");

            migrationBuilder.DropTable(
                name: "Linker_TransactionLineItems");

            migrationBuilder.DropTable(
                name: "Linker_UserAndArtistToContacts");

            migrationBuilder.DropTable(
                name: "Linker_UserFavorites");

            migrationBuilder.DropTable(
                name: "Linker_UserToArtist");

            migrationBuilder.DropTable(
                name: "Linker_VendorToUsers");

            migrationBuilder.DropTable(
                name: "ListingSalesItem");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserPreferences");

            migrationBuilder.DropTable(
                name: "UserPrivacies");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "BannedReasons");

            migrationBuilder.DropTable(
                name: "CompetitionListings");

            migrationBuilder.DropTable(
                name: "Forms_Metadata");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "DigitalDeliverySpecs");

            migrationBuilder.DropTable(
                name: "ShippingSpecs");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Resolutions");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Listings");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropTable(
                name: "StaffRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ExternalLinks");

            migrationBuilder.DropTable(
                name: "PhoneContacts");

            migrationBuilder.DropTable(
                name: "ArtCategories");

            migrationBuilder.DropTable(
                name: "Pictures");
        }
    }
}
