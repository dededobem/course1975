using PaymentContext.Domain.Enums;
using PyamentContext.Domain.ValueObjects;
using Xunit;

namespace PaymentContext.Tests.ValueObjects
{
    public class DocumentTests
    {
        [Fact]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            //Arrange
            var document = new Document("991823933", EDocumentType.CNPJ);

            //Act & Assert
            Assert.False(document.IsValid);

        }

        [Fact]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            //Arrange
            var document = new Document("62562505000105", EDocumentType.CNPJ);

            //Act & Assert
            Assert.True(document.IsValid);
        }

        [Fact]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            //Arrange
            var document = new Document("1542", EDocumentType.CPF);

            //Act & Assert
            Assert.False(document.IsValid);
        }

        [Theory]
        [InlineData("68100131058")]
        [InlineData("17548290071")]
        [InlineData("61639705007")]
        public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
        {
            //Arrange
            var document = new Document(cpf, EDocumentType.CPF);

            //Act & Assert
            Assert.True(document.IsValid);
        }
    }
}