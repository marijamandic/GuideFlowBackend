-- Prvi unos
INSERT INTO tours."Tours"(
    "Id", "Name", "Description", "Level", "Taggs", "Status", "Price", "LengthInKm", "AverageGrade", "Checkpoints", "TransportDurations", "Reviews")
VALUES 
(
    -1, 
    'Tura po planinama', 
    'Ova tura vodi kroz najlepše planinske predele sa lokalnim vodi?ima. Trajanje ture je 6 sati sa pauzama.', 
    2,  -- Ovaj broj odgovara `Level.Advanced`
    '[Planine, Lepota, Priroda]',  -- Pretvaranje liste u tekstualni niz
    'Published', 
    '{"Amount": 150.00, "Currency": "EUR"}',  -- JSON kao string (može biti analiziran na strani aplikacije)
    150.00, 
    4.5,  -- Prose?na ocena ture
    '[Start, Middle, End]',  -- Checkpoints kao tekstualni niz
    '[00:30:00, 00:45:00]',  -- TransportDurations kao tekstualni niz intervala
    '[Review 1, Review 2]'  -- Recenzije kao tekstualni niz
);

-- Drugi unos
INSERT INTO tours."Tours"(
    "Id", "Name", "Description", "Level", "Taggs", "Status", "Price", "LengthInKm", "AverageGrade", "Checkpoints", "TransportDurations", "Reviews")
VALUES 
(
    -2, 
    'Tura kroz doline', 
    'Ova tura istražuje doline i livade, pružaju?i odmor i uživanje u prirodnim pejzažima. Trajanje ture je 4 sata.', 
    1,  -- Ovaj broj odgovara `Level.Easy`
    '[Doline, Priroda, Relax]', 
    'Published', 
    '{"Amount": 100.00, "Currency": "EUR"}', 
    80.00, 
    4.2,
    '[Dolazak, Odmor, Kraj]', 
    '[00:20:00, 00:40:00]', 
    '[Review A, Review B]'
);

-- Tre?i unos
INSERT INTO tours."Tours"(
    "Id", "Name", "Description", "Level", "Taggs", "Status", "Price", "LengthInKm", "AverageGrade", "Checkpoints", "TransportDurations", "Reviews")
VALUES 
(
    -3, 
    'Tura kroz šume', 
    'Pro?ite kroz guste šumske staze uz stru?ne vodi?e. Trajanje ture je 5 sati sa pauzama.', 
    3,  -- Ovaj broj odgovara `Level.Expert`
    '[Šume, Avantura, Divljina]', 
    'Published', 
    '{"Amount": 120.00, "Currency": "EUR"}', 
    100.00, 
    4.7,
    '[Po?etak, Sredina, Kraj]', 
    '[00:35:00, 00:50:00]', 
    '[Review X, Review Y]'
);
