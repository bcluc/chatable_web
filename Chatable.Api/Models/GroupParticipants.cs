using Postgrest.Attributes;
using Postgrest.Models;

namespace Chatable.Api.Models
{
	[Table("_group_participants")]
	public class GroupParticipants : BaseModel
	{
		[PrimaryKey("group_id", true)]
		public string GroupId { get; set; }
		[PrimaryKey("username", true)]
		public string MemberId { get; set; }
		[Column("nickname")]
		public string NickName { get; set; }
	}
}
