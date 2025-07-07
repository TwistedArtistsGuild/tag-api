// <copyright file="TAGDBContext.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using TAGWEBAPI.Models;

    public class TAGDBContext : DbContext
    {
        public TAGDBContext(DbContextOptions<TAGDBContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ArtCategory> ArtCategories { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<ArtistPermissions> ArtistPermissions { get; set; }

        public DbSet<BannedList> BannedLists { get; set; }

        public DbSet<BannedReason> BannedReasons { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<CompetitionListing> CompetitionListings { get; set; }

        public DbSet<CompetitionVoteList> CompetitionVoteLists { get; set; }

        public DbSet<CompetitionWinnerList> CompetitionWinnerLists { get; set; }

        public DbSet<DigitalDeliverySpecs> DigitalDeliverySpecs { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<ExternalLink> ExternalLinks { get; set; }

        public DbSet<Forms_Field> Forms_Fields { get; set; }

        public DbSet<Forms_Metadata> Forms_Metadata { get; set; }

        public DbSet<Linker_TicketToEvent> Linker_TicketToEvents { get; set; }

        public DbSet<Linker_TransactionLineItem> Linker_TransactionLineItems { get; set; }

        public DbSet<Linker_UserFavorite> Linker_UserFavorites { get; set; }

        public DbSet<Linker_UserAndArtistToContact> Linker_UserAndArtistToContacts { get; set; }

        public DbSet<Linker_VendorToUser> Linker_VendorToUsers { get; set; }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<PhoneContact> PhoneContacts { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Resolution> Resolutions { get; set; }

        public DbSet<ShippingSpecs> ShippingSpecs { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<StaffRole> StaffRoles { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketType> TicketTypes { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserPreference> UserPreferences { get; set; }

        public DbSet<UserPrivacy> UserPrivacies { get; set; }

        public DbSet<UserSettings> UserSettings { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Vote> Votes { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Forms_MetadataConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Forms_FieldConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ArtCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.PhoneContactConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ExternalLinkConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.AddressConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.PictureConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.BlogConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.LinkerUserToArtistConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.LogConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ListingConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.StaffConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.StaffRoleConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.TicketConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.TicketTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.VenueConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ArtistPermissionsConfiguration());

        }
    }
}

