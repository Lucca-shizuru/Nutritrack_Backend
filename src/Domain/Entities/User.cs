namespace NutriTrack.src.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;

        private readonly List<Meal> _meals = new();

        public IReadOnlyCollection<Meal> Meals => _meals.AsReadOnly();

        private readonly List<object> _domainEvents = new();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        private User() { }
        public User(string name, string email, string passwordHash)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;

            _domainEvents.Add(new { Action = "UserRegistered", Id, Email });
        }   
        public void UpdatePersonalInfo(string newName, string newEmail)
        {
            Name = newName;
            Email = newEmail;
        }

        public void ClearDomainEvents() => _domainEvents.Clear();

    }
}
