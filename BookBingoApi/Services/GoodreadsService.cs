using AutoMapper;
using BookBingoApi.Dtos.Goodreads.Search.Books;
using BookBingoApi.Dtos.Goodreads.Shelf;
using BookBingoApi.Dtos.Goodreads.User;
using BookBingoApi.Exceptions;
using BookBingoApi.Models;
using BookBingoApi.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookBingoApi.Services
{
    public class GoodreadsService : IBooksService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsSnapshot<GoodreadsOptions> _options;
        private readonly IMapper _mapper;
        private readonly IOAuthService _oauthService;

        public GoodreadsService(
            HttpClient httpClient,
            IOAuthService oauthService,
            IOptionsSnapshot<GoodreadsOptions> options,
            IMapper mapper)
        {
            _httpClient = httpClient;
            _oauthService = oauthService;
            _options = options;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string query)
        {
            var serializer = new XmlSerializer(typeof(BookSearchResponse));
            var message = new HttpRequestMessage(HttpMethod.Get, $"{_httpClient.BaseAddress}search/index.xml?key={_options.Value.ApiKey}&q={query}");
            var response = await MakeRequest(message);

            var responseBody = serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as BookSearchResponse;
            return _mapper.Map<IEnumerable<Book>>(responseBody.Result.Items);
        }

        public async Task<IEnumerable<BookshelfEntry>> GetShelf(string shelfName, string userId, string session)
        {
            var serializer = new XmlSerializer(typeof(ShelfResponse));

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_httpClient.BaseAddress}review/list/{userId}.xml?shelf={shelfName}&v=2&key={_options.Value.ApiKey}")
            };
            await _oauthService.SignRequestAsync(request, session);

            var response = await MakeRequest(request);
            var responseBody = serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as ShelfResponse;
            return _mapper.Map<IEnumerable<BookshelfEntry>>(responseBody.Result.Items);
        }

        public async Task<Models.User> GetLoggedInUserAsync(string session)
        {
            var serializer = new XmlSerializer(typeof(UserResponse));
            var request = await _oauthService.SignRequestAsync(new HttpRequestMessage(HttpMethod.Get, $"{_httpClient.BaseAddress}api/auth_user"), session);
            var response = await MakeRequest(request);

            var responseBody = serializer.Deserialize(await response.Content.ReadAsStreamAsync()) as UserResponse;
            return _mapper.Map<Models.User>(responseBody.User);
        }

        private async Task<HttpResponseMessage> MakeRequest(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                throw new ApiNotAuthorisedException(request.RequestUri);
            }

            return response;
        }
    }
}
