using System;

namespace DevTeamup.Models
{
    public interface ITimeStamp
    {
        DateTime CreatedOn { get; set; }
        DateTime ModifiedOn { get; set; }
    }
}
