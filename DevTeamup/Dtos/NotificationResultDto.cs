using System.Collections.Generic;

namespace DevTeamup.Dtos
{
    public class NotificationResultDto
    {
        public int Unread { get; set; }

        public IEnumerable<NotificationDto> All { get; set; }
    }
}