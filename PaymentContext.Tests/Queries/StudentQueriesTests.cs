using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.Services;
using PyamentContext.Domain.ValueObjects;
using Xunit;

namespace PaymentContext.Tests.Queries
{
    public class StudentQueriesTests
    {
        private IList<Student> _students;

        public StudentQueriesTests()
        {
            _students = new List<Student>();
            for(var i = 0; i < 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("111111111", EDocumentType.CPF),
                    new Email(i.ToString() + "@teste.com")
                ));
            }
        }

        [Fact]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.Equal(null, studn);
        }

        [Fact]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("111111111");
            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.NotEqual(null, studn);
        }
    }
}