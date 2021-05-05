INSERT INTO `Final project`.`Locations` (`id`, `name`, `street`, `intro`) VALUES
(1, 'London Eye', 'London   SE1 7PB', 'Huge observation wheel giving passengers a privileged bird\'s-eye view of the city\'s landmarks'),
(2, 'SEA LIFE Centre London ', 'London  SE1 7PB', 'Sea life is an aquarium with a large collection of marine animals and is the largest collection of coral reefs in the UK.'),
(3, 'Big Ben', 'London SW1A 0AA', 'Big Ben is the famous clock in London.'),
(4, 'Westminster Abbey', 'London SW1P 3PA', 'Since 1066, the Abbey has hosted every coronation, and is the final resting place for the great kings, queens, poets, musicians, scientists and politicians of our past.'),
(5, 'St James\'s Park', 'London SW1A 2BJ', 'Green space with a lake with daily pelican feeding, a grass-roofed cafe and ceremonial displays.'),
(6, 'Buckingham Palace', 'London SW1A 1AA', 'You might be getting a treat to watch the Queen\'s guards marching and watch Changing of the guards ceremony. The official start time of the ceremony is 11:00 AM'),
(7, 'Piccadilly Circus', 'London  W1J 9HS', 'The square is famous for its neon signs, different displays and the Eros fountain. Piccadilly Circus offers a variety of cinemas, theatres, shops and restaurants, including famous traditional English pubs.'),
(8, 'Trafalgar Square', 'London WC2N 5DN', 'Trafalgar Square is one of the most important and bustling squares in London: designed in 1830 to commemorate the British victory against the French and Spanish fleets in the Battle of Trafalgar.'),
(9, 'The National Gallery', 'London WC2N 5DN', 'The National Gallery is a truly amazing place to be, especially for art lovers. Founded in 1824, it houses a collection of over 2,300 paintings dating from the mid-13th century to 1900. Admission to the gallery is completely free. ');

INSERT INTO `Final project`.`Connections` (`id`, `starting_node`, `ending_node`) VALUES
(1, 1, 2),
(2, 1, 4),
(3, 1, 8),
(4, 2, 3),
(5, 3, 4),
(6, 3, 6),
(7, 4, 5),
(8, 4, 7),
(9, 4, 8),
(10, 5, 7),
(11, 6, 7),
(12, 6, 9),
(13, 7, 9),
(14, 8, 9);
