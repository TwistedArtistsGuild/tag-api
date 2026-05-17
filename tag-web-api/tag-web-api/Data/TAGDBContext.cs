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

        public DbSet<ArtistCategory> ArtistCategories { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<ArtistPermissions> ArtistPermissions { get; set; }

        public DbSet<BannedList> BannedLists { get; set; }

        public DbSet<BannedReason> BannedReasons { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<DigitalDeliverySpecs> DigitalDeliverySpecs { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventCategory> EventCategories { get; set; }

        public DbSet<ExternalLink> ExternalLinks { get; set; }

        public DbSet<LinkCategory> LinkCategories { get; set; }

        public DbSet<Forms_Field> Forms_Fields { get; set; }

        public DbSet<Forms_Metadata> Forms_Metadata { get; set; }

        public DbSet<Linker_TicketToEvent> Linker_TicketToEvents { get; set; }

        public DbSet<LinkerArtistToCategory> Linker_ArtistToCategories { get; set; }

        public DbSet<Linker_TransactionLineItem> Linker_TransactionLineItems { get; set; }

        public DbSet<Linker_UserFavorite> Linker_UserFavorites { get; set; }

        public DbSet<Linker_UserAndArtistToContact> Linker_UserAndArtistToContacts { get; set; }

        public DbSet<Linker_VendorToUser> Linker_VendorToUsers { get; set; }

        public DbSet<Listing> Listings { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<PhoneContact> PhoneContacts { get; set; }

        public DbSet<PhoneContactLabel> PhoneContactLabels { get; set; }

        public DbSet<ContactLabel> ContactLabels { get; set; }

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

        public DbSet<ContentWarningGroup> ContentWarningGroups { get; set; }

        public DbSet<ContentWarningItem> ContentWarningItems { get; set; }

        public DbSet<UserContentPreference> UserContentPreferences { get; set; }

        public DbSet<Contest> Contests { get; set; }

        public DbSet<ContestEntry> ContestEntries { get; set; }

        public DbSet<MasterImpression> MasterImpressions { get; set; }

        public DbSet<ListingImpression> ListingImpressions { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Linker_EntityToContact> Linker_EntityToContacts { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Forms_MetadataConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Forms_FieldConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ArtCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ArtistCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.EventCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Contact.ContactLabelConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Contact.PhoneContactLabelConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Contact.PhoneContactConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Contact.ExternalLinkConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Contact.LinkCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Contact.AddressConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.PictureConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.BlogConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.LinkerUserToArtistConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.LinkerArtistToCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.LogConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ListingConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.EventConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.StaffConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.StaffRoleConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.TicketConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.TicketTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.VenueConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ArtistPermissionsConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ContestConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ContestEntryConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.MasterImpressionConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.ListingImpressionConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.UserContentPreferenceConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.Contact.ContactConfiguration());
            modelBuilder.ApplyConfiguration(new TAGWEBAPI.Models.Configurations.LinkerEntityToContactConfiguration());
        }
    }
}

