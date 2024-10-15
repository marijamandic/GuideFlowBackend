﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;

public class ProblemDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TourId { get; set; }
    public string Category { get; set; }
    public string Priority { get; set; }
    public string Description { get; set; }
    public DateOnly ReportedAt { get; set; }
}
