using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using PyamentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : 
        Notifiable<Notification>, 
        IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if(!command.IsValid)
            {
                AddNotifications(command);  
                return new CommandResult(false, "A assinatura não foi criada!");
            }                

            if(_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "CPF já existe.");

            if(_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "E-mail já existe.");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(
                command.Street, 
                command.Number, 
                command.Neighborhood, 
                command.City, 
                command.State, 
                command.Country, 
                command.ZipCode);
            
            var subscription = new Subscription(null, true);
            var student = new Student(name, document, email);          
            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, subscription, student, payment);

            if(!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            _studentRepository.CreateSubscription(subscription);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo!", "Sua assinatura foi realizada.");

            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}