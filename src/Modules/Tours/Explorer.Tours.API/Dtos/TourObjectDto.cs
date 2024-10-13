﻿namespace Explorer.Tours.API.Dtos;
public enum ObjectCategory
{
    Parking,
    Restaurant,
    Toilet,
    Other
}
public class TourObjectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string ImageUrl { get; set; }
    public ObjectCategory Category { get; set; }
}
