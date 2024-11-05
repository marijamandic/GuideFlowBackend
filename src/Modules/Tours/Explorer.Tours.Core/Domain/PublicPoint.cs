using Explorer.BuildingBlocks.Core.Domain;
using System;

namespace Explorer.Tours.Core.Domain
{
    public class PublicPoint : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string ImageUrl { get; private set; }
        public ApprovalStatus ApprovalStatus { get; private set; }
        public PointType PointType { get; private set; } 

        public PublicPoint(string name, string description, double longitude, double latitude, string imageUrl, ApprovalStatus approvalStatus, PointType pointType)
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            ImageUrl = imageUrl;
            ApprovalStatus = approvalStatus;
            PointType = pointType;

            Validate(); 
        }
        public PublicPoint()
        {
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name.");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description.");
            if (Latitude < -90 || Latitude > 90) throw new ArgumentException("Invalid Latitude value.");
            if (Longitude < -180 || Longitude > 180) throw new ArgumentException("Invalid Longitude value.");
        }
        public void ApproveRequest()
        {
            ApprovalStatus = ApprovalStatus.Accepted;
        }

        public void RejectRequest()
        {
            ApprovalStatus = ApprovalStatus.Rejected;
        }

        public bool IsVisibleToPublic()
        {
            return ApprovalStatus == ApprovalStatus.Accepted;
        }
    }

    public enum ApprovalStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public enum PointType
    {
        Checkpoint,
        Object
    }
}
