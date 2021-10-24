CREATE TABLE IF NOT EXISTS job (
    id BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    provider_id BIGINT NOT NULL COMMENT 'The provider from which this job listing came from.',
    provider_job_id VARCHAR(255) NOT NULL COMMENT 'The primary key of the job listing from the provider.',
    company_id BIGINT NOT NULL COMMENT 'The company for the job listing.',
    title VARCHAR(1000) NOT NULL COMMENT 'The job listing name or title.',
    salary VARCHAR(1000) NOT NULL COMMENT 'The string for salary from the provider.',
    salary_min DECIMAL(8, 2) NOT NULL COMMENT 'The minimum salary in the salary range.',
    salary_max DECIMAL(8, 2) NOT NULL COMMENT 'The maximum salary in the salary range.',
    location TEXT NOT NULL COMMENT 'The job location details stored as JSON.',
    description TEXT NOT NULL COMMENT 'The job description.',
    KEY `idx_provider_job_id` (`provider_job_id`)
) ENGINE=INNODB COMMENT 'Containing all job listings.';

CREATE TABLE IF NOT EXISTS company (
    id BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    provider_company_id VARCHAR(1000) NOT NULL COMMENT 'The primary key of the company listing from the provider.',
    name varchar(1000) NOT NULL COMMENT 'Company name'
) ENGINE=INNODB COMMENT 'Contains company information.';

CREATE TABLE IF NOT EXISTS provider (
    id BIGINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    name varchar(1000) NOT NULL COMMENT 'Provider name'
) ENGINE=INNODB COMMENT 'The list of different providers (sources) for job listings.';

INSERT INTO provider
VALUES
    (1, 'jobcentre');

