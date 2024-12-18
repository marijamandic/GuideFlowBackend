using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IAuthorDashboardService
    {
        double GetAverageGradeForAuthor(long authorId);
        Dictionary<int, int> GetReviewsPartitionedByGrade(long authorId);
    }
}
