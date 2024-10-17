using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos;
public class ProblemDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TourId { get; set; }
    public ProblemCategory Category { get; set; }
    public ProblemPriority Priority { get; set; }
    public string Description { get; set; }
    public DateOnly ReportedAt { get; set; }
}

public enum ProblemCategory
{
    Accommodation,
    Transportation,
    Guides,
    Organization,
    Safety
}

public enum ProblemPriority
{
    High,
    Medium,
    Low
}