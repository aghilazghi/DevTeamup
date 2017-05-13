namespace DevTeamup.Dtos
{
    public class CollaborationDto
    {
        public int TeamupId { get; set; }

        public TeamupDto Teamup { get; set; }

        public ApplicationUserDto Contributor { get; set; }
    }
}