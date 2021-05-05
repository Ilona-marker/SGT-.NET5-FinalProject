CREATE DATABASE IF NOT EXISTS `Final project`;

USE `Final project`;

CREATE USER 'user'@'localhost' IDENTIFIED BY 'password';

GRANT SELECT, INSERT, UPDATE on `Final project`.* TO 'user'@'localhost';