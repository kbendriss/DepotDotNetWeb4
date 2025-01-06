using System;
using System.Collections.Generic;

namespace BasketBallLiveScore.Models
{
    public abstract class Member
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public required string LicenseNumber { get; set; }
    }
}
