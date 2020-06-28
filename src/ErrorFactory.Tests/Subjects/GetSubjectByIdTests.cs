using System.Net;
using System.Threading.Tasks;
using ErrorFactory.Domain;
using FluentAssertions;
using Xunit;

namespace ErrorFactory.Tests.Subjects
{
    public class GetSubjectByIdTests
    {
        [Fact]
        public async Task GetSubjectById_WhenExists_ShouldReturnOk()
        {
            using var sut = new SystemUnderTestBuilder()
                .WithSubjects(FakeSubjects.List)
                .Build();
            var expected = new Subject(4, "Wzorce projektowe");
            
            var result = await sut.Get<Subject>($"/{expected.Id}");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Value.Should().BeEquivalentTo(expected);
        }
        
        [Theory]
        [InlineData("en", "Subject with id 5 does not exist.")]
        [InlineData("pl", "Przedmiot o id 5 nie istnieje.")]
        public async Task GetSubjectById_WhenDoesNotExists_ShouldReturnAppropriateErrorMessage(string language, string expectedMessage)
        {
            using var sut = new SystemUnderTestBuilder()
                .WithSubjects(FakeSubjects.List)
                .WithLanguage(language)
                .Build();
            
            var result = await sut.Get<Subject>("/5");

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.ErrorMessage.Should().BeEquivalentTo(expectedMessage);
        }
    }
}