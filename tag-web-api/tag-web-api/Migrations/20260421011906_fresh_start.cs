using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tag_web_api.Migrations
{
    /// <inheritdoc />
    public partial class fresh_start : Migration
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
                    Tags = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ParentArtCategoryID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtCategories", x => x.ArtCategoryID);
                    table.ForeignKey(
                        name: "FK_ArtCategories_ArtCategories_ParentArtCategoryID",
                        column: x => x.ParentArtCategoryID,
                        principalTable: "ArtCategories",
                        principalColumn: "ArtCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtistCategories",
                columns: table => new
                {
                    ArtistCategoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CategoryKey = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Tags = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ParentArtistCategoryID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistCategories", x => x.ArtistCategoryID);
                    table.ForeignKey(
                        name: "FK_ArtistCategories_ArtistCategories_ParentArtistCategoryID",
                        column: x => x.ParentArtistCategoryID,
                        principalTable: "ArtistCategories",
                        principalColumn: "ArtistCategoryID",
                        onDelete: ReferentialAction.Restrict);
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
                name: "EventCategories",
                columns: table => new
                {
                    EventCategoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CategoryKey = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Tags = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategories", x => x.EventCategoryID);
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
                    ProfilePicID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.ArtistID);
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
                    ArtistID = table.Column<int>(type: "integer", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtistID = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Doors = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxOccupancy = table.Column<int>(type: "integer", nullable: false),
                    MinimumAge = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PointOfContact = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    VenueID = table.Column<int>(type: "integer", nullable: false),
                    Path = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EventCategoryID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                    table.ForeignKey(
                        name: "FK_Events_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_EventCategories_EventCategoryID",
                        column: x => x.EventCategoryID,
                        principalTable: "EventCategories",
                        principalColumn: "EventCategoryID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Events_Venues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "VenueID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Linker_ArtistToCategories",
                columns: table => new
                {
                    Linker_ArtistToCategoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ArtistID = table.Column<int>(type: "integer", nullable: false),
                    ArtistCategoryID = table.Column<int>(type: "integer", nullable: false),
                    Genre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ExpertiseLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsProfessional = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linker_ArtistToCategories", x => x.Linker_ArtistToCategoryID);
                    table.ForeignKey(
                        name: "FK_Linker_ArtistToCategories_ArtistCategories_ArtistCategoryID",
                        column: x => x.ArtistCategoryID,
                        principalTable: "ArtistCategories",
                        principalColumn: "ArtistCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Linker_ArtistToCategories_Artists_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
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
                columns: new[] { "ArtCategoryID", "Category", "CategoryKey", "Description", "ParentArtCategoryID", "Tags" },
                values: new object[,]
                {
                    { 1, "Physical Art", "physical_art", "Tangible art forms created through hands-on techniques using traditional materials and mediums.", null, "root, physicalart" },
                    { 2, "Digital Art", "digital_art", "Art created through digital tools, software, and technology, representing modern creative expression.", null, "root, digitalart" },
                    { 3, "Event-Based Art", "event_based_art", "Live performances and interactive experiences shared with audiences in real-time.", null, "root, performanceart" }
                });

            migrationBuilder.InsertData(
                table: "ArtistCategories",
                columns: new[] { "ArtistCategoryID", "Category", "CategoryKey", "Description", "ParentArtistCategoryID", "Tags" },
                values: new object[,]
                {
                    { 1, "Physical Artists", "physical_art", "Artists primarily creating tangible works with physical materials and tools.", null, "artists,physical,tangible" },
                    { 2, "Digital Artists", "digital_art", "Artists primarily creating work with digital tools, software, and technology.", null, "artists,digital,software" },
                    { 3, "Event-Based Artists", "event_based_art", "Artists whose primary practice is live performance and audience-facing experiences.", null, "artists,live,performance" }
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistID", "Biography", "Byline", "CoverPicID", "Path", "ProfilePicID", "SEOTags", "Statement", "Title" },
                values: new object[,]
                {
                    { 2, null, "Acrylic Paintings", null, "ArtByEm", null, "moon, acrylic", "yes", "Art by Em" },
                    { 3, null, "Fire flow performance", null, "QC_Cirque", null, "fire flow, performance art", "Queen City Cirque is comprised of...", "Queen City Cirque" },
                    { 5, null, "Amazing DJ services", null, "djKandy", null, "DJ, house", "soooo good", "DJ Kandy" },
                    { 6, null, "“Saltwater Slide is leading the way for the Texas reggae scene by not only promoting conscious messages through their music, but following through by their actions” — Topshelf Music", null, "saltwaterslide", null, "reggea", "Saltwater Slide is a San Antonio-based reggae/rock band that has quickly become a staple in both local and regional circles. Those who are familiar with their music are accustomed to their positive, relatable lyrics, high energy live performances, and active contribution to their local community. You can catch the guys at their annual Reggae Beach Cleanup in Corpus Christi, their band-managed Adopt-A-Spot on Mulberry road in San Antonio, or at venues all throughout Texas and beyond. Saltwater Slide has been inspired by acts like Fortunate Youth, Passafire, The Expanders, Arise Roots, Iya Terra, Tribal Seeds, Dubbest, Roots of a Rebellion, Pepper, The Skints, Rebelution, and more, although their unique style is hard to miss and harder to forget.", "Saltwater Slide" }
                });

            migrationBuilder.InsertData(
                table: "EventCategories",
                columns: new[] { "EventCategoryID", "Category", "CategoryKey", "Description", "Tags" },
                values: new object[,]
                {
                    { 1, "Workshop", "workshop", "Hands-on learning sessions led by artists or instructors.", "education, interactive" },
                    { 2, "Class Series", "class_series", "Multi-session instructional programs with progressive curriculum.", "education, recurring" },
                    { 3, "Live Performance", "live_performance", "Stage or floor-based performances for a live audience.", "performance, audience" },
                    { 4, "Exhibition", "exhibition", "Curated showcase of visual, digital, or mixed media artwork.", "gallery, showcase" },
                    { 5, "Pop-Up Market", "pop_up_market", "Short-run vendor market featuring artists, makers, and local creatives.", "market, vendors" },
                    { 6, "Community Meetup", "community_meetup", "Casual gatherings for artists, supporters, and creative networking.", "community, networking" },
                    { 7, "Competition", "competition", "Juried or public-voted events where participants compete.", "contest, juried" },
                    { 8, "Open Studio", "open_studio", "Public access sessions where artists share their creative process.", "studio, behind-the-scenes" },
                    { 9, "Fundraiser", "fundraiser", "Events designed to raise financial support for artists or programs.", "charity, support" },
                    { 10, "Special Event", "special_event", "Seasonal, one-off, or collaborative events outside regular programming.", "seasonal, featured" }
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
                    { 16, "Graphic", 4, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "satarah, aerial, fire, circus", null, null, "satarah", null, null, null, "Satarah 1", "https://tagpictures.blob.core.windows.net/satarah/satarah_cover.png", null, null },
                    { 17, "Campfire Cirque Logo", 7, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "fire flow, performance art", null, null, "campfirecirque", null, null, null, "Campfire Cirque Logo", "https://tagpictures.blob.core.windows.net/campfirecirque/campfirecirque_logo.png", null, null },
                    { 18, "Campfire Cirque Cover photo", 7, "artists", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "fire flow, performance art", null, null, "campfirecirque", null, null, null, "Campfire Cirque Cover", "https://tagpictures.blob.core.windows.net/campfirecirque/campfirecirque_cover.png", null, null }
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
                table: "ArtCategories",
                columns: new[] { "ArtCategoryID", "Category", "CategoryKey", "Description", "ParentArtCategoryID", "Tags" },
                values: new object[,]
                {
                    { 4, "Painting & Drawing", "painting_drawing", "Two-dimensional art using paint, pencil, ink, and other drawing media.", 1, "physicalart, painting" },
                    { 5, "Sculpture & 3D Forms", "sculpture_3d", "Three-dimensional art forms created by shaping materials like clay, stone, or metal.", 1, "physicalart, sculpture" },
                    { 6, "Fiber & Textile Arts", "fiber_textile", "Art created through fibers, yarns, fabrics, and textile techniques.", 1, "physicalart, fiberarts" },
                    { 7, "Photography & Printmaking", "photography_print", "Capturing and reproducing images through photography, etching, and printing techniques.", 1, "physicalart, photography" },
                    { 8, "Mixed Media & Crafts", "mixed_media_crafts", "Artwork combining multiple materials and techniques, including ceramics and decorative arts.", 1, "physicalart, mixedmedia" },
                    { 9, "Street & Installation Art", "street_installation", "Large-scale and immersive artworks in public spaces and urban environments.", 1, "physicalart, streetart" },
                    { 10, "Architecture & Design", "architecture_design", "Design and construction of functional and aesthetic physical structures and spaces.", 1, "physicalart, architecture" },
                    { 11, "Visual & Graphic Design", "visual_graphic_design", "Digital design for visual communication, branding, and conceptual expression.", 2, "digitalart, design" },
                    { 12, "Animation & Video Art", "animation_video", "Moving imagery created through sequential frames, digital storytelling, and video techniques.", 2, "digitalart, animation" },
                    { 13, "Music & Audio Production", "music_audio", "Creation and production of music, sound design, and audio as a digital medium.", 2, "digitalart, musicproduction" },
                    { 14, "Interactive & Web Art", "interactive_web", "Digital art that engages audiences through interactive experiences and web technologies.", 2, "digitalart, interactive" },
                    { 15, "Software & Creative Code", "software_creative_code", "Art created through code, web design, and digital platforms.", 2, "digitalart, software" },
                    { 16, "Dance & Movement", "dance_movement", "Choreographed and improvisational movement expressing emotion and artistry.", 3, "performanceart, dance" },
                    { 17, "Circus & Acrobatics", "circus_acrobatics", "Thrilling performances blending acrobatics, juggling, and theatrical elements.", 3, "performanceart, circusarts" },
                    { 18, "Live Music Performance", "live_music", "Live musical performances including bands, ensembles, and musical entertainment.", 3, "performanceart, music" },
                    { 19, "Spoken Word & Hosting", "spoken_word_hosting", "Live spoken performance including comedy, poetry, spoken word, and event hosting.", 3, "performanceart, spoken" }
                });

            migrationBuilder.InsertData(
                table: "ArtistCategories",
                columns: new[] { "ArtistCategoryID", "Category", "CategoryKey", "Description", "ParentArtistCategoryID", "Tags" },
                values: new object[,]
                {
                    { 4, "Band", "band", "Artists working in Band.", 3, "artists,band" },
                    { 80, "Musician", "musician", "Artists working in Musician.", 3, "artists,musician" },
                    { 89, "DJ", "dj", "Artists working in DJ.", 3, "artists,dj" },
                    { 110, "Music Producer", "music_producer", "Artists working in Music Producer.", 2, "artists,music_producer" },
                    { 118, "Audio Artist", "audio_artist", "Artists working in Audio Artist.", 2, "artists,audio_artist" },
                    { 125, "Painter", "painter", "Artists working in Painter.", 1, "artists,painter" },
                    { 143, "Visual Artist", "visual_artist", "Artists working in Visual Artist.", 1, "artists,visual_artist" },
                    { 151, "Mixed Media", "mixed_media", "Artists working in Mixed Media.", 1, "artists,mixed_media" },
                    { 155, "Installation Artist", "installation_artist", "Artists working in Installation Artist.", 1, "artists,installation_artist" },
                    { 161, "Blacksmith", "blacksmith", "Artists working in Blacksmith.", 1, "artists,blacksmith" },
                    { 168, "Woodworker", "woodworker", "Artists working in Woodworker.", 1, "artists,woodworker" },
                    { 174, "Ceramicist", "ceramicist", "Artists working in Ceramicist.", 1, "artists,ceramicist" },
                    { 180, "Potter", "potter", "Artists working in Potter.", 1, "artists,potter" },
                    { 184, "Metalsmith", "metalsmith", "Artists working in Metalsmith.", 1, "artists,metalsmith" },
                    { 188, "Glass Artist", "glass_artist", "Artists working in Glass Artist.", 1, "artists,glass_artist" },
                    { 194, "Leatherworker", "leatherworker", "Artists working in Leatherworker.", 1, "artists,leatherworker" },
                    { 198, "Luthier", "luthier", "Artists working in Luthier.", 1, "artists,luthier" },
                    { 202, "Book Arts", "book_arts", "Artists working in Book Arts.", 1, "artists,book_arts" },
                    { 205, "Papermaker", "papermaker", "Artists working in Papermaker.", 1, "artists,papermaker" },
                    { 209, "Digital Illustrator", "digital_illustrator", "Artists working in Digital Illustrator.", 2, "artists,digital_illustrator" },
                    { 214, "Graphic Designer", "graphic_designer", "Artists working in Graphic Designer.", 2, "artists,graphic_designer" },
                    { 218, "Motion Designer", "motion_designer", "Artists working in Motion Designer.", 2, "artists,motion_designer" },
                    { 222, "Animator", "animator", "Artists working in Animator.", 2, "artists,animator" },
                    { 226, "3D Artist", "3d_artist", "Artists working in 3D Artist.", 2, "artists,3d_artist" },
                    { 230, "Creative Technologist", "creative_technologist", "Artists working in Creative Technologist.", 2, "artists,creative_technologist" },
                    { 234, "AR Artist", "ar_artist", "Artists working in AR Artist.", 2, "artists,ar_artist" },
                    { 238, "VR Artist", "vr_artist", "Artists working in VR Artist.", 2, "artists,vr_artist" },
                    { 242, "Dancer", "dancer", "Artists working in Dancer.", 3, "artists,dancer" },
                    { 250, "Choreographer", "choreographer", "Artists working in Choreographer.", 3, "artists,choreographer" },
                    { 253, "Theater Performer", "theater_performer", "Artists working in Theater Performer.", 3, "artists,theater_performer" },
                    { 258, "Circus Artist", "circus_artist", "Artists working in Circus Artist.", 3, "artists,circus_artist" },
                    { 265, "Poet", "poet", "Artists working in Poet.", 3, "artists,poet" },
                    { 270, "Writer", "writer", "Artists working in Writer.", 3, "artists,writer" },
                    { 277, "Spoken Word Artist", "spoken_word_artist", "Artists working in Spoken Word Artist.", 3, "artists,spoken_word_artist" },
                    { 280, "Playwright", "playwright", "Artists working in Playwright.", 3, "artists,playwright" }
                });

            migrationBuilder.InsertData(
                table: "ArtistPermissions",
                columns: new[] { "ArtistPermissionsID", "ArtistID", "OwnerRole", "POS_Authorized" },
                values: new object[,]
                {
                    { 2, 2, true, true },
                    { 3, 3, false, false },
                    { 5, 5, false, false },
                    { 6, 6, false, false }
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistID", "Biography", "Byline", "CoverPicID", "Path", "ProfilePicID", "SEOTags", "Statement", "Title" },
                values: new object[,]
                {
                    { 1, null, "Tie Dye Artisan", 10, "TwistedPassions", 6, "tie dye, art, tshirts", "The best damn tie dye, like EVER.", "Twisted Passions" },
                    { 4, null, "To learn more about pricing and schedule your time with Satarah Productions please us contact today.", 16, "satarah", 1, "dance, bellydancer, aerialist, fire performer, duo", "Satarah is the lovechild of Satya and Sarah Hahn, two passionate and talented professional bellydancers, aerialists and fire performers that have come together to produce fantastic events and entertain the world. Between the two of them, they have been professionally performing for over 20 years, bringing a dynamic and exciting experience wherever they may go. Currently calling Charlotte home, this duo travels near and far to produce and perform at events and teach workshops. They have also recently opened studio Satarah, hosting all types of events in Charlotte.", "Satarah" },
                    { 7, null, "Fire Flow Performance Artist", 18, "CampfireCirque", 17, "fire, poi", "Long statement about how qualified i am!", "Campfire Cirque" }
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
                values: new object[] { 101, null, null, "styles.input", "", null, null, 3, null, null, true, "Slug", null, null, null, null, "path", "Slug (similar to title)", "validate_string", "text", null });

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
                values: new object[] { 211, null, null, "styles.input", "", null, null, 7, null, null, true, "Path", null, null, null, null, "path", "Path (similar to title)", "validate_string", "text", null });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsRequired", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 301, null, null, "styles.input", "", null, null, 8, null, null, true, "Title", null, null, null, null, "title", "Blog Title", "validate_string", "text", null },
                    { 302, null, null, "styles.input", "", null, null, 8, "15", null, true, "Content", null, null, null, null, "body", "Blog Content", "validate_string", "tiptap_portfolio", "100%" },
                    { 303, null, null, "styles.input", "", null, null, 8, "3", null, true, "Byline", null, null, null, null, "byline", "Short preview or summary", "validate_string", "tiptap_title", "100%" }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsReadOnly", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[] { 304, null, null, "styles.input", "", null, null, 8, null, null, true, "Path", null, null, null, null, "path", "Path (similar to title)", "validate_string", "text", null });

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
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsRequired", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[] { 601, null, null, "styles.input", "", null, null, 5, null, null, true, "Form Name", null, null, null, null, "name", "Form Name", "validate_string", "text", null });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 602, null, null, "styles.input", "", null, null, 5, null, null, "API URL Postfix", null, null, null, null, "APIURLpostfix", "API URL Postfix (e.g. artist)", "validate_string", "text", null },
                    { 603, null, null, "styles.input", "", null, null, 5, null, null, "H1", null, null, null, null, "H1", "H1 text", null, "text", null },
                    { 604, null, null, "styles.input", "", null, null, 5, null, null, "H2", null, null, null, null, "H2", "H2 text", null, "text", null },
                    { 605, null, null, "styles.input", "", null, null, 5, null, null, "H3", null, null, null, null, "H3", "H3 text", null, "text", null },
                    { 606, null, null, "styles.input", "", null, null, 5, "5", null, "Form Body", null, null, null, null, "FormBody", "Form body / instructions", null, "textarea", "100%" },
                    { 607, null, null, "styles.input", "", null, null, 5, null, null, "Form Style", null, null, null, null, "FormStyle", "Form style class", null, "text", null },
                    { 608, null, null, "styles.input", "update", null, null, 5, null, null, "Request Type", null, null, null, null, "RequestType", "Request type (add/update)", null, "text", null },
                    { 609, null, null, "styles.input", "", null, null, 5, null, null, "Segmentation Type", null, null, null, null, "SegmentationType", "Segmentation type", null, "text", null }
                });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "IsRequired", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[] { 701, null, null, "styles.input", "", null, null, 6, null, null, true, "Field Name", null, null, null, null, "Name", "Field name/key", "validate_string", "text", null });

            migrationBuilder.InsertData(
                table: "Forms_Fields",
                columns: new[] { "Forms_FieldID", "Autocomplete", "Autofocus", "ClassName", "DefaultValue", "Disabled", "FieldOrder", "Forms_MetadataID", "Height", "Hidden", "Label", "Max", "Maxlength", "Min", "Minlength", "Name", "Placeholder", "RegexValidationPattern", "Type", "Width" },
                values: new object[,]
                {
                    { 702, null, null, "styles.input", "text", null, null, 6, null, null, "Field Type", null, null, null, null, "Type", "Input type (text,textarea,number,email)", null, "text", null },
                    { 703, null, null, "styles.input", "", null, null, 6, null, null, "Placeholder", null, null, null, null, "Placeholder", "Placeholder text", null, "text", null },
                    { 704, null, null, "styles.input", "", null, null, 6, null, null, "Label", null, null, null, null, "Label", "Label text", null, "text", null },
                    { 705, null, null, "styles.input", "", null, null, 6, null, null, "Default Value", null, null, null, null, "DefaultValue", "Default value", null, "text", null },
                    { 706, null, null, "styles.input", "", null, null, 6, null, null, "Class Name", null, null, null, null, "ClassName", "CSS class", null, "text", null },
                    { 707, null, null, "styles.input", "100%", null, null, 6, null, null, "Width", null, null, null, null, "Width", "Width (e.g. 100%)", null, "text", null },
                    { 708, null, null, "styles.input", "", null, null, 6, null, null, "Height", null, null, null, null, "Height", "Height (e.g. 40px)", null, "text", null },
                    { 709, null, null, "styles.input", "", null, null, 6, null, null, "Regex Validation", null, null, null, null, "RegexValidationPattern", "Validation pattern key", null, "text", null },
                    { 710, null, null, "styles.input", "0", null, null, 6, null, null, "Field Order", null, null, null, null, "FieldOrder", "Order (integer)", null, "number", null },
                    { 711, null, null, "styles.input", "false", null, null, 6, null, null, "Is Required", null, null, null, null, "IsRequired", "", null, "checkbox", null },
                    { 712, null, null, "styles.input", "false", null, null, 6, null, null, "Is Read Only", null, null, null, null, "IsReadOnly", "", null, "checkbox", null },
                    { 713, null, null, "styles.input", "", null, null, 6, null, null, "Min", null, null, null, null, "Min", "Min (optional)", null, "number", null },
                    { 714, null, null, "styles.input", "", null, null, 6, null, null, "Max", null, null, null, null, "Max", "Max (optional)", null, "number", null }
                });

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
                    { 9, 6, "Member", 4 }
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
                table: "ArtCategories",
                columns: new[] { "ArtCategoryID", "Category", "CategoryKey", "Description", "ParentArtCategoryID", "Tags" },
                values: new object[,]
                {
                    { 20, "Painting", "painting", "An expressive medium using color, brush, and canvas to capture emotion and imagery.", 4, "painting" },
                    { 21, "Drawing", "drawing", "A detailed and meticulous rendering using pen, ink, or charcoal to express form and emotion.", 4, "drawing" },
                    { 22, "Sketch", "sketch", "A swift and expressive drawing that captures the essence of a subject using minimal lines.", 4, "sketch" },
                    { 23, "Calligraphy", "calligraphy", "The art of beautiful handwriting, focusing on the elegance, form, and fluidity of written script.", 4, "calligraphy" },
                    { 24, "Sculpture", "sculpture", "A three-dimensional art form that shapes materials like clay, stone, or metal into enduring forms.", 5, "sculpture" },
                    { 25, "Ceramics", "ceramics", "The craft of molding, firing, and glazing clay to produce both functional and decorative art objects.", 5, "ceramics" },
                    { 26, "Tie Dye", "tie_dye", "A vibrant textile art form that employs dye techniques to create unique patterns on fabric.", 6, "tiedye" },
                    { 27, "Fiber Arts", "fiber_arts", "An intricate craft that uses fibers, yarns, and textiles to produce tactile, woven artworks.", 6, "fiberarts" },
                    { 28, "Photography", "photography", "The art of capturing and preserving moments through compelling composition and effective lighting.", 7, "photography" },
                    { 29, "Printmaking", "printmaking", "A traditional art form employing carving, etching, or screen printing techniques to produce reproducible images.", 7, "printmaking" },
                    { 30, "Mixed Media", "mixed_media", "Artwork that combines a variety of materials and techniques, resulting in layered, textured expressions.", 8, "mixedmedia" },
                    { 31, "Collage", "collage", "The creative assembly of various materials to form a unified, imaginative artwork.", 8, "collage" },
                    { 32, "Mosaic", "mosaic", "A detailed art form that assembles small pieces of tile, glass, or stone into intricate designs.", 8, "mosaic" },
                    { 33, "Fashion Design", "fashion_design", "A creative endeavor that merges aesthetic vision with practical design to produce wearable art.", 8, "fashiondesign" },
                    { 34, "Merchandise", "merchandise", "Creative products designed for sale, often featuring artistic designs or branding.", 8, "merchandise" },
                    { 35, "Street Art", "street_art", "Urban creativity expressed through murals, graffiti, and public installations that transform cityscapes.", 9, "streetart" },
                    { 36, "Installation Art", "installation_art", "Large-scale and immersive art installations designed to transform and engage physical spaces.", 9, "installationart" },
                    { 37, "Conceptual Art", "conceptual_art", "An art form in which the concept or idea behind the work is more significant than its physical execution.", 9, "conceptualart" },
                    { 38, "Graphic Design", "graphic_design", "Visual design for communication, branding, and digital platforms.", 11, "graphicdesign" },
                    { 39, "Conceptual Design", "conceptual_design", "Innovative design exploring ideas and pushing creative boundaries.", 11, "conceptualdesign" },
                    { 40, "Animation", "animation", "The process of creating moving images through sequential frames, blending artistic storytelling with technology.", 12, "animation" },
                    { 41, "Video Art", "video_art", "A contemporary medium that uses video technology to explore narrative structures and abstract concepts.", 12, "videoart" },
                    { 42, "Music Production", "music_production", "The creation and production of music through composition, mixing, and arrangement.", 13, "musicproduction" },
                    { 44, "Interactive Art", "interactive_art", "Innovative art that actively engages the audience through digital interfaces and immersive experiences.", 14, "interactiveart" },
                    { 45, "Web Art", "web_art", "Art created specifically for the web using HTML, CSS, and interactive technologies.", 14, "webdesign" },
                    { 46, "Website Creation", "website_creation", "The craft of designing and building engaging, interactive digital spaces using code and design principles.", 15, "websitecreation" }
                });

            migrationBuilder.InsertData(
                table: "ArtistCategories",
                columns: new[] { "ArtistCategoryID", "Category", "CategoryKey", "Description", "ParentArtistCategoryID", "Tags" },
                values: new object[,]
                {
                    { 5, "Rock", "band_rock", "Band artists focused on Rock.", 4, "artists,band,rock" },
                    { 16, "Punk", "band_punk", "Band artists focused on Punk.", 4, "artists,band,punk" },
                    { 24, "Alternative", "band_alternative", "Band artists focused on Alternative.", 4, "artists,band,alternative" },
                    { 30, "Metal", "band_metal", "Band artists focused on Metal.", 4, "artists,band,metal" },
                    { 39, "Reggae", "band_reggae", "Band artists focused on Reggae.", 4, "artists,band,reggae" },
                    { 46, "Ska", "band_ska", "Band artists focused on Ska.", 4, "artists,band,ska" },
                    { 51, "Funk", "band_funk", "Band artists focused on Funk.", 4, "artists,band,funk" },
                    { 55, "Soul", "band_soul", "Band artists focused on Soul.", 4, "artists,band,soul" },
                    { 59, "Blues", "band_blues", "Band artists focused on Blues.", 4, "artists,band,blues" },
                    { 63, "Indie", "band_indie", "Band artists focused on Indie.", 4, "artists,band,indie" },
                    { 67, "Folk", "band_folk", "Band artists focused on Folk.", 4, "artists,band,folk" },
                    { 71, "Jazz", "band_jazz", "Band artists focused on Jazz.", 4, "artists,band,jazz" },
                    { 75, "World Music", "band_world_music", "Band artists focused on World Music.", 4, "artists,band,world_music" },
                    { 81, "Rock", "musician_rock", "Musician artists focused on Rock.", 80, "artists,musician,rock" },
                    { 83, "Jazz", "musician_jazz", "Musician artists focused on Jazz.", 80, "artists,musician,jazz" },
                    { 85, "Folk", "musician_folk", "Musician artists focused on Folk.", 80, "artists,musician,folk" },
                    { 87, "Classical", "musician_classical", "Musician artists focused on Classical.", 80, "artists,musician,classical" },
                    { 90, "House", "dj_house", "DJ artists focused on House.", 89, "artists,dj,house" },
                    { 96, "Techno", "dj_techno", "DJ artists focused on Techno.", 89, "artists,dj,techno" },
                    { 100, "Drum and Bass", "dj_drum_and_bass", "DJ artists focused on Drum and Bass.", 89, "artists,dj,drum_and_bass" },
                    { 103, "Trance", "dj_trance", "DJ artists focused on Trance.", 89, "artists,dj,trance" },
                    { 106, "Electronic", "dj_electronic", "DJ artists focused on Electronic.", 89, "artists,dj,electronic" },
                    { 111, "Hip-Hop", "music_producer_hip_hop", "Music Producer artists focused on Hip-Hop.", 110, "artists,music_producer,hip_hop" },
                    { 115, "Electronic", "music_producer_electronic", "Music Producer artists focused on Electronic.", 110, "artists,music_producer,electronic" },
                    { 119, "Sound Art", "audio_artist_sound_art", "Audio Artist artists focused on Sound Art.", 118, "artists,audio_artist,sound_art" },
                    { 123, "Electro-Acoustic", "audio_artist_electro_acoustic", "Audio Artist artists focused on Electro-Acoustic.", 118, "artists,audio_artist,electro_acoustic" },
                    { 126, "Modern Art", "painter_modern_art", "Painter artists focused on Modern Art.", 125, "artists,painter,modern_art" },
                    { 128, "Contemporary Art", "painter_contemporary_art", "Painter artists focused on Contemporary Art.", 125, "artists,painter,contemporary_art" },
                    { 130, "Abstract", "painter_abstract", "Painter artists focused on Abstract.", 125, "artists,painter,abstract" },
                    { 134, "Figurative", "painter_figurative", "Painter artists focused on Figurative.", 125, "artists,painter,figurative" },
                    { 137, "Landscape", "painter_landscape", "Painter artists focused on Landscape.", 125, "artists,painter,landscape" },
                    { 140, "Conceptual Art", "painter_conceptual_art", "Painter artists focused on Conceptual Art.", 125, "artists,painter,conceptual_art" },
                    { 144, "Pop Art", "visual_artist_pop_art", "Visual Artist artists focused on Pop Art.", 143, "artists,visual_artist,pop_art" },
                    { 146, "Street Art", "visual_artist_street_art", "Visual Artist artists focused on Street Art.", 143, "artists,visual_artist,street_art" },
                    { 149, "Lowbrow", "visual_artist_lowbrow", "Visual Artist artists focused on Lowbrow.", 143, "artists,visual_artist,lowbrow" },
                    { 152, "Conceptual Art", "mixed_media_conceptual_art", "Mixed Media artists focused on Conceptual Art.", 151, "artists,mixed_media,conceptual_art" },
                    { 156, "Environmental", "installation_artist_environmental", "Installation Artist artists focused on Environmental.", 155, "artists,installation_artist,environmental" },
                    { 159, "Social Practice", "installation_artist_social_practice", "Installation Artist artists focused on Social Practice.", 155, "artists,installation_artist,social_practice" },
                    { 162, "Functional Forge", "blacksmith_functional_forge", "Blacksmith artists focused on Functional Forge.", 161, "artists,blacksmith,functional_forge" },
                    { 165, "Sculptural Forge", "blacksmith_sculptural_forge", "Blacksmith artists focused on Sculptural Forge.", 161, "artists,blacksmith,sculptural_forge" },
                    { 169, "Furniture", "woodworker_furniture", "Woodworker artists focused on Furniture.", 168, "artists,woodworker,furniture" },
                    { 172, "Sculptural", "woodworker_sculptural", "Woodworker artists focused on Sculptural.", 168, "artists,woodworker,sculptural" },
                    { 175, "Functional Ware", "ceramicist_functional_ware", "Ceramicist artists focused on Functional Ware.", 174, "artists,ceramicist,functional_ware" },
                    { 178, "Sculptural Ceramic", "ceramicist_sculptural_ceramic", "Ceramicist artists focused on Sculptural Ceramic.", 174, "artists,ceramicist,sculptural_ceramic" },
                    { 181, "Wheel-Thrown", "potter_wheel_thrown", "Potter artists focused on Wheel-Thrown.", 180, "artists,potter,wheel_thrown" },
                    { 185, "Jewelry", "metalsmith_jewelry", "Metalsmith artists focused on Jewelry.", 184, "artists,metalsmith,jewelry" },
                    { 189, "Blown Glass", "glass_artist_blown_glass", "Glass Artist artists focused on Blown Glass.", 188, "artists,glass_artist,blown_glass" },
                    { 192, "Fused Glass", "glass_artist_fused_glass", "Glass Artist artists focused on Fused Glass.", 188, "artists,glass_artist,fused_glass" },
                    { 195, "Leather Goods", "leatherworker_leather_goods", "Leatherworker artists focused on Leather Goods.", 194, "artists,leatherworker,leather_goods" },
                    { 199, "String Instruments", "luthier_string_instruments", "Luthier artists focused on String Instruments.", 198, "artists,luthier,string_instruments" },
                    { 203, "Artist Books", "book_arts_artist_books", "Book Arts artists focused on Artist Books.", 202, "artists,book_arts,artist_books" },
                    { 206, "Handmade Paper", "papermaker_handmade_paper", "Papermaker artists focused on Handmade Paper.", 205, "artists,papermaker,handmade_paper" },
                    { 210, "Illustration", "digital_illustrator_illustration", "Digital Illustrator artists focused on Illustration.", 209, "artists,digital_illustrator,illustration" },
                    { 215, "Design", "graphic_designer_design", "Graphic Designer artists focused on Design.", 214, "artists,graphic_designer,design" },
                    { 219, "Motion Graphics", "motion_designer_motion_graphics", "Motion Designer artists focused on Motion Graphics.", 218, "artists,motion_designer,motion_graphics" },
                    { 223, "Animation", "animator_animation", "Animator artists focused on Animation.", 222, "artists,animator,animation" },
                    { 227, "3D Art", "3d_artist_3d_art", "3D Artist artists focused on 3D Art.", 226, "artists,3d_artist,3d_art" },
                    { 231, "Interactive Art", "creative_technologist_interactive_art", "Creative Technologist artists focused on Interactive Art.", 230, "artists,creative_technologist,interactive_art" },
                    { 235, "Augmented Reality", "ar_artist_augmented_reality", "AR Artist artists focused on Augmented Reality.", 234, "artists,ar_artist,augmented_reality" },
                    { 239, "Virtual Reality", "vr_artist_virtual_reality", "VR Artist artists focused on Virtual Reality.", 238, "artists,vr_artist,virtual_reality" },
                    { 243, "Contemporary Dance", "dancer_contemporary_dance", "Dancer artists focused on Contemporary Dance.", 242, "artists,dancer,contemporary_dance" },
                    { 246, "Street Dance", "dancer_street_dance", "Dancer artists focused on Street Dance.", 242, "artists,dancer,street_dance" },
                    { 248, "Ballet", "dancer_ballet", "Dancer artists focused on Ballet.", 242, "artists,dancer,ballet" },
                    { 251, "Contemporary Dance", "choreographer_contemporary_dance", "Choreographer artists focused on Contemporary Dance.", 250, "artists,choreographer,contemporary_dance" },
                    { 254, "Experimental Theater", "theater_performer_experimental_theater", "Theater Performer artists focused on Experimental Theater.", 253, "artists,theater_performer,experimental_theater" },
                    { 256, "Physical Theater", "theater_performer_physical_theater", "Theater Performer artists focused on Physical Theater.", 253, "artists,theater_performer,physical_theater" },
                    { 259, "Contemporary Circus", "circus_artist_contemporary_circus", "Circus Artist artists focused on Contemporary Circus.", 258, "artists,circus_artist,contemporary_circus" },
                    { 262, "Flow Arts", "circus_artist_flow_arts", "Circus Artist artists focused on Flow Arts.", 258, "artists,circus_artist,flow_arts" },
                    { 266, "Poetry", "poet_poetry", "Poet artists focused on Poetry.", 265, "artists,poet,poetry" },
                    { 271, "Fiction", "writer_fiction", "Writer artists focused on Fiction.", 270, "artists,writer,fiction" },
                    { 274, "Nonfiction", "writer_nonfiction", "Writer artists focused on Nonfiction.", 270, "artists,writer,nonfiction" },
                    { 278, "Performance Poetry", "spoken_word_artist_performance_poetry", "Spoken Word Artist artists focused on Performance Poetry.", 277, "artists,spoken_word_artist,performance_poetry" },
                    { 281, "Theater Writing", "playwright_theater_writing", "Playwright artists focused on Theater Writing.", 280, "artists,playwright,theater_writing" }
                });

            migrationBuilder.InsertData(
                table: "ArtistPermissions",
                columns: new[] { "ArtistPermissionsID", "ArtistID", "OwnerRole", "POS_Authorized" },
                values: new object[,]
                {
                    { 1, 1, true, true },
                    { 4, 4, false, false },
                    { 7, 7, false, false }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "ArtistID", "Cost", "Description", "Doors", "EndTime", "EventCategoryID", "MaxOccupancy", "MinimumAge", "Note", "Path", "PointOfContact", "StartTime", "Title", "VenueID" },
                values: new object[,]
                {
                    { 1, 1, 35.00m, "Learn how to create beautiful tie dye patterns in this hands-on workshop!", new DateTime(2025, 6, 3, 13, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 3, 16, 0, 0, 0, DateTimeKind.Utc), 1, 15, 12, "All materials provided. Wear clothes you don't mind getting dye on!", "tie-dye-workshop", "Twisted Passions Team", new DateTime(2025, 6, 3, 14, 0, 0, 0, DateTimeKind.Utc), "Tie Dye Workshop", 1 },
                    { 2, 4, 25.00m, "Introduction to aerial arts. Learn basic climbs, poses, and transitions.", new DateTime(2025, 6, 4, 17, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 4, 19, 30, 0, 0, DateTimeKind.Utc), 2, 10, 16, "Wear fitted athletic clothes. No jewelry. Beginners welcome!", "beginner-aerial-class", "Studio Satarah", new DateTime(2025, 6, 4, 18, 0, 0, 0, DateTimeKind.Utc), "Beginner Aerial Class", 5 },
                    { 3, 4, 30.00m, "Build on your aerial foundation with more complex sequences and transitions.", new DateTime(2025, 6, 6, 17, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 6, 19, 30, 0, 0, DateTimeKind.Utc), 2, 8, 16, "Requires at least 3 months of aerial experience. Bring water!", "intermediate-aerial-class", "Studio Satarah", new DateTime(2025, 6, 6, 18, 0, 0, 0, DateTimeKind.Utc), "Intermediate Aerial Class", 5 },
                    { 4, 4, 35.00m, "Master complex aerial techniques, drops, and performance-ready sequences.", new DateTime(2025, 6, 8, 17, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 8, 19, 30, 0, 0, DateTimeKind.Utc), 2, 6, 18, "For experienced aerialists only. Minimum 1 year of consistent training required.", "advanced-aerial-class", "Studio Satarah", new DateTime(2025, 6, 8, 18, 0, 0, 0, DateTimeKind.Utc), "Advanced Aerial Class", 5 }
                });

            migrationBuilder.InsertData(
                table: "Linker_UserToArtist",
                columns: new[] { "Linker_UserToArtistID", "ArtistID", "Role", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "Member", 1 },
                    { 6, 4, "Member", 3 },
                    { 7, 4, "Member", 6 },
                    { 10, 7, "Member", 1 }
                });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingID", "ArtCategoryID", "ArtistID", "CatalogueID", "CommissionInquiryWelcome", "Credits", "Culture", "Date", "Department", "Description", "Locale", "Locus", "Medium", "Path", "Period", "Price", "ProfilePicID", "Repository", "Rights", "TaxJurisdiction", "Title", "Work_BeginDate", "Work_CompletionDate" },
                values: new object[,]
                {
                    { 2, 3, 1, null, false, null, null, null, null, "A gorgeous custom tie dye", null, null, null, "tiedye1", null, 19.99m, 9, null, null, null, "Tie Dye Shirt", null, null },
                    { 3, 3, 1, null, false, null, null, null, null, "A gorgeous custom tie dye", null, null, null, "tiedye2", null, 29.99m, 9, null, null, null, "Tie Dye Shirt2", null, null },
                    { 4, 3, 1, null, false, null, null, null, null, "A gorgeous custom tie dye", null, null, null, "tiedye3", null, 39.99m, 9, null, null, null, "Tie Dye Shirt3", null, null }
                });

            migrationBuilder.InsertData(
                table: "ArtistCategories",
                columns: new[] { "ArtistCategoryID", "Category", "CategoryKey", "Description", "ParentArtistCategoryID", "Tags" },
                values: new object[,]
                {
                    { 6, "Classic Rock", "band_rock_classic_rock", "Band artists creating Classic Rock within Rock.", 5, "artists,band,rock,classic_rock" },
                    { 7, "Hard Rock", "band_rock_hard_rock", "Band artists creating Hard Rock within Rock.", 5, "artists,band,rock,hard_rock" },
                    { 8, "Soft Rock", "band_rock_soft_rock", "Band artists creating Soft Rock within Rock.", 5, "artists,band,rock,soft_rock" },
                    { 9, "Garage Rock", "band_rock_garage_rock", "Band artists creating Garage Rock within Rock.", 5, "artists,band,rock,garage_rock" },
                    { 10, "Psychedelic Rock", "band_rock_psychedelic_rock", "Band artists creating Psychedelic Rock within Rock.", 5, "artists,band,rock,psychedelic_rock" },
                    { 11, "Progressive Rock", "band_rock_progressive_rock", "Band artists creating Progressive Rock within Rock.", 5, "artists,band,rock,progressive_rock" },
                    { 12, "Art Rock", "band_rock_art_rock", "Band artists creating Art Rock within Rock.", 5, "artists,band,rock,art_rock" },
                    { 13, "Blues Rock", "band_rock_blues_rock", "Band artists creating Blues Rock within Rock.", 5, "artists,band,rock,blues_rock" },
                    { 14, "Stoner Rock", "band_rock_stoner_rock", "Band artists creating Stoner Rock within Rock.", 5, "artists,band,rock,stoner_rock" },
                    { 15, "Desert Rock", "band_rock_desert_rock", "Band artists creating Desert Rock within Rock.", 5, "artists,band,rock,desert_rock" },
                    { 17, "Punk Rock", "band_punk_punk_rock", "Band artists creating Punk Rock within Punk.", 16, "artists,band,punk,punk_rock" },
                    { 18, "Hardcore Punk", "band_punk_hardcore_punk", "Band artists creating Hardcore Punk within Punk.", 16, "artists,band,punk,hardcore_punk" },
                    { 19, "Post-Punk", "band_punk_post_punk", "Band artists creating Post-Punk within Punk.", 16, "artists,band,punk,post_punk" },
                    { 20, "Anarcho-Punk", "band_punk_anarcho_punk", "Band artists creating Anarcho-Punk within Punk.", 16, "artists,band,punk,anarcho_punk" },
                    { 21, "Street Punk", "band_punk_street_punk", "Band artists creating Street Punk within Punk.", 16, "artists,band,punk,street_punk" },
                    { 22, "Crossover Thrash", "band_punk_crossover_thrash", "Band artists creating Crossover Thrash within Punk.", 16, "artists,band,punk,crossover_thrash" },
                    { 23, "Pop Punk", "band_punk_pop_punk", "Band artists creating Pop Punk within Punk.", 16, "artists,band,punk,pop_punk" },
                    { 25, "Alternative Rock", "band_alternative_alternative_rock", "Band artists creating Alternative Rock within Alternative.", 24, "artists,band,alternative,alternative_rock" },
                    { 26, "Indie Rock", "band_alternative_indie_rock", "Band artists creating Indie Rock within Alternative.", 24, "artists,band,alternative,indie_rock" },
                    { 27, "College Rock", "band_alternative_college_rock", "Band artists creating College Rock within Alternative.", 24, "artists,band,alternative,college_rock" },
                    { 28, "Noise Rock", "band_alternative_noise_rock", "Band artists creating Noise Rock within Alternative.", 24, "artists,band,alternative,noise_rock" },
                    { 29, "Post-Rock", "band_alternative_post_rock", "Band artists creating Post-Rock within Alternative.", 24, "artists,band,alternative,post_rock" },
                    { 31, "Heavy Metal", "band_metal_heavy_metal", "Band artists creating Heavy Metal within Metal.", 30, "artists,band,metal,heavy_metal" },
                    { 32, "Thrash Metal", "band_metal_thrash_metal", "Band artists creating Thrash Metal within Metal.", 30, "artists,band,metal,thrash_metal" },
                    { 33, "Death Metal", "band_metal_death_metal", "Band artists creating Death Metal within Metal.", 30, "artists,band,metal,death_metal" },
                    { 34, "Black Metal", "band_metal_black_metal", "Band artists creating Black Metal within Metal.", 30, "artists,band,metal,black_metal" },
                    { 35, "Doom Metal", "band_metal_doom_metal", "Band artists creating Doom Metal within Metal.", 30, "artists,band,metal,doom_metal" },
                    { 36, "Sludge Metal", "band_metal_sludge_metal", "Band artists creating Sludge Metal within Metal.", 30, "artists,band,metal,sludge_metal" },
                    { 37, "Post-Metal", "band_metal_post_metal", "Band artists creating Post-Metal within Metal.", 30, "artists,band,metal,post_metal" },
                    { 38, "Power Metal", "band_metal_power_metal", "Band artists creating Power Metal within Metal.", 30, "artists,band,metal,power_metal" },
                    { 40, "Roots Reggae", "band_reggae_roots_reggae", "Band artists creating Roots Reggae within Reggae.", 39, "artists,band,reggae,roots_reggae" },
                    { 41, "Dub", "band_reggae_dub", "Band artists creating Dub within Reggae.", 39, "artists,band,reggae,dub" },
                    { 42, "Dancehall", "band_reggae_dancehall", "Band artists creating Dancehall within Reggae.", 39, "artists,band,reggae,dancehall" },
                    { 43, "Rocksteady", "band_reggae_rocksteady", "Band artists creating Rocksteady within Reggae.", 39, "artists,band,reggae,rocksteady" },
                    { 44, "Conscious Reggae", "band_reggae_conscious_reggae", "Band artists creating Conscious Reggae within Reggae.", 39, "artists,band,reggae,conscious_reggae" },
                    { 45, "Reggae Fusion", "band_reggae_reggae_fusion", "Band artists creating Reggae Fusion within Reggae.", 39, "artists,band,reggae,reggae_fusion" },
                    { 47, "Traditional Ska", "band_ska_traditional_ska", "Band artists creating Traditional Ska within Ska.", 46, "artists,band,ska,traditional_ska" },
                    { 48, "2 Tone Ska", "band_ska_2_tone_ska", "Band artists creating 2 Tone Ska within Ska.", 46, "artists,band,ska,2_tone_ska" },
                    { 49, "Ska Punk", "band_ska_ska_punk", "Band artists creating Ska Punk within Ska.", 46, "artists,band,ska,ska_punk" },
                    { 50, "Ska Rock", "band_ska_ska_rock", "Band artists creating Ska Rock within Ska.", 46, "artists,band,ska,ska_rock" },
                    { 52, "Funk Rock", "band_funk_funk_rock", "Band artists creating Funk Rock within Funk.", 51, "artists,band,funk,funk_rock" },
                    { 53, "P-Funk", "band_funk_p_funk", "Band artists creating P-Funk within Funk.", 51, "artists,band,funk,p_funk" },
                    { 54, "Neo-Funk", "band_funk_neo_funk", "Band artists creating Neo-Funk within Funk.", 51, "artists,band,funk,neo_funk" },
                    { 56, "Classic Soul", "band_soul_classic_soul", "Band artists creating Classic Soul within Soul.", 55, "artists,band,soul,classic_soul" },
                    { 57, "Psych Soul", "band_soul_psych_soul", "Band artists creating Psych Soul within Soul.", 55, "artists,band,soul,psych_soul" },
                    { 58, "Neo-Soul", "band_soul_neo_soul", "Band artists creating Neo-Soul within Soul.", 55, "artists,band,soul,neo_soul" },
                    { 60, "Delta Blues", "band_blues_delta_blues", "Band artists creating Delta Blues within Blues.", 59, "artists,band,blues,delta_blues" },
                    { 61, "Chicago Blues", "band_blues_chicago_blues", "Band artists creating Chicago Blues within Blues.", 59, "artists,band,blues,chicago_blues" },
                    { 62, "Electric Blues", "band_blues_electric_blues", "Band artists creating Electric Blues within Blues.", 59, "artists,band,blues,electric_blues" },
                    { 64, "Indie Folk", "band_indie_indie_folk", "Band artists creating Indie Folk within Indie.", 63, "artists,band,indie,indie_folk" },
                    { 65, "Indie Pop", "band_indie_indie_pop", "Band artists creating Indie Pop within Indie.", 63, "artists,band,indie,indie_pop" },
                    { 66, "Lo-Fi Indie", "band_indie_lo_fi_indie", "Band artists creating Lo-Fi Indie within Indie.", 63, "artists,band,indie,lo_fi_indie" },
                    { 68, "Traditional Folk", "band_folk_traditional_folk", "Band artists creating Traditional Folk within Folk.", 67, "artists,band,folk,traditional_folk" },
                    { 69, "Americana", "band_folk_americana", "Band artists creating Americana within Folk.", 67, "artists,band,folk,americana" },
                    { 70, "Psych Folk", "band_folk_psych_folk", "Band artists creating Psych Folk within Folk.", 67, "artists,band,folk,psych_folk" },
                    { 72, "Jazz Fusion", "band_jazz_jazz_fusion", "Band artists creating Jazz Fusion within Jazz.", 71, "artists,band,jazz,jazz_fusion" },
                    { 73, "Avant-Garde Jazz", "band_jazz_avant_garde_jazz", "Band artists creating Avant-Garde Jazz within Jazz.", 71, "artists,band,jazz,avant_garde_jazz" },
                    { 74, "Free Jazz", "band_jazz_free_jazz", "Band artists creating Free Jazz within Jazz.", 71, "artists,band,jazz,free_jazz" },
                    { 76, "Afrobeat", "band_world_music_afrobeat", "Band artists creating Afrobeat within World Music.", 75, "artists,band,world_music,afrobeat" },
                    { 77, "Latin Rock", "band_world_music_latin_rock", "Band artists creating Latin Rock within World Music.", 75, "artists,band,world_music,latin_rock" },
                    { 78, "Cumbia", "band_world_music_cumbia", "Band artists creating Cumbia within World Music.", 75, "artists,band,world_music,cumbia" },
                    { 79, "Highlife", "band_world_music_highlife", "Band artists creating Highlife within World Music.", 75, "artists,band,world_music,highlife" },
                    { 82, "Solo Rock", "musician_rock_solo_rock", "Musician artists creating Solo Rock within Rock.", 81, "artists,musician,rock,solo_rock" },
                    { 84, "Improvisational Jazz", "musician_jazz_improvisational_jazz", "Musician artists creating Improvisational Jazz within Jazz.", 83, "artists,musician,jazz,improvisational_jazz" },
                    { 86, "Singer-Songwriter", "musician_folk_singer_songwriter", "Musician artists creating Singer-Songwriter within Folk.", 85, "artists,musician,folk,singer_songwriter" },
                    { 88, "Contemporary Classical", "musician_classical_contemporary_classical", "Musician artists creating Contemporary Classical within Classical.", 87, "artists,musician,classical,contemporary_classical" },
                    { 91, "Deep House", "dj_house_deep_house", "DJ artists creating Deep House within House.", 90, "artists,dj,house,deep_house" },
                    { 92, "Tech House", "dj_house_tech_house", "DJ artists creating Tech House within House.", 90, "artists,dj,house,tech_house" },
                    { 93, "Minimal House", "dj_house_minimal_house", "DJ artists creating Minimal House within House.", 90, "artists,dj,house,minimal_house" },
                    { 94, "Progressive House", "dj_house_progressive_house", "DJ artists creating Progressive House within House.", 90, "artists,dj,house,progressive_house" },
                    { 95, "Acid House", "dj_house_acid_house", "DJ artists creating Acid House within House.", 90, "artists,dj,house,acid_house" },
                    { 97, "Detroit Techno", "dj_techno_detroit_techno", "DJ artists creating Detroit Techno within Techno.", 96, "artists,dj,techno,detroit_techno" },
                    { 98, "Minimal Techno", "dj_techno_minimal_techno", "DJ artists creating Minimal Techno within Techno.", 96, "artists,dj,techno,minimal_techno" },
                    { 99, "Industrial Techno", "dj_techno_industrial_techno", "DJ artists creating Industrial Techno within Techno.", 96, "artists,dj,techno,industrial_techno" },
                    { 101, "Liquid DnB", "dj_drum_and_bass_liquid_dnb", "DJ artists creating Liquid DnB within Drum and Bass.", 100, "artists,dj,drum_and_bass,liquid_dnb" },
                    { 102, "Jungle", "dj_drum_and_bass_jungle", "DJ artists creating Jungle within Drum and Bass.", 100, "artists,dj,drum_and_bass,jungle" },
                    { 104, "Uplifting Trance", "dj_trance_uplifting_trance", "DJ artists creating Uplifting Trance within Trance.", 103, "artists,dj,trance,uplifting_trance" },
                    { 105, "Psytrance", "dj_trance_psytrance", "DJ artists creating Psytrance within Trance.", 103, "artists,dj,trance,psytrance" },
                    { 107, "IDM", "dj_electronic_idm", "DJ artists creating IDM within Electronic.", 106, "artists,dj,electronic,idm" },
                    { 108, "Glitch", "dj_electronic_glitch", "DJ artists creating Glitch within Electronic.", 106, "artists,dj,electronic,glitch" },
                    { 109, "Experimental Electronic", "dj_electronic_experimental_electronic", "DJ artists creating Experimental Electronic within Electronic.", 106, "artists,dj,electronic,experimental_electronic" },
                    { 112, "Boom Bap", "music_producer_hip_hop_boom_bap", "Music Producer artists creating Boom Bap within Hip-Hop.", 111, "artists,music_producer,hip_hop,boom_bap" },
                    { 113, "Trap", "music_producer_hip_hop_trap", "Music Producer artists creating Trap within Hip-Hop.", 111, "artists,music_producer,hip_hop,trap" },
                    { 114, "Drill", "music_producer_hip_hop_drill", "Music Producer artists creating Drill within Hip-Hop.", 111, "artists,music_producer,hip_hop,drill" },
                    { 116, "Lo-Fi Beats", "music_producer_electronic_lo_fi_beats", "Music Producer artists creating Lo-Fi Beats within Electronic.", 115, "artists,music_producer,electronic,lo_fi_beats" },
                    { 117, "Ambient Electronic", "music_producer_electronic_ambient_electronic", "Music Producer artists creating Ambient Electronic within Electronic.", 115, "artists,music_producer,electronic,ambient_electronic" },
                    { 120, "Soundscape", "audio_artist_sound_art_soundscape", "Audio Artist artists creating Soundscape within Sound Art.", 119, "artists,audio_artist,sound_art,soundscape" },
                    { 121, "Field Recording", "audio_artist_sound_art_field_recording", "Audio Artist artists creating Field Recording within Sound Art.", 119, "artists,audio_artist,sound_art,field_recording" },
                    { 122, "Noise", "audio_artist_sound_art_noise", "Audio Artist artists creating Noise within Sound Art.", 119, "artists,audio_artist,sound_art,noise" },
                    { 124, "Experimental Audio", "audio_artist_electro_acoustic_experimental_audio", "Audio Artist artists creating Experimental Audio within Electro-Acoustic.", 123, "artists,audio_artist,electro_acoustic,experimental_audio" },
                    { 127, "Mid-Century Modern", "painter_modern_art_mid_century_modern", "Painter artists creating Mid-Century Modern within Modern Art.", 126, "artists,painter,modern_art,mid_century_modern" },
                    { 129, "Post-Modern", "painter_contemporary_art_post_modern", "Painter artists creating Post-Modern within Contemporary Art.", 128, "artists,painter,contemporary_art,post_modern" },
                    { 131, "Geometric Abstraction", "painter_abstract_geometric_abstraction", "Painter artists creating Geometric Abstraction within Abstract.", 130, "artists,painter,abstract,geometric_abstraction" },
                    { 132, "Color Field", "painter_abstract_color_field", "Painter artists creating Color Field within Abstract.", 130, "artists,painter,abstract,color_field" },
                    { 133, "Gestural", "painter_abstract_gestural", "Painter artists creating Gestural within Abstract.", 130, "artists,painter,abstract,gestural" },
                    { 135, "Contemporary Figurative", "painter_figurative_contemporary_figurative", "Painter artists creating Contemporary Figurative within Figurative.", 134, "artists,painter,figurative,contemporary_figurative" },
                    { 136, "Portraiture", "painter_figurative_portraiture", "Painter artists creating Portraiture within Figurative.", 134, "artists,painter,figurative,portraiture" },
                    { 138, "Urban Landscape", "painter_landscape_urban_landscape", "Painter artists creating Urban Landscape within Landscape.", 137, "artists,painter,landscape,urban_landscape" },
                    { 139, "Natural Landscape", "painter_landscape_natural_landscape", "Painter artists creating Natural Landscape within Landscape.", 137, "artists,painter,landscape,natural_landscape" },
                    { 141, "Process-Based", "painter_conceptual_art_process_based", "Painter artists creating Process-Based within Conceptual Art.", 140, "artists,painter,conceptual_art,process_based" },
                    { 142, "Site-Specific", "painter_conceptual_art_site_specific", "Painter artists creating Site-Specific within Conceptual Art.", 140, "artists,painter,conceptual_art,site_specific" },
                    { 145, "Neo-Pop", "visual_artist_pop_art_neo_pop", "Visual Artist artists creating Neo-Pop within Pop Art.", 144, "artists,visual_artist,pop_art,neo_pop" },
                    { 147, "Graffiti", "visual_artist_street_art_graffiti", "Visual Artist artists creating Graffiti within Street Art.", 146, "artists,visual_artist,street_art,graffiti" },
                    { 148, "Mural-Based", "visual_artist_street_art_mural_based", "Visual Artist artists creating Mural-Based within Street Art.", 146, "artists,visual_artist,street_art,mural_based" },
                    { 150, "Pop Surrealism", "visual_artist_lowbrow_pop_surrealism", "Visual Artist artists creating Pop Surrealism within Lowbrow.", 149, "artists,visual_artist,lowbrow,pop_surrealism" },
                    { 153, "Assemblage", "mixed_media_conceptual_art_assemblage", "Mixed Media artists creating Assemblage within Conceptual Art.", 152, "artists,mixed_media,conceptual_art,assemblage" },
                    { 154, "Collage", "mixed_media_conceptual_art_collage", "Mixed Media artists creating Collage within Conceptual Art.", 152, "artists,mixed_media,conceptual_art,collage" },
                    { 157, "Immersive", "installation_artist_environmental_immersive", "Installation Artist artists creating Immersive within Environmental.", 156, "artists,installation_artist,environmental,immersive" },
                    { 158, "Interactive", "installation_artist_environmental_interactive", "Installation Artist artists creating Interactive within Environmental.", 156, "artists,installation_artist,environmental,interactive" },
                    { 160, "Community-Based", "installation_artist_social_practice_community_base", "Installation Artist artists creating Community-Based within Social Practice.", 159, "artists,installation_artist,social_practice,community_based" },
                    { 163, "Tools", "blacksmith_functional_forge_tools", "Blacksmith artists creating Tools within Functional Forge.", 162, "artists,blacksmith,functional_forge,tools" },
                    { 164, "Furniture", "blacksmith_functional_forge_furniture", "Blacksmith artists creating Furniture within Functional Forge.", 162, "artists,blacksmith,functional_forge,furniture" },
                    { 166, "Abstract Form", "blacksmith_sculptural_forge_abstract_form", "Blacksmith artists creating Abstract Form within Sculptural Forge.", 165, "artists,blacksmith,sculptural_forge,abstract_form" },
                    { 167, "Architectural", "blacksmith_sculptural_forge_architectural", "Blacksmith artists creating Architectural within Sculptural Forge.", 165, "artists,blacksmith,sculptural_forge,architectural" },
                    { 170, "Modern", "woodworker_furniture_modern", "Woodworker artists creating Modern within Furniture.", 169, "artists,woodworker,furniture,modern" },
                    { 171, "Rustic", "woodworker_furniture_rustic", "Woodworker artists creating Rustic within Furniture.", 169, "artists,woodworker,furniture,rustic" },
                    { 173, "Carved Forms", "woodworker_sculptural_carved_forms", "Woodworker artists creating Carved Forms within Sculptural.", 172, "artists,woodworker,sculptural,carved_forms" },
                    { 176, "Dinnerware", "ceramicist_functional_ware_dinnerware", "Ceramicist artists creating Dinnerware within Functional Ware.", 175, "artists,ceramicist,functional_ware,dinnerware" },
                    { 177, "Vessels", "ceramicist_functional_ware_vessels", "Ceramicist artists creating Vessels within Functional Ware.", 175, "artists,ceramicist,functional_ware,vessels" },
                    { 179, "Abstract", "ceramicist_sculptural_ceramic_abstract", "Ceramicist artists creating Abstract within Sculptural Ceramic.", 178, "artists,ceramicist,sculptural_ceramic,abstract" },
                    { 182, "Traditional Forms", "potter_wheel_thrown_traditional_forms", "Potter artists creating Traditional Forms within Wheel-Thrown.", 181, "artists,potter,wheel_thrown,traditional_forms" },
                    { 183, "Contemporary Forms", "potter_wheel_thrown_contemporary_forms", "Potter artists creating Contemporary Forms within Wheel-Thrown.", 181, "artists,potter,wheel_thrown,contemporary_forms" },
                    { 186, "Hand-Fabricated", "metalsmith_jewelry_hand_fabricated", "Metalsmith artists creating Hand-Fabricated within Jewelry.", 185, "artists,metalsmith,jewelry,hand_fabricated" },
                    { 187, "Casting", "metalsmith_jewelry_casting", "Metalsmith artists creating Casting within Jewelry.", 185, "artists,metalsmith,jewelry,casting" },
                    { 190, "Functional", "glass_artist_blown_glass_functional", "Glass Artist artists creating Functional within Blown Glass.", 189, "artists,glass_artist,blown_glass,functional" },
                    { 191, "Sculptural", "glass_artist_blown_glass_sculptural", "Glass Artist artists creating Sculptural within Blown Glass.", 189, "artists,glass_artist,blown_glass,sculptural" },
                    { 193, "Panels", "glass_artist_fused_glass_panels", "Glass Artist artists creating Panels within Fused Glass.", 192, "artists,glass_artist,fused_glass,panels" },
                    { 196, "Bags", "leatherworker_leather_goods_bags", "Leatherworker artists creating Bags within Leather Goods.", 195, "artists,leatherworker,leather_goods,bags" },
                    { 197, "Footwear", "leatherworker_leather_goods_footwear", "Leatherworker artists creating Footwear within Leather Goods.", 195, "artists,leatherworker,leather_goods,footwear" },
                    { 200, "Guitar", "luthier_string_instruments_guitar", "Luthier artists creating Guitar within String Instruments.", 199, "artists,luthier,string_instruments,guitar" },
                    { 201, "Violin", "luthier_string_instruments_violin", "Luthier artists creating Violin within String Instruments.", 199, "artists,luthier,string_instruments,violin" },
                    { 204, "Conceptual Books", "book_arts_artist_books_conceptual_books", "Book Arts artists creating Conceptual Books within Artist Books.", 203, "artists,book_arts,artist_books,conceptual_books" },
                    { 207, "Textured Sheets", "papermaker_handmade_paper_textured_sheets", "Papermaker artists creating Textured Sheets within Handmade Paper.", 206, "artists,papermaker,handmade_paper,textured_sheets" },
                    { 208, "Art Paper", "papermaker_handmade_paper_art_paper", "Papermaker artists creating Art Paper within Handmade Paper.", 206, "artists,papermaker,handmade_paper,art_paper" },
                    { 211, "Character Design", "digital_illustrator_illustration_character_design", "Digital Illustrator artists creating Character Design within Illustration.", 210, "artists,digital_illustrator,illustration,character_design" },
                    { 212, "Editorial Illustration", "digital_illustrator_illustration_editorial_illustr", "Digital Illustrator artists creating Editorial Illustration within Illustration.", 210, "artists,digital_illustrator,illustration,editorial_illustration" },
                    { 213, "Concept Art", "digital_illustrator_illustration_concept_art", "Digital Illustrator artists creating Concept Art within Illustration.", 210, "artists,digital_illustrator,illustration,concept_art" },
                    { 216, "Brand Identity", "graphic_designer_design_brand_identity", "Graphic Designer artists creating Brand Identity within Design.", 215, "artists,graphic_designer,design,brand_identity" },
                    { 217, "Typography", "graphic_designer_design_typography", "Graphic Designer artists creating Typography within Design.", 215, "artists,graphic_designer,design,typography" },
                    { 220, "Kinetic Typography", "motion_designer_motion_graphics_kinetic_typography", "Motion Designer artists creating Kinetic Typography within Motion Graphics.", 219, "artists,motion_designer,motion_graphics,kinetic_typography" },
                    { 221, "Title Design", "motion_designer_motion_graphics_title_design", "Motion Designer artists creating Title Design within Motion Graphics.", 219, "artists,motion_designer,motion_graphics,title_design" },
                    { 224, "2D Animation", "animator_animation_2d_animation", "Animator artists creating 2D Animation within Animation.", 223, "artists,animator,animation,2d_animation" },
                    { 225, "3D Animation", "animator_animation_3d_animation", "Animator artists creating 3D Animation within Animation.", 223, "artists,animator,animation,3d_animation" },
                    { 228, "Stylized 3D", "3d_artist_3d_art_stylized_3d", "3D Artist artists creating Stylized 3D within 3D Art.", 227, "artists,3d_artist,3d_art,stylized_3d" },
                    { 229, "Photorealism", "3d_artist_3d_art_photorealism", "3D Artist artists creating Photorealism within 3D Art.", 227, "artists,3d_artist,3d_art,photorealism" },
                    { 232, "Sensor-Based", "creative_technologist_interactive_art_sensor_based", "Creative Technologist artists creating Sensor-Based within Interactive Art.", 231, "artists,creative_technologist,interactive_art,sensor_based" },
                    { 233, "Installation-Based", "creative_technologist_interactive_art_installation", "Creative Technologist artists creating Installation-Based within Interactive Art.", 231, "artists,creative_technologist,interactive_art,installation_based" },
                    { 236, "Site-Based AR", "ar_artist_augmented_reality_site_based_ar", "AR Artist artists creating Site-Based AR within Augmented Reality.", 235, "artists,ar_artist,augmented_reality,site_based_ar" },
                    { 237, "Mobile AR", "ar_artist_augmented_reality_mobile_ar", "AR Artist artists creating Mobile AR within Augmented Reality.", 235, "artists,ar_artist,augmented_reality,mobile_ar" },
                    { 240, "Immersive World", "vr_artist_virtual_reality_immersive_world", "VR Artist artists creating Immersive World within Virtual Reality.", 239, "artists,vr_artist,virtual_reality,immersive_world" },
                    { 241, "Narrative VR", "vr_artist_virtual_reality_narrative_vr", "VR Artist artists creating Narrative VR within Virtual Reality.", 239, "artists,vr_artist,virtual_reality,narrative_vr" },
                    { 244, "Improvisation", "dancer_contemporary_dance_improvisation", "Dancer artists creating Improvisation within Contemporary Dance.", 243, "artists,dancer,contemporary_dance,improvisation" },
                    { 245, "Floor Work", "dancer_contemporary_dance_floor_work", "Dancer artists creating Floor Work within Contemporary Dance.", 243, "artists,dancer,contemporary_dance,floor_work" },
                    { 247, "Freestyle", "dancer_street_dance_freestyle", "Dancer artists creating Freestyle within Street Dance.", 246, "artists,dancer,street_dance,freestyle" },
                    { 249, "Classical Ballet", "dancer_ballet_classical_ballet", "Dancer artists creating Classical Ballet within Ballet.", 248, "artists,dancer,ballet,classical_ballet" },
                    { 252, "Conceptual Choreography", "choreographer_contemporary_dance_conceptual_choreo", "Choreographer artists creating Conceptual Choreography within Contemporary Dance.", 251, "artists,choreographer,contemporary_dance,conceptual_choreography" },
                    { 255, "Devised Performance", "theater_performer_experimental_theater_devised_per", "Theater Performer artists creating Devised Performance within Experimental Theater.", 254, "artists,theater_performer,experimental_theater,devised_performance" },
                    { 257, "Movement-Based", "theater_performer_physical_theater_movement_based", "Theater Performer artists creating Movement-Based within Physical Theater.", 256, "artists,theater_performer,physical_theater,movement_based" },
                    { 260, "Aerial Silks", "circus_artist_contemporary_circus_aerial_silks", "Circus Artist artists creating Aerial Silks within Contemporary Circus.", 259, "artists,circus_artist,contemporary_circus,aerial_silks" },
                    { 261, "Aerial Hoop", "circus_artist_contemporary_circus_aerial_hoop", "Circus Artist artists creating Aerial Hoop within Contemporary Circus.", 259, "artists,circus_artist,contemporary_circus,aerial_hoop" },
                    { 263, "Fire Spinning", "circus_artist_flow_arts_fire_spinning", "Circus Artist artists creating Fire Spinning within Flow Arts.", 262, "artists,circus_artist,flow_arts,fire_spinning" },
                    { 264, "LED Performance", "circus_artist_flow_arts_led_performance", "Circus Artist artists creating LED Performance within Flow Arts.", 262, "artists,circus_artist,flow_arts,led_performance" },
                    { 267, "Free Verse", "poet_poetry_free_verse", "Poet artists creating Free Verse within Poetry.", 266, "artists,poet,poetry,free_verse" },
                    { 268, "Spoken Poetry", "poet_poetry_spoken_poetry", "Poet artists creating Spoken Poetry within Poetry.", 266, "artists,poet,poetry,spoken_poetry" },
                    { 269, "Slam Poetry", "poet_poetry_slam_poetry", "Poet artists creating Slam Poetry within Poetry.", 266, "artists,poet,poetry,slam_poetry" },
                    { 272, "Short Stories", "writer_fiction_short_stories", "Writer artists creating Short Stories within Fiction.", 271, "artists,writer,fiction,short_stories" },
                    { 273, "Speculative Fiction", "writer_fiction_speculative_fiction", "Writer artists creating Speculative Fiction within Fiction.", 271, "artists,writer,fiction,speculative_fiction" },
                    { 275, "Essays", "writer_nonfiction_essays", "Writer artists creating Essays within Nonfiction.", 274, "artists,writer,nonfiction,essays" },
                    { 276, "Memoir", "writer_nonfiction_memoir", "Writer artists creating Memoir within Nonfiction.", 274, "artists,writer,nonfiction,memoir" },
                    { 279, "Narrative Spoken Word", "spoken_word_artist_performance_poetry_narrative_sp", "Spoken Word Artist artists creating Narrative Spoken Word within Performance Poetry.", 278, "artists,spoken_word_artist,performance_poetry,narrative_spoken_word" },
                    { 282, "One-Act Plays", "playwright_theater_writing_one_act_plays", "Playwright artists creating One-Act Plays within Theater Writing.", 281, "artists,playwright,theater_writing,one_act_plays" },
                    { 283, "Full-Length Plays", "playwright_theater_writing_full_length_plays", "Playwright artists creating Full-Length Plays within Theater Writing.", 281, "artists,playwright,theater_writing,full_length_plays" }
                });

            migrationBuilder.InsertData(
                table: "Linker_ArtistToCategories",
                columns: new[] { "Linker_ArtistToCategoryID", "ArtistCategoryID", "ArtistID", "ExpertiseLevel", "Genre", "IsProfessional" },
                values: new object[] { 2, 16, 2, "Intermediate", "Acrylic Painting", false });

            migrationBuilder.InsertData(
                table: "Listings",
                columns: new[] { "ListingID", "ArtCategoryID", "ArtistID", "CatalogueID", "CommissionInquiryWelcome", "Credits", "Culture", "Date", "Department", "Description", "Locale", "Locus", "Medium", "Path", "Period", "Price", "ProfilePicID", "Repository", "Rights", "TaxJurisdiction", "Title", "Work_BeginDate", "Work_CompletionDate" },
                values: new object[] { 1, 30, 4, null, false, null, null, null, null, "Funny when they kiss!", null, null, null, "booty-shorts", null, 199.99m, 9, null, null, null, "Studio Satarah Booty Shorts", null, null });

            migrationBuilder.InsertData(
                table: "Linker_ArtistToCategories",
                columns: new[] { "Linker_ArtistToCategoryID", "ArtistCategoryID", "ArtistID", "ExpertiseLevel", "Genre", "IsProfessional" },
                values: new object[,]
                {
                    { 1, 29, 1, "Advanced", "Tie Dye", true },
                    { 3, 45, 3, "Advanced", "Fire Flow", true },
                    { 4, 43, 4, "Advanced", "Aerial Arts", true },
                    { 5, 10, 5, "Advanced", "House Music", true },
                    { 6, 40, 6, "Advanced", "Reggae Rock", true },
                    { 7, 45, 7, "Intermediate", "Fire Flow", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtCategories_CategoryKey",
                table: "ArtCategories",
                column: "CategoryKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArtCategories_ParentArtCategoryID",
                table: "ArtCategories",
                column: "ParentArtCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistCategories_CategoryKey",
                table: "ArtistCategories",
                column: "CategoryKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArtistCategories_ParentArtistCategoryID",
                table: "ArtistCategories",
                column: "ParentArtistCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistPermissions_ArtistID",
                table: "ArtistPermissions",
                column: "ArtistID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_CoverPicID",
                table: "Artists",
                column: "CoverPicID");

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
                name: "IX_EventCategories_CategoryKey",
                table: "EventCategories",
                column: "CategoryKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ArtistID",
                table: "Events",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventCategoryID",
                table: "Events",
                column: "EventCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_Path",
                table: "Events",
                column: "Path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueID",
                table: "Events",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_Fields_Forms_MetadataID",
                table: "Forms_Fields",
                column: "Forms_MetadataID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_ArtistToCategories_ArtistCategoryID",
                table: "Linker_ArtistToCategories",
                column: "ArtistCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Linker_ArtistToCategories_ArtistID_ArtistCategoryID",
                table: "Linker_ArtistToCategories",
                columns: new[] { "ArtistID", "ArtistCategoryID" },
                unique: true);

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
                name: "Linker_ArtistToCategories");

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
                name: "ArtistCategories");

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
                name: "EventCategories");

            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropTable(
                name: "StaffRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ArtCategories");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ExternalLinks");

            migrationBuilder.DropTable(
                name: "PhoneContacts");

            migrationBuilder.DropTable(
                name: "Pictures");
        }
    }
}
