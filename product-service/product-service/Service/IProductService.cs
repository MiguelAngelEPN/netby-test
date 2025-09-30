using product_service.Dto;
using product_service.Models;

namespace product_service.Service
{
    public interface IProductService
    {
        Task<AnswerModel> RegisterProduct(ProductoDto dto);
        Task<AnswerModel> UpsateProduct(Producto dto);
        Task<AnswerModel> DeleteProduct(int productId);
        Task<AnswerModel> GetListProduct(int page, int amount);
    }
}
