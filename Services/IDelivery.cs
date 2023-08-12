namespace PlantNestBackEnd.Services
{
    public interface IDelivery
    {
        Task<dynamic> findByIdOrder(int idOrder);
    }
}
