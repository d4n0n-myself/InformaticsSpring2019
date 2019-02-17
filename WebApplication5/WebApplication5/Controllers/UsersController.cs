using Microsoft.AspNetCore.Mvc;
using WebApplication5.Database;
using WebApplication5.Database.Entites;

namespace WebApplication5.Controllers
{
	public class UsersController : Controller
	{
		public void AddUser(string login, string password) => Repo.AddUser(login, password);

		public bool ContainUser(string login) => Repo.ContainUser(login);

		public User GetUser(string login) => Repo.GetUserByLogin(login);

		private static readonly UserRepository Repo = new UserRepository();
	}
}