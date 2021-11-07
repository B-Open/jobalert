ALTER TABLE job
    ADD is_deleted TINYINT(1) NOT NULL DEFAULT 0 COMMENT 'Whether the item is deleted or not. 0 = active, 1 = deleted',
    ADD created_on DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'The DateTime when the item was created',
    ADD updated_on DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'The DateTime when the item was created';

ALTER TABLE company
    ADD provider_id BIGINT NOT NULL COMMENT 'The provider from which this company listing came from.',
    ADD is_deleted TINYINT(1) NOT NULL DEFAULT 0 COMMENT 'Whether the item is deleted or not. 0 = active, 1 = deleted',
    ADD created_on DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'The DateTime when the item was created',
    ADD updated_on DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'The DateTime when the item was created';

ALTER TABLE provider
    ADD is_deleted TINYINT(1) NOT NULL DEFAULT 0 COMMENT 'Whether the item is deleted or not. 0 = active, 1 = deleted',
    ADD created_on DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'The DateTime when the item was created',
    ADD updated_on DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'The DateTime when the item was created';
