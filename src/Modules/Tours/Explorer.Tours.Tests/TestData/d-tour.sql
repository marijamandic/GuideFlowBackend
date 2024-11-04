-- First entry
INSERT INTO tours."Tours"(
    "Id", "Name", "Description", "Level", "Taggs", "Status", "Price", "LengthInKm", "AverageGrade", "TransportDurations")
VALUES 
(
    -1, 
    'Tura po planinama', 
    'Ova tura vodi kroz najlep�e planinske predele sa lokalnim vodi?ima. Trajanje ture je 6 sati sa pauzama.', 
    0,  -- Equivalent to Level.Easy
    '{"Planine", "Lepota", "Priroda"}', 
    1, 
    '{"Cost": 150.00, "Currency": 0}'::jsonb,  -- JSON as a JSONB object
    150.00, 
    4.5, 
    '[{"Time": "00:30:00", "TransportType": 0}, {"Time": "00:45:00", "TransportType": 1}]'::jsonb  -- TransportDurations as a JSONB array
);

-- Second entry
INSERT INTO tours."Tours"(
    "Id", "Name", "Description", "Level", "Taggs", "Status", "Price", "LengthInKm", "AverageGrade", "TransportDurations")
VALUES 
(
    -2, 
    'Tura kroz doline', 
    'Ova tura istra�uje doline i livade, pru�aju?i odmor i u�ivanje u prirodnim pejza�ima. Trajanje ture je 4 sata.', 
    1,  -- Equivalent to Level.Advanced
    '{"Doline", "Priroda", "Relax"}', 
    0, 
    '{"Cost": 100.00, "Currency": 0}'::jsonb, 
    80.00, 
    4.2,
    '[{"Time": "00:20:00", "TransportType": 2}, {"Time": "00:40:00", "TransportType": 1}]'::jsonb 
);

-- Third entry
INSERT INTO tours."Tours"(
    "Id", "Name", "Description", "Level", "Taggs", "Status", "Price", "LengthInKm", "AverageGrade", "TransportDurations")
VALUES 
(
    -3, 
    'Tura kroz �ume', 
    'Pro?ite kroz guste �umske staze uz stru?ne vodi?e. Trajanje ture je 5 sati sa pauzama.', 
    2,  -- Equivalent to Level.Expert
    '{"�ume", "Avantura", "Divljina"}', 
    0, 
    '{"Cost": 120.00, "Currency": 0}'::jsonb, 
    100.00, 
    4.7,
    '[{"Time": "00:35:00", "TransportType": 0}, {"Time": "00:50:00", "TransportType": 2}]'::jsonb
);
