using System.Net;
using System.Threading.Tasks;
using ErrorFactory.Domain.Commands;
using FluentAssertions;
using Xunit;

namespace ErrorFactory.Tests.Subjects
{
    public class AddSubjectTests
    {
        [Fact]
        public async Task AddSubject_WithCorrectData_ShouldReturnOk()
        {
            using var sut = new SystemUnderTestBuilder()
                .WithSubjects(FakeSubjects.List)
                .Build();
            const int expectedId = 5;
            const string name = "Systemy autonomiczne";
            
            var command = new AddSubjectCommand(name);

            var result = await sut.Post("", command);
            var subject = sut.SubjectsRepository.GetById(expectedId);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            subject.Should().NotBeNull();
            subject.Name.Should().Be(name);
        }
        
        [Theory]
        [InlineData("en", "Subject with name Wzorce projektowe already exists.")]
        [InlineData("pl", "Przedmiot o nazwie Wzorce projektowe już istnieje.")]
        public async Task AddSubject_WhenNameAlreadyExist_ShouldReturnAppropriateErrorMessage(string language, string expectedMessage)
        {
            using var sut = new SystemUnderTestBuilder()
                .WithSubjects(FakeSubjects.List)
                .WithLanguage(language)
                .Build();
            var command = new AddSubjectCommand("Wzorce projektowe");
            
            var result = await sut.Post("", command);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.Message.Should().BeEquivalentTo(expectedMessage);
        }
    }
}