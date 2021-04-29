CREATE TABLE IF NOT EXISTS `final project`.`Progress` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `user_name` VARCHAR(50) NOT NULL,
  `status` VARCHAR(15) NOT NULL CHECK (status IN('started', 'in progress', 'finished')),
  `progress` VARCHAR(20) NULL,
  PRIMARY KEY (`id`)
);