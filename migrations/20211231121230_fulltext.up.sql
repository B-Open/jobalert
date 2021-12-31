ALTER TABLE `job` ADD FULLTEXT INDEX `idx_fulltext` (`title`, `description`);
ALTER TABLE `company` ADD FULLTEXT INDEX `idx_fulltext` (`name`);
