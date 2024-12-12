﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Club
{
    public enum ClubInvitationStatus
    {
        PENDING,
        ACCEPTED,
        DECLINED,
        CANCELLED
    }

    public class ClubInvitationDto
    {
        public long Id { get; set; }
        public long ClubId { get; set; }
        public long TouristId { get; set; }
        public ClubInvitationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsOpened { get; set; }
        public long OwnerId { get; set; }
        public string ClubName { get; set; }
        public string TouristName { get; set; }
    }

}
