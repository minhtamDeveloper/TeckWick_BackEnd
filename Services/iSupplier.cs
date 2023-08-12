namespace PlantNestBackEnd.Services;

public interface ISupplier
{
    public dynamic showAll();

    public dynamic SearchId(int id);
    public dynamic Search(string keyword);
}
