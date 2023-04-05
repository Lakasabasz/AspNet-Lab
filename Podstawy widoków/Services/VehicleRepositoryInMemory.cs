using Podstawy_widoków.Models;

namespace Podstawy_widoków.Services;

public class VehicleRepositoryInMemory: IRepository<Vehicle>
{
    private readonly ApplicationDatabaseInMemory _db;

    public VehicleRepositoryInMemory(ApplicationDatabaseInMemory db)
    {
        _db = db;
        _db.Database.EnsureCreated();
    }

    public Vehicle? Get(Guid id) => _db.Vehicles.FirstOrDefault(x => x.Id == id);
    public IQueryable<Vehicle> GetAll() => _db.Vehicles;

    public void Add(Vehicle value) => _db.Vehicles.Add(value);

    public void SaveChanges() => _db.SaveChanges();
    public void Remove(Vehicle? value)
    {
        if (value is not null) _db.Vehicles.Remove(value);
    }
}