using Microsoft.AspNetCore.Mvc;
using WebApplication5.Database;
using WebApplication5.Database.Entites;

namespace WebApplication5.Controllers
{
	/// /// <summary>
	///	This class collects information about users.
	/// </summary>
	public class UsersController : Controller
	{
		public void Add(string login, string password) => Repo.AddUser(login, password);

		public bool Contains(string login) => Repo.ContainUser(login);

		public User Get(string login) => Repo.GetUserByLogin(login);

		private static readonly UserRepository Repo = new UserRepository();
	}
}