INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name", "Description", "Level", "Taggs", "Status", "Price")
VALUES 
(-1, 1, 'Tura po planinama', 'Ova tura vodi kroz najlep�e planinske predele sa lokalnim vodi�ima. Trajanje ture je 6 sati sa pauzama.', 3, ARRAY[1, 2, 3]::Integer[], 'Active', 150.00);
INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name", "Description", "Level", "Taggs", "Status", "Price")
VALUES 
(-2, 1, 'Tura kroz doline', 'Ova tura istra�uje doline i livade, pru�aju�i odmor i u�ivanje u prirodnim pejza�ima. Trajanje ture je 4 sata.', 2, ARRAY[4, 5, 6]::Integer[], 'Active', 100.00);
INSERT INTO tours."Tours"(
    "Id", "AuthorId", "Name", "Description", "Level", "Taggs", "Status", "Price")
VALUES 
(-3, 1, 'Tura kroz �ume', 'Pro�ite kroz guste �umske staze uz stru�ne vodi�e. Trajanje ture je 5 sati sa pauzama.', 4, ARRAY[7, 8, 9]::Integer[], 'Active', 120.00);
