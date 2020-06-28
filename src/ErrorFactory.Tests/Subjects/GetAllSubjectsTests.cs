using System.Net;
using System.Threading.Tasks;
using ErrorFactory.Domain;
using FluentAssertions;
using Xunit;

namespace ErrorFactory.Tests.Subjects
{
    public class GetAllSubjectsTests
    {
        [Fact]
        public async Task GetAllSubjects_WithExists_ShouldReturnOk()
        {
            var subjects = FakeSubjects.List;
            using var sut = new SystemUnderTestBuilder()
                .WithSubjects(subjects)
                .Build();
            
            var result = await sut.Get<Subject[]>("");

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Value.Should().BeEquivalentTo(subjects);
        }
        
        [Theory]
        [InlineData("en", "Unhandled exception occurred. Please check logs for more details.")]
        [InlineData("pl", "Wystąpił niespodziewany błąd aplikacji. Proszę sprawdzić logi w celu odczytania szczegółów.")]
        public async Task GetAllSubjects_WhenDoesNotExists_ShouldReturnAppropriateErrorMessage(string language, string expectedMessage)
        {
            using var sut = new SystemUnderTestBuilder()
                .WithLanguage(language)
                .Build();
            
            var result = await sut.Get<Subject[]>("");

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            result.ErrorMessage.Should().BeEquivalentTo(expectedMessage);
        }
    }
}