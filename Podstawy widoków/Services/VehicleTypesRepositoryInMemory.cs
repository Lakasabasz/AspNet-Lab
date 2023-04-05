using Podstawy_widoków.Models;

namespace Podstawy_widoków.Services;

public class VehicleTypesRepositoryInMemory: IRepository<VehicleType>
{
    private readonly ApplicationDatabaseInMemory _db;

    public VehicleTypesRepositoryInMemory(ApplicationDatabaseInMemory db)
    {
        _db = db;
    }
    
    public VehicleType? Get(Guid id) => null;

    public IQueryable<VehicleType> GetAll() => _db.VehicleTypes;

    public void Add(VehicleType value) => _db.Add(value);

    public void SaveChanges() => _db.SaveChanges();

    public void Remove(VehicleType? value)
    {
        if (value is not null) _db.Remove(value);
    }
}