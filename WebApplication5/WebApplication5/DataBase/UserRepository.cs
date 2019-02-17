using System.Linq;
using WebApplication5.Database.Entites;

namespace WebApplication5.Database
{
	public class UserRepository
	{
		public UserRepository(DataBase context)
		{
			_context = context;
		}

		public UserRepository()
		{
			_context = new DataBase();
		}

		public void AddUser(string login, string password)
		{
			if (!_context.Users.Any(u => u.Login == login))
				_context.Users.Add(new User(login, password));
			_context.SaveChanges();
		}

		public bool ContainUser(string login) => _context.Users.Any(u => u.Login.Equals(login));

		public User GetUserByLogin(string login) => _context.Users.First(u => u.Login == login);

		private readonly DataBase _context;
	}
}