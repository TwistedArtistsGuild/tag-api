// <copyright file="ContactController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Controllers.Contact
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TAGWEBAPI.Data;
    using TAGWEBAPI.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        // Social platforms are not blocked — they are handled via Social Handles instead.
        // URLs pointing to these domains are redirected to that flow rather than saved here.
        private static readonly HashSet<string> SocialHandleDomains = new(StringComparer.OrdinalIgnoreCase)
        {
            "instagram.com",
            "tiktok.com",
            "x.com",
            "twitter.com",
            "facebook.com",
            "fb.com",
            "youtube.com",
            "youtu.be",
            "threads.net",
            "snapchat.com",
            "mastodon.social",
            "linkedin.com",
            "discord.com",
            "telegram.org",
            "whatsapp.com",
            "reddit.com",
        };

        // Categories used by the Social Handles UI flow.
        private static readonly HashSet<string> SocialHandleCategories = new(StringComparer.OrdinalIgnoreCase)
        {
            "instagram",
            "tiktok",
            "x",
            "facebook",
            "youtube",
            "threads",
            "twitter",
            "snapchat",
            "mastodon",
            "linkedin",
            "discord",
            "telegram",
            "whatsapp",
            "reddit",
        };

        // Adult-content domains are fully blocked and cannot be added anywhere on the platform.
        private static readonly HashSet<string> BlockedAdultDomains = new(StringComparer.OrdinalIgnoreCase)
        {
            "onlyfans.com",
            "fansly.com",
            "pornhub.com",
            "xvideos.com",
            "xnxx.com",
            "redtube.com",
            "youporn.com",
            "chaturbate.com",
            "cam4.com",
            "stripchat.com",
            "manyvids.com",
        };

        private static readonly HashSet<string> ValidContexts = new(StringComparer.OrdinalIgnoreCase)
        {
            "artist", "user", "venue", "vendor",
        };

        private readonly TAGDBContext context;

        public ContactController(TAGDBContext context)
        {
            this.context = context;
        }

        // GET: api/contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await this.PublicContactsQuery()
                .Include(c => c.Address)
                .Include(c => c.PhoneContact)
                .OrderBy(c => c.ContactID)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        // GET: api/contact/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await this.PublicContactsQuery()
                .Include(c => c.Address)
                .Include(c => c.PhoneContact)
                .FirstOrDefaultAsync(c => c.ContactID == id)
                .ConfigureAwait(false);

            if (contact == null)
            {
                return this.NotFound();
            }

            return contact;
        }

        // GET: api/contact/artist/4
        // Returns the entity's primary phone + address merged with all public linked contacts.
        [HttpGet("{context}/{entityId:int}")]
        public async Task<ActionResult<object>> GetContactsByContext(string context, int entityId)
        {
            if (!ValidContexts.Contains(context))
            {
                return this.BadRequest($"Invalid context. Valid values: {string.Join(", ", ValidContexts)}.");
            }

            var normalizedContext = context.Trim().ToLowerInvariant();

            // Load linked public contacts for this entity
            var linksQuery = this.context.Set<Linker_EntityToContact>()
                .AsNoTracking()
                .Where(l => l.MakePublic);

            linksQuery = normalizedContext switch
            {
                "artist" => linksQuery.Where(l => l.ArtistID == entityId),
                "user" => linksQuery.Where(l => l.UserID == entityId),
                "venue" => linksQuery.Where(l => l.VenueID == entityId),
                "vendor" => linksQuery.Where(l => l.VendorID == entityId),
                _ => linksQuery.Where(l => false),
            };

            var linkedContacts = await linksQuery
                .Include(l => l.Contact)
                    .ThenInclude(c => c.Address)
                .Include(l => l.Contact)
                    .ThenInclude(c => c.PhoneContact)
                        .ThenInclude(pc => pc != null ? pc.PhoneContactLabel : null)
                .Include(l => l.Contact)
                    .ThenInclude(c => c.ContactLabel)
                .Include(l => l.Contact)
                    .ThenInclude(c => c.PhoneContact)
                        .ThenInclude(pc => pc != null ? pc.ContactLabel : null)
                .OrderBy(l => l.DisplayOrder)
                .ToListAsync()
                .ConfigureAwait(false);

            // Filter out linker rows where the underlying data is private
            var publicLinks = linkedContacts.Where(l =>
                l.Contact != null &&
                !l.Contact.IsPrivate &&
                (l.Contact.Address == null || !l.Contact.Address.IsPrivate) &&
                (l.Contact.PhoneContact == null || !l.Contact.PhoneContact.IsPrivate))
                .ToList();

            // Load the entity's primary contact
            Contact? primaryContact = null;
            object? primaryPhone = null;
            object? primaryAddress = null;

            if (normalizedContext == "artist")
            {
                var artist = await this.context.Set<Artist>()
                    .AsNoTracking()
                    .Include(a => a.PrimaryContact)
                        .ThenInclude(c => c.Address)
                    .Include(a => a.PrimaryContact)
                        .ThenInclude(c => c.PhoneContact)
                            .ThenInclude(pc => pc != null ? pc.PhoneContactLabel : null)
                    .FirstOrDefaultAsync(a => a.ArtistID == entityId)
                    .ConfigureAwait(false);

                if (artist == null)
                {
                    return this.NotFound();
                }

                primaryContact = artist.PrimaryContact;
            }
            else if (normalizedContext == "user")
            {
                var user = await this.context.Set<User>()
                    .AsNoTracking()
                    .Include(u => u.PrimaryContact)
                        .ThenInclude(c => c.Address)
                    .Include(u => u.PrimaryContact)
                        .ThenInclude(c => c.PhoneContact)
                            .ThenInclude(pc => pc != null ? pc.PhoneContactLabel : null)
                    .FirstOrDefaultAsync(u => u.UserID == entityId)
                    .ConfigureAwait(false);

                if (user == null)
                {
                    return this.NotFound();
                }

                primaryContact = user.PrimaryContact;
            }
            else if (normalizedContext == "venue")
            {
                var venue = await this.context.Set<Venue>()
                    .AsNoTracking()
                    .Include(v => v.PrimaryContact)
                        .ThenInclude(c => c.Address)
                    .Include(v => v.PrimaryContact)
                        .ThenInclude(c => c.PhoneContact)
                            .ThenInclude(pc => pc != null ? pc.PhoneContactLabel : null)
                    .FirstOrDefaultAsync(v => v.VenueID == entityId)
                    .ConfigureAwait(false);

                if (venue == null)
                {
                    return this.NotFound();
                }

                primaryContact = venue.PrimaryContact;
            }
            else if (normalizedContext == "vendor")
            {
                var vendor = await this.context.Set<Vendor>()
                    .AsNoTracking()
                    .Include(v => v.PrimaryContact)
                        .ThenInclude(c => c.Address)
                    .Include(v => v.PrimaryContact)
                        .ThenInclude(c => c.PhoneContact)
                            .ThenInclude(pc => pc != null ? pc.PhoneContactLabel : null)
                    .FirstOrDefaultAsync(v => v.VendorID == entityId)
                    .ConfigureAwait(false);

                if (vendor == null)
                {
                    return this.NotFound();
                }

                primaryContact = vendor.PrimaryContact;
            }

            if (primaryContact != null &&
                !primaryContact.IsPrivate &&
                (primaryContact.Address == null || !primaryContact.Address.IsPrivate) &&
                (primaryContact.PhoneContact == null || !primaryContact.PhoneContact.IsPrivate))
            {
                if (string.Equals(primaryContact.ContactType, "address", StringComparison.OrdinalIgnoreCase))
                {
                    primaryAddress = primaryContact.Address;
                }

                if (string.Equals(primaryContact.ContactType, "phone", StringComparison.OrdinalIgnoreCase))
                {
                    primaryPhone = primaryContact.PhoneContact;
                }
            }

            return this.Ok(new
            {
                context = normalizedContext,
                entityId,
                primaryContactId = primaryContact?.ContactID,
                primaryPhone,
                primaryAddress,
                contacts = publicLinks.Select(l => new
                {
                    linkId = l.Linker_EntityToContactID,
                    contactId = l.ContactID,
                    displayOrder = l.DisplayOrder,
                    contactType = l.Contact.ContactType,
                    label = l.Contact.ContactLabel != null ? l.Contact.ContactLabel.Label : l.Contact.Label,
                    category = l.Contact.Category,
                    value = l.Contact.ContactType == "phone"
                        ? l.Contact.PhoneContact?.PhoneNumber
                        : l.Contact.Value,
                    phoneLabel = l.Contact.PhoneContact?.ContactLabel?.Label ?? l.Contact.PhoneContact?.PhoneContactLabel?.Label,
                    address = l.Contact.Address == null ? null : new
                    {
                        line1 = l.Contact.Address.AddressLine1,
                        line2 = l.Contact.Address.AddressLine2,
                        city = l.Contact.Address.City,
                        state = l.Contact.Address.State,
                        region = l.Contact.Address.Region,
                        postalCode = l.Contact.Address.ZipCode,
                        country = l.Contact.Address.Country,
                        hours = l.Contact.Address.OperationHours,
                    },
                    handle = l.Contact.Handle,
                    description = l.Contact.Description,
                }),
            });
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            var validationError = ValidateContact(contact);
            if (validationError != null)
            {
                return this.BadRequest(validationError);
            }

            this.context.Set<Contact>().Add(contact);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.CreatedAtAction(nameof(this.GetContact), new { id = contact.ContactID }, contact);
        }

        // POST: api/contact/manage
        // Creates a Contact (+ optional Address/PhoneContact) and links it to an owner entity.
        [HttpPost("manage")]
        public async Task<ActionResult<object>> PostManagedContact(ManageContactRequest request)
        {
            if (request == null)
            {
                return this.BadRequest("Request body is required.");
            }

            var normalizedContext = request.Context?.Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(normalizedContext) || !ValidContexts.Contains(normalizedContext))
            {
                return this.BadRequest($"Invalid context. Valid values: {string.Join(", ", ValidContexts)}.");
            }

            if (request.EntityID <= 0)
            {
                return this.BadRequest("EntityID must be a positive integer.");
            }

            var normalizedLabel = request.Label?.Trim();
            var resolvedLabel = await this.ResolveContactLabelAsync(normalizedLabel).ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(normalizedLabel) && resolvedLabel == null)
            {
                return this.BadRequest("Label is not recognized. Select one of the shared contact labels.");
            }

            var resolvedLabelId = resolvedLabel?.ContactLabelID;
            var resolvedLabelText = resolvedLabel?.Label ?? normalizedLabel;

            if (!this.EntityExists(normalizedContext, request.EntityID))
            {
                return this.NotFound($"{normalizedContext} entity {request.EntityID} was not found.");
            }

            var normalizedType = request.ContactType?.Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(normalizedType))
            {
                return this.BadRequest("ContactType is required.");
            }

            if (normalizedType is not ("address" or "phone" or "email" or "url"))
            {
                return this.BadRequest("ContactType must be one of: address, phone, email, url.");
            }

            var isPrivate = request.IsPrivate ?? (normalizedContext == "user");

            Address? address = null;
            PhoneContact? phoneContact = null;

            if (normalizedType == "address")
            {
                if (string.IsNullOrWhiteSpace(request.AddressLine1))
                {
                    return this.BadRequest("AddressLine1 is required for address contacts.");
                }

                if (string.IsNullOrWhiteSpace(request.Country))
                {
                    return this.BadRequest("Country is required for address contacts.");
                }

                address = new Address
                {
                    AddressLine1 = request.AddressLine1,
                    AddressLine2 = request.AddressLine2,
                    AddressLine3 = request.AddressLine3,
                    AddressLine4 = request.AddressLine4,
                    City = request.City,
                    State = request.State,
                    ZipCode = request.ZipCode,
                    Country = request.Country,
                    Region = !string.IsNullOrWhiteSpace(request.Region)
                        ? request.Region
                        : request.State ?? "Unknown",
                    OperationHours = request.OperationHours,
                    ContactLabelID = resolvedLabelId,
                    IsPrivate = isPrivate,
                };

                this.context.Addresses.Add(address);
                await this.context.SaveChangesAsync().ConfigureAwait(false);
            }

            if (normalizedType == "phone")
            {
                var phoneValue = !string.IsNullOrWhiteSpace(request.PhoneNumber)
                    ? request.PhoneNumber
                    : request.Value;

                if (string.IsNullOrWhiteSpace(phoneValue))
                {
                    return this.BadRequest("PhoneNumber (or Value) is required for phone contacts.");
                }

                phoneContact = new PhoneContact
                {
                    PhoneContactLabelID = request.PhoneContactLabelID,
                    ContactLabelID = resolvedLabelId,
                    PhoneNumber = phoneValue,
                    Description = request.PhoneDescription,
                    IsPrivate = isPrivate,
                };

                this.context.PhoneContacts.Add(phoneContact);
                await this.context.SaveChangesAsync().ConfigureAwait(false);
            }

            if ((normalizedType == "email" || normalizedType == "url") && string.IsNullOrWhiteSpace(request.Value))
            {
                return this.BadRequest("Value is required for email and url contacts.");
            }

            if (normalizedType == "url")
            {
                if (!TryNormalizeUrl(request.Value, out var normalizedUrl))
                {
                    return this.BadRequest("Value must be a valid URL.");
                }

                var looksLikeSocialHandleSubmission =
                    !string.IsNullOrWhiteSpace(request.Handle) ||
                    (!string.IsNullOrWhiteSpace(request.Category) &&
                     SocialHandleCategories.Contains(request.Category.Trim()));

                if (IsBlockedUrl(normalizedUrl, looksLikeSocialHandleSubmission, out var blockedReason))
                {
                    return this.BadRequest(blockedReason);
                }

                request.Value = normalizedUrl;
            }

            var contact = new Models.Contact
            {
                ContactType = normalizedType,
                Label = resolvedLabelText,
                ContactLabelID = resolvedLabelId,
                Category = request.Category,
                Value = normalizedType == "phone"
                    ? null
                    : request.Value,
                Handle = request.Handle,
                Description = request.Description,
                AddressID = address?.AddressID,
                PhoneContactID = phoneContact?.PhoneContactID,
                IsPrivate = isPrivate,
            };

            this.context.Contacts.Add(contact);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            var link = new Linker_EntityToContact
            {
                ContactID = contact.ContactID,
                MakePublic = !isPrivate,
                DisplayOrder = request.DisplayOrder ?? 0,
            };

            switch (normalizedContext)
            {
                case "artist":
                    link.ArtistID = request.EntityID;
                    break;
                case "user":
                    link.UserID = request.EntityID;
                    break;
                case "venue":
                    link.VenueID = request.EntityID;
                    break;
                case "vendor":
                    link.VendorID = request.EntityID;
                    break;
            }

            this.context.Linker_EntityToContacts.Add(link);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            if (request.SetAsPrimary == true)
            {
                await this.ApplyPrimaryContactAsync(
                    normalizedContext,
                    request.EntityID,
                    contact.ContactID)
                    .ConfigureAwait(false);
            }

            return this.CreatedAtAction(
                nameof(this.GetContact),
                new { id = contact.ContactID },
                new
                {
                    context = normalizedContext,
                    entityId = request.EntityID,
                    contactId = contact.ContactID,
                    linkId = link.Linker_EntityToContactID,
                    primaryUpdated = request.SetAsPrimary == true,
                });
        }

        private async Task ApplyPrimaryContactAsync(string contextName, int entityId, int contactId)
        {
            switch (contextName)
            {
                case "artist":
                {
                    var artist = await this.context.Artists.FirstOrDefaultAsync(a => a.ArtistID == entityId).ConfigureAwait(false);
                    if (artist == null)
                    {
                        return;
                    }

                    artist.PrimaryContactID = contactId;

                    break;
                }

                case "user":
                {
                    var user = await this.context.Users.FirstOrDefaultAsync(u => u.UserID == entityId).ConfigureAwait(false);
                    if (user == null)
                    {
                        return;
                    }

                    user.PrimaryContactID = contactId;

                    break;
                }

                case "vendor":
                {
                    var vendor = await this.context.Vendors.FirstOrDefaultAsync(v => v.VendorID == entityId).ConfigureAwait(false);
                    if (vendor == null)
                    {
                        return;
                    }

                    vendor.PrimaryContactID = contactId;

                    break;
                }

                case "venue":
                {
                    var venue = await this.context.Venues.FirstOrDefaultAsync(v => v.VenueID == entityId).ConfigureAwait(false);
                    if (venue == null)
                    {
                        return;
                    }

                    venue.PrimaryContactID = contactId;

                    break;
                }
            }

            await this.context.SaveChangesAsync().ConfigureAwait(false);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.ContactID)
            {
                return this.BadRequest();
            }

            var validationError = ValidateContact(contact);
            if (validationError != null)
            {
                return this.BadRequest(validationError);
            }

            this.context.Entry(contact).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.ContactExists(id))
                {
                    return this.NotFound();
                }

                throw;
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await this.context.Set<Contact>().FindAsync(id).ConfigureAwait(false);
            if (contact == null)
            {
                return this.NotFound();
            }

            this.context.Set<Contact>().Remove(contact);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }

        private static string? ValidateContact(Contact contact)
        {
            var normalizedType = contact.ContactType?.Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(normalizedType))
            {
                return "ContactType is required.";
            }

            switch (normalizedType)
            {
                case "address":
                    if (!contact.AddressID.HasValue)
                    {
                        return "Address contacts require AddressID.";
                    }

                    if (contact.PhoneContactID.HasValue || !string.IsNullOrWhiteSpace(contact.Value))
                    {
                        return "Address contacts cannot set PhoneContactID or Value.";
                    }

                    break;
                case "phone":
                    if (!contact.PhoneContactID.HasValue)
                    {
                        return "Phone contacts require PhoneContactID.";
                    }

                    if (contact.AddressID.HasValue || !string.IsNullOrWhiteSpace(contact.Value))
                    {
                        return "Phone contacts cannot set AddressID or Value.";
                    }

                    break;
                case "email":
                case "url":
                    if (string.IsNullOrWhiteSpace(contact.Value))
                    {
                        return "Phone, email, and url contacts require Value.";
                    }

                    if (contact.AddressID.HasValue || contact.PhoneContactID.HasValue)
                    {
                        return "Email and url contacts cannot set AddressID or PhoneContactID.";
                    }

                    break;
                default:
                    return "ContactType must be one of: address, phone, email, url.";
            }

            contact.ContactType = normalizedType;
            return null;
        }

        private IQueryable<Contact> PublicContactsQuery()
        {
            return this.context.Set<Contact>()
                .AsNoTracking()
                .Where(c =>
                    !c.IsPrivate &&
                    c.EntityLinks.Any(l => l.MakePublic) &&
                    (c.Address == null || !c.Address.IsPrivate) &&
                    (c.PhoneContact == null || !c.PhoneContact.IsPrivate));
        }

        private bool EntityExists(string contextName, int entityId)
        {
            return contextName switch
            {
                "artist" => this.context.Artists.Any(a => a.ArtistID == entityId),
                "user" => this.context.Users.Any(u => u.UserID == entityId),
                "venue" => this.context.Venues.Any(v => v.VenueID == entityId),
                "vendor" => this.context.Vendors.Any(v => v.VendorID == entityId),
                _ => false,
            };
        }

        private bool ContactExists(int id)
        {
            return this.context.Set<Contact>().Any(e => e.ContactID == id);
        }

        private async Task<ContactLabel?> ResolveContactLabelAsync(string? label)
        {
            if (string.IsNullOrWhiteSpace(label))
            {
                return null;
            }

            var normalized = label.Trim().ToLowerInvariant();
            return await this.context.Set<ContactLabel>()
                .AsNoTracking()
                .FirstOrDefaultAsync(cl => cl.Label.ToLower() == normalized)
                .ConfigureAwait(false);
        }

        private static bool TryNormalizeUrl(string? rawValue, out string normalizedUrl)
        {
            normalizedUrl = string.Empty;
            var value = rawValue?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            if (!value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !value.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                value = $"https://{value}";
            }

            if (!Uri.TryCreate(value, UriKind.Absolute, out var parsed) ||
                (parsed.Scheme != Uri.UriSchemeHttp && parsed.Scheme != Uri.UriSchemeHttps))
            {
                return false;
            }

            normalizedUrl = parsed.ToString();
            return true;
        }

        private static bool IsBlockedUrl(string normalizedUrl, bool allowSocialDomains, out string reason)
        {
            reason = string.Empty;

            if (!Uri.TryCreate(normalizedUrl, UriKind.Absolute, out var parsed))
            {
                reason = "URL is invalid.";
                return true;
            }

            var host = parsed.Host.Trim().ToLowerInvariant();
            if (host.StartsWith("www.", StringComparison.Ordinal))
            {
                host = host[4..];
            }

            if (!allowSocialDomains && SocialHandleDomains.Any(d => host == d || host.EndsWith($".{d}", StringComparison.Ordinal)))
            {
                reason = "That platform belongs in Social Handles. Please add it there instead.";
                return true;
            }

            if (BlockedAdultDomains.Any(d => host == d || host.EndsWith($".{d}", StringComparison.Ordinal)))
            {
                reason = "Adult-content domains are not supported for profile links.";
                return true;
            }

            return false;
        }
    }

    public sealed class ManageContactRequest
    {
        public string? Context { get; set; }

        public int EntityID { get; set; }

        public string? ContactType { get; set; }

        public string? Label { get; set; }

        public string? Category { get; set; }

        public string? Value { get; set; }

        public string? Handle { get; set; }

        public string? Description { get; set; }

        public bool? IsPrivate { get; set; }

        public int? DisplayOrder { get; set; }

        public string? PhoneNumber { get; set; }

        public string? PhoneDescription { get; set; }

        public int? PhoneContactLabelID { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? AddressLine3 { get; set; }

        public string? AddressLine4 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Region { get; set; }

        public string? ZipCode { get; set; }

        public string? Country { get; set; }

        public string? OperationHours { get; set; }

        public bool? SetAsPrimary { get; set; }
    }
}