using System.Net.Http.Json;
using DevVault.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DevVault.Tests.Api
{
    public class ProjectsEndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ProjectsEndpointTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task PostProject_ShouldReturnCreated()
        {
            var project = new
            {
                name = "Integration Test Project",
                description = "Testing API POST endpoint",
                ownerId = Guid.NewGuid()
            };

            var response = await _client.PostAsJsonAsync("/api/projects", project);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }
    }
}
