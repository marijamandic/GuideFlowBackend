-- First user entry
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "Location"
)
VALUES 
    (-1, 'admin@gmail.com', 'admin', 0, true, 
    '{"Latitude": 40.7128, "Longitude": -74.0060}');

-- Second user entry
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "Location"
)
VALUES 
    (-11, 'autor1@gmail.com', 'autor1', 1, true, 
    '{"Latitude": 34.0522, "Longitude": -118.2437}');

-- Third user entry
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "Location"
)
VALUES 
    (-12, 'autor2@gmail.com', 'autor2', 1, true, 
    '{"Latitude": 51.5074, "Longitude": -0.1278}');

-- Fourth user entry
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "Location"
)
VALUES 
    (-13, 'autor3@gmail.com', 'autor3', 1, false, 
    '{"Latitude": 48.8566, "Longitude": 2.3522}');

-- Fifth user entry
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "Location"
)
VALUES 
    (-21, 'turista1@gmail.com', 'turista1', 2, true, 
    '{"Latitude": 35.6895, "Longitude": 139.6917}');

-- Sixth user entry
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "Location"
)
VALUES 
    (-22, 'turista2@gmail.com', 'turista2', 2, true, 
    '{"Latitude": -33.8688, "Longitude": 151.2093}');

-- Seventh user entry
INSERT INTO stakeholders."Users"(
    "Id", "Username", "Password", "Role", "IsActive", "Location"
)
VALUES 
    (-23, 'turista3@gmail.com', 'turista3', 2, true, 
    '{"Latitude": 55.7558, "Longitude": 37.6173}');
