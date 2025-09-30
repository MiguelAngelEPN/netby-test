using transaccion_service.Dto;
using transaccion_service.Models;

namespace transaccion_service.Service
{
    public interface ITransaccionService
    {
        Task<AnswerModel> RegisterTransaccion(TransaccionDto dto);
        Task<AnswerModel> GetListTransaccion(int page, int amount);
        Task<AnswerModel> UpdateTransaccion(Transaccion transaccion);
        Task<AnswerModel> DeleteTransaccion(int id);

    }
}
