using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IPublicPointNotificationService
    {
        Result<PublicPointNotificationDto> Create(PublicPointNotificationDto publicPointNotification);
        Result<PublicPointNotificationDto> Update(PublicPointNotificationDto publicPointNotification);
        Result Delete(int id);
        Result<PagedResult<PublicPointNotificationDto>> GetPaged(int page, int pageSize);
        Result<PublicPointNotificationDto> Get(int id);
        Result<IEnumerable<PublicPointNotificationDto>> GetUnreadByAuthor(int authorId);
        Result<IEnumerable<PublicPointNotificationDto>> GetAllByAuthor(int authorId);
    }
}
