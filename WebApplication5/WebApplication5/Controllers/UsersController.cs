using Microsoft.AspNetCore.Mvc;
using WebApplication5.Database;
using WebApplication5.Database.Entites;

namespace WebApplication5.Controllers
{
	/// /// <summary>
	///	This class collects information about users.
	/// </summary>
	[Route("[controller]/[action]")]
	public class UsersController : Controller
	{
		[HttpGet]
		public void Add(string login, string password) => Repo.AddUser(login, password);

		[HttpGet]
		public bool Contains(string login) => Repo.ContainUser(login);

		[HttpGet]
		public bool Check(string login, string password) => Repo.CheckPassword(login, password);

		[HttpGet]
		public User Get(string login) => Repo.GetUserByLogin(login);

		private static readonly UserRepository Repo = new UserRepository();
	}
}