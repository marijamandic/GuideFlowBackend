INSERT INTO tours."Tours"(
	"Id", "Name", "AuthorId", "Description", "Level", "Status", "StatusChangeDate", "LengthInKm", "Price", "AverageGrade", "Taggs", "WeatherRequirements", "TransportDurations")
VALUES 
	(-1, 'Mountain Adventure', -12, 'A thrilling mountain adventure tour.', 0, 0, NULL, 15.5, 
    120, 4.5, ARRAY['Adventure', 'Mountain', 'Hiking'],'{{"MinTemperature": 0, "MaxTemperature": 10, "SuitableConditions": [0,1]}}',
    '[{{"Time": 180, "TransportType": 0}}, {{"Time": 60, "TransportType": 1}}]');
INSERT INTO tours."Tours"(
	"Id", "Name", "AuthorId", "Description", "Level", "Status", "StatusChangeDate", "LengthInKm", "Price", "AverageGrade", "Taggs", "WeatherRequirements", "TransportDurations")
VALUES 
	(-2, 'City Exploration', -12, 'Explore the city with guided tours and hidden spots.', 1, 1,NULL, 10.0, 
    50, 4.8, ARRAY['Adventure', 'Mountain', 'Hiking'],'{{"MinTemperature": 0, "MaxTemperature": 10, "SuitableConditions": [1,2]}}',
    '[{{"Time": 120, "TransportType": 2}}, {{"Time": 30, "TransportType": 0}}]');
INSERT INTO tours."Tours"(
	"Id", "Name", "AuthorId", "Description", "Level", "Status", "StatusChangeDate", "LengthInKm", "Price", "AverageGrade", "Taggs", "WeatherRequirements", "TransportDurations")
VALUES 
	(-3, 'Beach Getaway', -12, 'Relaxing beach getaway with scenic routes.', 0, 2, NULL, 8.0, 
    80, 4.0, ARRAY['Adventure', 'Mountain', 'Hiking'], '{{"MinTemperature": 0, "MaxTemperature": 10, "SuitableConditions": [1,3]}}',
    '[{{"Time": 90, "TransportType": 1}}, {{"Time": 45, "TransportType": 2}}]'); 
INSERT INTO tours."Tours"(
    "Id", "Name", "AuthorId", "Description", "Level", "Status", "StatusChangeDate", "LengthInKm", "Price", "AverageGrade", "Taggs", "WeatherRequirements", "TransportDurations")
VALUES 
    (-11, 'Lakeside Adventure', -12, 'Discover beautiful lakes and scenic routes.', 1, 0, NULL, 12.0, 
   100, 4.7, ARRAY['Nature', 'Lake', 'Hiking'], '{{"MinTemperature": 0, "MaxTemperature": 10, "SuitableConditions": [3,4]}}',
    '[{{"Time": 150, "TransportType": 0}}, {{"Time": 60, "TransportType": 1}}]');
INSERT INTO tours."Tours"(
    "Id", "Name", "AuthorId", "Description", "Level", "Status", "StatusChangeDate", "LengthInKm", "Price", "AverageGrade", "Taggs", "WeatherRequirements", "TransportDurations")
VALUES 
    (-12, 'Forest Expedition', -12, 'Explore dense forests and wildlife.', 2, 1, NULL, 20.0, 
    200, 4.9, ARRAY['Forest', 'Wildlife', 'Adventure'], '{{"MinTemperature": 0, "MaxTemperature": 10, "SuitableConditions": [1,2]}}',
    '[{{"Time": 240, "TransportType": 0}}, {{"Time": 90, "TransportType": 1}}]');