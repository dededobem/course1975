using PaymentContext.Domain.Commands;
using Xunit;

namespace PaymentContext.Tests.Commands
{
    public class CreateBoletoSubscriptionCommandTests
    {
        [Fact]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            //Arrange
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "";

            //Act 
            command.Validate();

            //Assert
            Assert.False(command.IsValid);

        }        
    }
}