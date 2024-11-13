-- Insert a new tour review
INSERT INTO tours."TourReviews"(
    "Id", "Rating", "Comment", "TourDate", "CreationDate", "PercentageCompleted", "TouristId", "TourId")
VALUES 
    (-1, 5, 'Great tour!', '2024-10-20', '2024-10-20', 100, 1, -1);

-- Insert another tour review
INSERT INTO tours."TourReviews"(
    "Id", "Rating", "Comment", "TourDate", "CreationDate", "PercentageCompleted", "TouristId", "TourId")
VALUES 
    (-2, 4, 'Amazing experience, but could improve the itinerary.', '2024-10-21', '2024-10-21', 90, -21, -2);

-- Insert a third tour review
INSERT INTO tours."TourReviews"(
    "Id", "Rating", "Comment", "TourDate", "CreationDate", "PercentageCompleted", "TouristId", "TourId")
VALUES 
    (-3, 3, 'The tour was okay, but some of the stops were too short.', '2024-10-22', '2024-10-22', 75, -23, -3);
