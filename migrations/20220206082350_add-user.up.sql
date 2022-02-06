CREATE TABLE IF NOT EXISTS `user` (
    id BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    email varchar(255) NOT NULL COMMENT 'Email',
    password_hash varchar(255) NOT NULL COMMENT 'Hashed password',
    is_deleted TINYINT(1) NOT NULL DEFAULT 0 COMMENT 'Whether the item is deleted or not. 0 = active, 1 = deleted',
    created_on DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'The DateTime when the item was created',
    updated_on DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'The DateTime when the item was created',
    KEY `idx_email` (`email`)
) ENGINE=INNODB COMMENT 'Contains user information.';
