namespace Project_Client.Models
{
    public class LoginResponse
    {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }

		public string RedirectUrl { get; set; }
		public List<Role> Role { get; set; }
		public string JwtToken { get; set; }
	}

	public class Role
	{
		public int RoleId { get; set; }
		public string RoleName { get; set; } = null!;
		public string Description { get; set; } = null!;
	}


}
