ALTER TABLE job
    DROP COLUMN is_deleted,
    DROP COLUMN created_on,
    DROP COLUMN updated_on;

ALTER TABLE company
    DROP COLUMN provider_id,
    DROP COLUMN is_deleted,
    DROP COLUMN created_on,
    DROP COLUMN updated_on;

ALTER TABLE provider
    DROP COLUMN is_deleted,
    DROP COLUMN created_on,
    DROP COLUMN updated_on;
