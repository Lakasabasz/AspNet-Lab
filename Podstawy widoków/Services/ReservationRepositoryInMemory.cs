using Podstawy_widoków.Models;

namespace Podstawy_widoków.Services;

public class ReservationRepositoryInMemory: IRepository<Reservation>
{
    private readonly ApplicationDatabaseInMemory _db;
    
    public ReservationRepositoryInMemory(ApplicationDatabaseInMemory db) => _db = db;

    public Reservation? Get(Guid id) => _db.Reservations.FirstOrDefault(x => x.Id == id);

    public IQueryable<Reservation> GetAll() => _db.Reservations;

    public void Add(Reservation value) => _db.Reservations.Add(value);

    public void SaveChanges() => _db.SaveChanges();
    public void Remove(Reservation? value)
    {
        if (value is not null) _db.Reservations.Remove(value);
    }
}