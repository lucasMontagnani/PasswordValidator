using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PasswordValidator.API.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.IntegrationTests
{
    public class PasswordValidationControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PasswordValidationControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("aa", false)]
        [InlineData("ab", false)]
        [InlineData("AAAbbbCc", false)]
        [InlineData("AbTp9!foo", false)]
        [InlineData("AbTp9!foA", false)]
        [InlineData("AbTp9 fok", false)]
        [InlineData("AbTp9!fok", true)]
        public async Task Post_ShouldReturnExpectedValidationResult(string password, bool expected)
        {
            var request = new ValidatePasswordRequest(password);

            var httpResponse = await _client.PostAsJsonAsync("/api/PasswordValidation/password-validation", request);

            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadFromJsonAsync<ValidatePasswordResponse>();

            Assert.NotNull(response);
            Assert.Equal(expected, response!.IsValid);
        }
    }
}
