using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using ErrorFactory.Domain;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ErrorFactory.Tests
{
    public class SystemUnderTestBuilder
    {
        private readonly ISubjectsRepository _subjectsRepository;
        private readonly HttpClient _httpClient;

        public SystemUnderTestBuilder()
        {
            _subjectsRepository = new SubjectsRepository(new List<Subject>());
            var apiFactory = new ErrorsWebApplicationFactory(_subjectsRepository);
            
            _httpClient = apiFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://localhost:5000/")
            });
        }
        
        public SystemUnderTestBuilder WithSubjects(IEnumerable<Subject> subjects)
        {
            foreach (var subject in subjects)
            {
                _subjectsRepository.Add(subject);
            }
            return this;
        }
        
        public SystemUnderTestBuilder WithLanguage(string language)
        {
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
            return this;
        }
        
        public SystemUnderTest Build() => new SystemUnderTest(_httpClient, _subjectsRepository);
    }
}