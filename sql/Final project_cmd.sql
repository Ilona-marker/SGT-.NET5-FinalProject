USE `Final project`;

ALTER TABLE `Progress`
ADD column created_date datetime
after user_name;

ALTER TABLE `Progress`
modify progress  int not null;

ALTER TABLE `final project`.`Progress`
add constraint foreign key fk_Locations_Progress (progress)
references `final project`.`locations` (id);

ALTER TABLE progress ADD 
CONSTRAINT fk_Locations_Progress 
FOREIGN KEY (progress)
REFERENCES locations(id);

drop table progress;

SELECT NOW();

select * from progress;

select * from `Final project`.`Locations`
where id = 1;

select c.ending_node, l.name from `final project`.`connections` c
join `final project`.`locations` l on l.id = c.ending_node
where starting_node = 9;

select p.*, l.name
  FROM Progress p
  JOIN Locations l ON l.id = p.progress;
 
  
  SELECT * FROM Progress
  
 
 