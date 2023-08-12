namespace PlantNestBackEnd.Services
{
    public interface IOrderDetail
    {
        Task<dynamic> findByIdOrder(int idOrder);
        Task<dynamic> findAll();
    }
}
