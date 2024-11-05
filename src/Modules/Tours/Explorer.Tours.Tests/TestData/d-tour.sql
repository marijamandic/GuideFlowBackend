INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name", "Description", "Level", "Taggs", "Status", "Price")
VALUES 
(-1, 1, 'Tura po planinama', 'Ova tura vodi kroz najlepše planinske predele sa lokalnim vodièima. Trajanje ture je 6 sati sa pauzama.', 3, ARRAY[1, 2, 3]::Integer[], 'Active', 150.00);
INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name", "Description", "Level", "Taggs", "Status", "Price")
VALUES 
(-2, 1, 'Tura kroz doline', 'Ova tura istražuje doline i livade, pružajuæi odmor i uživanje u prirodnim pejzažima. Trajanje ture je 4 sata.', 2, ARRAY[4, 5, 6]::Integer[], 'Active', 100.00);
INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name", "Description", "Level", "Taggs", "Status", "Price")
VALUES 
(-3, 1, 'Tura kroz šume', 'Proðite kroz guste šumske staze uz struène vodièe. Trajanje ture je 5 sati sa pauzama.', 4, ARRAY[7, 8, 9]::Integer[], 'Active', 120.00);
