// <copyright file="PictureController.cs" company="Twisted Artists Guild">
// Copyright © Twisted Artists Guild. All rights reserved
// </copyright>

namespace TAGWEBAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TAGWEBAPI.Data;
    using TAGWEBAPI.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly TAGDBContext context;

        public PictureController(TAGDBContext context)
        {
            this.context = context;
        }

        // GET: api/Picture
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Picture>>> GetPictures()
        {
            return await this.context.Set<Picture>().ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Picture/credits?pictureIds=1,2,3
        [HttpGet("credits")]
        public async Task<ActionResult<IEnumerable<PictureCreditsDto>>> GetPictureCredits([FromQuery] string pictureIds)
        {
            var parsedIds = string.IsNullOrWhiteSpace(pictureIds)
                ? new List<int>()
                : pictureIds
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(id => int.TryParse(id, out var parsed) ? parsed : (int?)null)
                    .Where(id => id.HasValue && id.Value > 0)
                    .Select(id => id!.Value)
                    .Distinct()
                    .ToList();

            if (parsedIds.Count == 0)
            {
                return Ok(Array.Empty<PictureCreditsDto>());
            }

            var rows = await (
                from credit in this.context.MediaCredits
                join role in this.context.CreditRoles on credit.CreditRoleID equals role.CreditRoleID
                join party in this.context.CreditParties on credit.CreditPartyID equals party.CreditPartyID
                where credit.PictureID.HasValue && parsedIds.Contains(credit.PictureID.Value)
                select new
                {
                    PictureID = credit.PictureID!.Value,
                    SortOrder = credit.SortOrder,
                    RoleLabel = role.Label,
                    RoleKey = role.KeyName,
                    DisplayName = party.DisplayName,
                    ExternalURL = party.ExternalURL,
                    BioNote = party.BioNote,
                    CreditText = credit.CreditText,
                })
                .OrderBy(row => row.PictureID)
                .ThenBy(row => row.SortOrder)
                .ToListAsync()
                .ConfigureAwait(false);

            var payload = rows
                .GroupBy(row => row.PictureID)
                .Select(group => new PictureCreditsDto
                {
                    PictureID = group.Key,
                    Credits = group.Select(row => new PictureCreditEntryDto
                    {
                        Role = row.RoleLabel,
                        Name = row.DisplayName,
                        Url = row.ExternalURL,
                        Note = string.IsNullOrWhiteSpace(row.CreditText) ? row.BioNote : row.CreditText,
                        RoleKey = row.RoleKey,
                    }).ToList(),
                })
                .ToList();

            return Ok(payload);
        }

        // GET: api/Picture/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Picture>> GetPicture(int id)
        {
            var picture = await this.context.Set<Picture>().FindAsync(id).ConfigureAwait(false);

            if (picture == null)
            {
                return this.NotFound();
            }

            return picture;
        }

        [HttpGet("{id}/credits")]
        public async Task<ActionResult<IEnumerable<MediaCreditDto>>> GetPictureCreditRows(int id)
        {
            if (!this.PictureExists(id))
            {
                return this.NotFound();
            }

            var credits = await this.QueryCreditsByPictureId(id).ConfigureAwait(false);
            return this.Ok(credits);
        }

        [HttpPut("{id}/credits")]
        public async Task<ActionResult<IEnumerable<MediaCreditDto>>> PutPictureCredits(int id, [FromBody] MediaCreditsUpsertRequest request)
        {
            if (!this.PictureExists(id))
            {
                return this.NotFound();
            }

            var submittedCredits = request?.Credits ?? new List<MediaCreditInputDto>();
            var validationError = this.ValidateSubmittedCredits(submittedCredits);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                return this.BadRequest(validationError);
            }

            var existingCredits = await this.context.MediaCredits
                .Where(credit => credit.PictureID == id)
                .ToListAsync()
                .ConfigureAwait(false);

            this.context.MediaCredits.RemoveRange(existingCredits);

            foreach (var submitted in submittedCredits.OrderBy(credit => credit.SortOrder))
            {
                var creditPartyId = await this.ResolveCreditPartyId(submitted).ConfigureAwait(false);
                if (!creditPartyId.HasValue)
                {
                    return this.BadRequest("Unable to resolve CreditPartyID for one or more credits.");
                }

                this.context.MediaCredits.Add(new MediaCredit
                {
                    PictureID = id,
                    CreditRoleID = submitted.CreditRoleID,
                    CreditPartyID = creditPartyId.Value,
                    CreditText = submitted.CreditText,
                    SortOrder = submitted.SortOrder,
                });
            }

            await this.context.SaveChangesAsync().ConfigureAwait(false);
            return this.Ok(await this.QueryCreditsByPictureId(id).ConfigureAwait(false));
        }

        [HttpGet("video/{id}/credits")]
        public async Task<ActionResult<IEnumerable<MediaCreditDto>>> GetVideoCreditRows(int id)
        {
            if (!this.context.Set<Video>().Any(video => video.VideoID == id))
            {
                return this.NotFound();
            }

            var credits = await this.QueryCreditsByVideoId(id).ConfigureAwait(false);
            return this.Ok(credits);
        }

        [HttpPut("video/{id}/credits")]
        public async Task<ActionResult<IEnumerable<MediaCreditDto>>> PutVideoCredits(int id, [FromBody] MediaCreditsUpsertRequest request)
        {
            if (!this.context.Set<Video>().Any(video => video.VideoID == id))
            {
                return this.NotFound();
            }

            var submittedCredits = request?.Credits ?? new List<MediaCreditInputDto>();
            var validationError = this.ValidateSubmittedCredits(submittedCredits);
            if (!string.IsNullOrWhiteSpace(validationError))
            {
                return this.BadRequest(validationError);
            }

            var existingCredits = await this.context.MediaCredits
                .Where(credit => credit.VideoID == id)
                .ToListAsync()
                .ConfigureAwait(false);

            this.context.MediaCredits.RemoveRange(existingCredits);

            foreach (var submitted in submittedCredits.OrderBy(credit => credit.SortOrder))
            {
                var creditPartyId = await this.ResolveCreditPartyId(submitted).ConfigureAwait(false);
                if (!creditPartyId.HasValue)
                {
                    return this.BadRequest("Unable to resolve CreditPartyID for one or more credits.");
                }

                this.context.MediaCredits.Add(new MediaCredit
                {
                    VideoID = id,
                    CreditRoleID = submitted.CreditRoleID,
                    CreditPartyID = creditPartyId.Value,
                    CreditText = submitted.CreditText,
                    SortOrder = submitted.SortOrder,
                });
            }

            await this.context.SaveChangesAsync().ConfigureAwait(false);
            return this.Ok(await this.QueryCreditsByVideoId(id).ConfigureAwait(false));
        }

        // POST: api/Picture
        [HttpPost]
        public async Task<ActionResult<Picture>> PostPicture(Picture picture)
        {
            this.context.Set<Picture>().Add(picture);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.CreatedAtAction("GetPicture", new { id = picture.PictureID }, picture);
        }

        // PUT: api/Picture/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPicture(int id, Picture picture)
        {
            if (id != picture.PictureID)
            {
                return this.BadRequest();
            }

            this.context.Entry(picture).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.PictureExists(id))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.NoContent();
        }

        // DELETE: api/Picture/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture(int id)
        {
            var picture = await this.context.Set<Picture>().FindAsync(id).ConfigureAwait(false);
            if (picture == null)
            {
                return this.NotFound();
            }

            this.context.Set<Picture>().Remove(picture);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            return this.NoContent();
        }

        private bool PictureExists(int id)
        {
            return this.context.Set<Picture>().Any(e => e.PictureID == id);
        }

        private async Task<List<MediaCreditDto>> QueryCreditsByPictureId(int pictureId)
        {
            return await (
                from credit in this.context.MediaCredits
                join role in this.context.CreditRoles on credit.CreditRoleID equals role.CreditRoleID
                join party in this.context.CreditParties on credit.CreditPartyID equals party.CreditPartyID
                where credit.PictureID == pictureId
                orderby credit.SortOrder, role.DisplayOrder, role.Label
                select new MediaCreditDto
                {
                    MediaCreditID = credit.MediaCreditID,
                    CreditRoleID = role.CreditRoleID,
                    Role = role.Label,
                    CreditPartyID = party.CreditPartyID,
                    DisplayName = party.DisplayName,
                    UserID = party.UserID,
                    ArtistID = party.ArtistID,
                    ExternalURL = party.ExternalURL,
                    BioNote = party.BioNote,
                    CreditText = credit.CreditText,
                    SortOrder = credit.SortOrder,
                })
                .ToListAsync()
                .ConfigureAwait(false);
        }

        private async Task<List<MediaCreditDto>> QueryCreditsByVideoId(int videoId)
        {
            return await (
                from credit in this.context.MediaCredits
                join role in this.context.CreditRoles on credit.CreditRoleID equals role.CreditRoleID
                join party in this.context.CreditParties on credit.CreditPartyID equals party.CreditPartyID
                where credit.VideoID == videoId
                orderby credit.SortOrder, role.DisplayOrder, role.Label
                select new MediaCreditDto
                {
                    MediaCreditID = credit.MediaCreditID,
                    CreditRoleID = role.CreditRoleID,
                    Role = role.Label,
                    CreditPartyID = party.CreditPartyID,
                    DisplayName = party.DisplayName,
                    UserID = party.UserID,
                    ArtistID = party.ArtistID,
                    ExternalURL = party.ExternalURL,
                    BioNote = party.BioNote,
                    CreditText = credit.CreditText,
                    SortOrder = credit.SortOrder,
                })
                .ToListAsync()
                .ConfigureAwait(false);
        }

        private string ValidateSubmittedCredits(List<MediaCreditInputDto> submittedCredits)
        {
            foreach (var submitted in submittedCredits)
            {
                if (submitted.CreditRoleID <= 0)
                {
                    return "Each credit must include a valid CreditRoleID.";
                }

                if (submitted.CreditPartyID.HasValue)
                {
                    continue;
                }

                if (submitted.Party == null)
                {
                    return "Each credit must include either CreditPartyID or Party details.";
                }

                var hasLinkedIdentity = submitted.Party.UserID.HasValue || submitted.Party.ArtistID.HasValue;
                var hasDisplayName = !string.IsNullOrWhiteSpace(submitted.Party.DisplayName);
                if (!hasLinkedIdentity && !hasDisplayName)
                {
                    return "Each Party requires a linked identity or a display name.";
                }
            }

            return string.Empty;
        }

        private async Task<int?> ResolveCreditPartyId(MediaCreditInputDto submitted)
        {
            if (submitted.CreditPartyID.HasValue)
            {
                return submitted.CreditPartyID;
            }

            if (submitted.Party == null)
            {
                return null;
            }

            var party = new CreditParty
            {
                UserID = submitted.Party.UserID,
                ArtistID = submitted.Party.ArtistID,
                DisplayName = submitted.Party.DisplayName,
                ExternalURL = submitted.Party.ExternalURL,
                BioNote = submitted.Party.BioNote,
                Created = DateTime.UtcNow,
            };

            this.context.CreditParties.Add(party);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            return party.CreditPartyID;
        }

        public class PictureCreditsDto
        {
            public int PictureID { get; set; }

            public List<PictureCreditEntryDto> Credits { get; set; } = new();
        }

        public class PictureCreditEntryDto
        {
            public string? Role { get; set; }

            public string? Name { get; set; }

            public string? Url { get; set; }

            public string? Note { get; set; }

            public string? RoleKey { get; set; }
        }

        public class MediaCreditsUpsertRequest
        {
            public List<MediaCreditInputDto> Credits { get; set; } = new();
        }

        public class MediaCreditInputDto
        {
            public int? CreditPartyID { get; set; }

            public int CreditRoleID { get; set; }

            public int SortOrder { get; set; }

            public string? CreditText { get; set; }

            public MediaCreditPartyInputDto? Party { get; set; }
        }

        public class MediaCreditPartyInputDto
        {
            public int? UserID { get; set; }

            public int? ArtistID { get; set; }

            public string? DisplayName { get; set; }

            public string? ExternalURL { get; set; }

            public string? BioNote { get; set; }
        }

        public class MediaCreditDto
        {
            public int MediaCreditID { get; set; }

            public int CreditRoleID { get; set; }

            public string Role { get; set; } = string.Empty;

            public int CreditPartyID { get; set; }

            public string? DisplayName { get; set; }

            public int? UserID { get; set; }

            public int? ArtistID { get; set; }

            public string? ExternalURL { get; set; }

            public string? BioNote { get; set; }

            public string? CreditText { get; set; }

            public int SortOrder { get; set; }
        }
    }
}
