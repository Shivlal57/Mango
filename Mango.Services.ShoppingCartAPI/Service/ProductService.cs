using Mango.Services.ShoppingCartAPI.Models.Dto;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;

namespace Mango.Services.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClient;
        public ProductService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var client = _httpClient.CreateClient("Product");
            var response = await client.GetAsync($"/api/ProductAPI");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(resp.Result));
            }
            return new List<ProductDto>();
        }
    }
}
