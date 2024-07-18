namespace Project_Client.Services
{
	public class MyCookieService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public MyCookieService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GetCookieValue(string cookieName)
		{
			return _httpContextAccessor.HttpContext?.Request.Cookies[cookieName];
		}
	}
}
