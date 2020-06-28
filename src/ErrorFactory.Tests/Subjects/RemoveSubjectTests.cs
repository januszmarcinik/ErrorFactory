using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ErrorFactory.Tests.Subjects
{
    public class RemoveSubjectTests
    {
        [Fact]
        public async Task RemoveSubject_WhenExists_ShouldReturnOk()
        {
            using var sut = new SystemUnderTestBuilder()
                .WithSubjects(FakeSubjects.List)
                .Build();
            const int id = 1;
            
            var result = await sut.Delete($"/{id}");
            var subject = sut.SubjectsRepository.GetById(id);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            subject.Should().BeNull();
        }
        
        [Theory]
        [InlineData("en", "Subject with id 5 does not exist.")]
        [InlineData("pl", "Przedmiot o id 5 nie istnieje.")]
        public async Task RemoveSubject_WhenDoesNotExist_ShouldReturnAppropriateErrorMessage(string language, string expectedMessage)
        {
            using var sut = new SystemUnderTestBuilder()
                .WithSubjects(FakeSubjects.List)
                .WithLanguage(language)
                .Build();
            const int id = 5;
            
            var result = await sut.Delete($"/{id}");

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.Message.Should().BeEquivalentTo(expectedMessage);
        }
    }
}