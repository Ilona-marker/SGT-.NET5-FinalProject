CREATE TABLE `Final project`.`Locations` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(255) NOT NULL,
  `street` VARCHAR(255) NOT NULL,
  `intro` VARCHAR(4000) NOT NULL,
  PRIMARY KEY (`id`)
);

CREATE TABLE IF NOT EXISTS `Final project`.`Connections` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `starting_node` INT UNSIGNED NOT NULL,
  `ending_node` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Connections_Locations_idx` (`starting_node` ASC, `ending_node` ASC),
  CONSTRAINT `fk_Connections_Locations` FOREIGN KEY (`starting_node`) REFERENCES `Final project`.`Locations` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (`ending_node`) REFERENCES `Final project`.`Locations` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS `Final project`.`Progress` (
  `id` INT(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `user_name` VARCHAR(50) NULL,
  `created_date` DATETIME(6) NULL,
  `status` VARCHAR(15) NULL,
  `progress` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_Locations_Progress_idx` (`progress` ASC),
  CONSTRAINT `fk_Locations_Progress` FOREIGN KEY (`progress`) REFERENCES `Final project`.`Locations` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
);