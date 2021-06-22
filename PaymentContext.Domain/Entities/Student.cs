using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.Entities;
using PyamentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscription;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscription = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Address { get; private set; }
        public IReadOnlyList<Subscription> Subscriptions { get => _subscription.ToArray(); }       

        public void AddSubscription(Subscription subscription){
            var hasSubsctiptionActive = false;
            foreach (var item in _subscription)
            {
                if (item.Active)
                    hasSubsctiptionActive = true;
            }
            
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsFalse(
                    hasSubsctiptionActive, 
                    "Student.Subscription", 
                    "Você já possui uma assinatura ativa."
                )
                .AreNotEquals(
                    0, 
                    subscription.Payments.Count, 
                    "Student.Subscription.Payments", 
                    "Esta assinatura não possui pagamentos."
                )
            ); 

            _subscription.Add(subscription);           
                
        } 
        
    }
}