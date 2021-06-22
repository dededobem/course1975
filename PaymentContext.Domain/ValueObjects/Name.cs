using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PyamentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsGreaterThan(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
                .IsGreaterThan(LastName, 3, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
    
}