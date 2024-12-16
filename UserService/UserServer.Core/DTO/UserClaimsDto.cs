
namespace UserServer.Core.DTO
{
    public class UserClaimsDto
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }        
        public IEnumerable<string>? Rolles { get; set; }
    }
}
