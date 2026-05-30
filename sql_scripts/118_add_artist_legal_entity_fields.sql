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

