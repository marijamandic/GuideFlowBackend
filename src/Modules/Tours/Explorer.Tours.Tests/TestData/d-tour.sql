-- First entry
INSERT INTO tours."Tours"(
	"Id", "Name", "AuthorId", "Description", "Level", "Status", "StatusChangeDate", "LengthInKm", "Price", "AverageGrade", "Taggs", "TransportDurations")
VALUES 
	(-1, 'Mountain Adventure', 101, 'A thrilling mountain adventure tour.', 0, 0, NULL, 15.5, 
    '{{"Cost": 120.00, "Currency": 1}}', 4.5, ARRAY['Adventure', 'Mountain', 'Hiking'], 
    '[{{"Time": 180, "TransportType": 0}}, {{"Time": 60, "TransportType": 1}}]');
INSERT INTO tours."Tours"(
	"Id", "Name", "AuthorId", "Description", "Level", "Status", "StatusChangeDate", "LengthInKm", "Price", "AverageGrade", "Taggs", "TransportDurations")
VALUES 
	(-2, 'City Exploration', 102, 'Explore the city with guided tours and hidden spots.', 1, 1,'2024-11-05T10:00:00Z', 10.0, 
    '{{"Cost": 50.00, "Currency": 2}}', 4.8, ARRAY['Adventure', 'Mountain', 'Hiking'], 
    '[{{"Time": 120, "TransportType": 2}}, {{"Time": 30, "TransportType": 0}}]');
INSERT INTO tours."Tours"(
	"Id", "Name", "AuthorId", "Description", "Level", "Status", "StatusChangeDate", "LengthInKm", "Price", "AverageGrade", "Taggs", "TransportDurations")
VALUES 
	(-3, 'Beach Getaway', 103, 'Relaxing beach getaway with scenic routes.', 0, 2, '2024-11-05T10:00:00Z', 8.0, 
    '{{"Cost": 80.00, "Currency": 0}}', 4.0, ARRAY['Adventure', 'Mountain', 'Hiking'], 
    '[{{"Time": 90, "TransportType": 1}}, {{"Time": 45, "TransportType": 2}}]');
