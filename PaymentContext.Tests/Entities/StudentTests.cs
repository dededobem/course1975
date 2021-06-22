using System;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PyamentContext.Domain.ValueObjects;
using Xunit;

namespace PaymentContext.Tests.Entities
{
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly PayPalPayment _payment;

        public StudentTests()
        {
            _name = new Name("André", "Dantas");
            _document = new Document("01786171562", EDocumentType.CPF);
            _email = new Email("dededobem@gmail.com");
            _address = new Address("Rua x", "10", "Bairro feliz", "FSA", "BA", "Brasil", "44125000");
            _subscription = new Subscription(null, true);
            _student = new Student(_name, _document, _email);            
            _payment = new PayPalPayment(
                "123456", 
                DateTime.Now, 
                DateTime.Now.AddDays(5), 
                100, 
                100, 
                "André",
                _document,
                _address,
                _email);
        }

        [Fact]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {                
            _subscription.AddPayment(_payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.False(_student.IsValid);
        }

        [Fact]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {  
            _student.AddSubscription(_subscription);

            Assert.False(_student.IsValid);
        }

        [Fact]
        public void ShouldReturnSuccessWhenAddSubscription()
        {            
            _subscription.AddPayment(_payment);
            _student.AddSubscription(_subscription);

            Assert.True(_student.IsValid);
        }
    }
}