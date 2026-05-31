-- Run this script against the TAG database after backing up.
-- Purpose: enforce uniqueness for Picture.NormalizedURL and support URL-based picture lookups.

BEGIN;

UPDATE "Pictures"
SET "NormalizedURL" = lower(trim("URL"))
WHERE ("NormalizedURL" IS NULL OR trim("NormalizedURL") = '')
  AND "URL" IS NOT NULL;

CREATE UNIQUE INDEX IF NOT EXISTS "IX_Pictures_NormalizedURL"
ON "Pictures" ("NormalizedURL")
WHERE "NormalizedURL" IS NOT NULL;

COMMIT;



-- Run this script against the TAG database after backing up.
-- Purpose: add legal-entity metadata columns to Artist for step-1 registration.

BEGIN;

ALTER TABLE "Artists"
    ADD COLUMN IF NOT EXISTS "Country" character varying(120),
    ADD COLUMN IF NOT EXISTS "StateOrProvince" character varying(120),
    ADD COLUMN IF NOT EXISTS "BusinessEntityType" character varying(80),
    ADD COLUMN IF NOT EXISTS "IsFormallyIncorporated" boolean;

COMMIT;

-- Run this script against the TAG database after backing up.
-- Purpose: expand DB-driven DynaForm coverage for ArtistForm1 (Forms_MetadataID=3)
--          and UserForm1 (Forms_MetadataID=9) without hardcoding fields in the web app.

BEGIN;

-- Keep metadata copy aligned with the expanded scope text.
UPDATE "Forms_Metadata"
SET
    "FormBody" = 'Fill out the information below to create or update your artist profile. Title is required; additional profile and linkage fields are optional and DB-driven.'
WHERE "Forms_MetadataID" = 3;

UPDATE "Forms_Metadata"
SET
    "FormBody" = 'Enter your user information. Username and email are required; optional profile/privacy fields are DB-driven from Forms_Fields.'
WHERE "Forms_MetadataID" = 9;

-- ArtistForm1 expansion (near-complete editable coverage of Artist table profile fields).
INSERT INTO "Forms_Fields"
("Forms_FieldID", "Forms_MetadataID", "Name", "Type", "Label", "Placeholder", "DefaultValue", "FieldOrder", "Hidden", "IsRequired", "IsReadOnly", "ClassName", "Width", "Height", "RegexValidationPattern")
VALUES
    (108, 3, 'biography', 'textarea', 'Biography', 'Artist biography / background', '', 8, FALSE, FALSE, FALSE, 'textarea textarea-bordered w-full', '100%', '6', NULL),
    (109, 3, 'galleryid', 'number', 'Gallery ID', 'Linked gallery ID (optional)', '', 9, FALSE, FALSE, FALSE, 'input input-bordered w-full', '100%', NULL, 'validate_number'),
    (110, 3, 'primarycontactid', 'number', 'Primary Contact ID', 'Linked contact ID (optional)', '', 10, FALSE, FALSE, FALSE, 'input input-bordered w-full', '100%', NULL, 'validate_number'),
    (111, 3, 'profilepicid', 'number', 'Profile Picture ID', 'Picture ID for profile image', '', 11, FALSE, FALSE, FALSE, 'input input-bordered w-full', '100%', NULL, 'validate_number'),
    (112, 3, 'coverpicid', 'number', 'Cover Picture ID', 'Picture ID for cover image', '', 12, FALSE, FALSE, FALSE, 'input input-bordered w-full', '100%', NULL, 'validate_number')
ON CONFLICT ("Forms_FieldID") DO UPDATE
SET
    "Forms_MetadataID" = EXCLUDED."Forms_MetadataID",
    "Name" = EXCLUDED."Name",
    "Type" = EXCLUDED."Type",
    "Label" = EXCLUDED."Label",
    "Placeholder" = EXCLUDED."Placeholder",
    "DefaultValue" = EXCLUDED."DefaultValue",
    "FieldOrder" = EXCLUDED."FieldOrder",
    "Hidden" = EXCLUDED."Hidden",
    "IsRequired" = EXCLUDED."IsRequired",
    "IsReadOnly" = EXCLUDED."IsReadOnly",
    "ClassName" = EXCLUDED."ClassName",
    "Width" = EXCLUDED."Width",
    "Height" = EXCLUDED."Height",
    "RegexValidationPattern" = EXCLUDED."RegexValidationPattern";

-- UserForm1 expansion (near-complete editable profile/privacy coverage of User table).
INSERT INTO "Forms_Fields"
("Forms_FieldID", "Forms_MetadataID", "Name", "Type", "Label", "Placeholder", "DefaultValue", "FieldOrder", "Hidden", "IsRequired", "IsReadOnly", "ClassName", "Width", "Height", "RegexValidationPattern")
VALUES
    (412, 9, 'birthdate', 'date', 'Birth Date', 'YYYY-MM-DD', '', 12, FALSE, FALSE, FALSE, 'input input-bordered w-full', '100%', NULL, NULL),
    (413, 9, 'deathdate', 'date', 'Death Date', 'YYYY-MM-DD', '', 13, FALSE, FALSE, FALSE, 'input input-bordered w-full', '100%', NULL, NULL),
    (414, 9, 'hidefrompublic', 'checkbox', 'Hide Profile From Public', '', 'false', 14, FALSE, FALSE, FALSE, 'checkbox', '100%', NULL, NULL),
    (415, 9, 'joined', 'datetime', 'Joined', '', '', 15, TRUE, FALSE, TRUE, 'input input-bordered w-full', '100%', NULL, NULL)
ON CONFLICT ("Forms_FieldID") DO UPDATE
SET
    "Forms_MetadataID" = EXCLUDED."Forms_MetadataID",
    "Name" = EXCLUDED."Name",
    "Type" = EXCLUDED."Type",
    "Label" = EXCLUDED."Label",
    "Placeholder" = EXCLUDED."Placeholder",
    "DefaultValue" = EXCLUDED."DefaultValue",
    "FieldOrder" = EXCLUDED."FieldOrder",
    "Hidden" = EXCLUDED."Hidden",
    "IsRequired" = EXCLUDED."IsRequired",
    "IsReadOnly" = EXCLUDED."IsReadOnly",
    "ClassName" = EXCLUDED."ClassName",
    "Width" = EXCLUDED."Width",
    "Height" = EXCLUDED."Height",
    "RegexValidationPattern" = EXCLUDED."RegexValidationPattern";

COMMIT;


-- Run this script against the TAG database after backing up.
-- Purpose: add IncorporatedYear column to Artists table to record the year a formally incorporated entity was established.

BEGIN;

ALTER TABLE "Artists"
    ADD COLUMN IF NOT EXISTS "IncorporatedYear" integer;

COMMENT ON COLUMN "Artists"."IncorporatedYear" IS 'Four-digit year the artist entity was formally incorporated. Null for unincorporated or unknown.';

COMMIT;
