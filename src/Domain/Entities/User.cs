namespace NutriTrack.src.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        private readonly List<Meal> _meals = new();

        public IReadOnlyCollection<Meal> Meals => _meals.AsReadOnly();

        private User() { }
        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
        public void UpdatePersonalInfo(string newName, string newEmail)
        {
            Name = newName;
            Email = newEmail;
        }


    }
}
