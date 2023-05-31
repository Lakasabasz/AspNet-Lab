using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.Models;

namespace Podstawy_widoków.Services;

public class ReservationRepositoryInMemory: IRepository<Reservation>
{
    private readonly ApplicationDatabaseInMemory _db;
    
    public ReservationRepositoryInMemory(ApplicationDatabaseInMemory db) => _db = db;

    public Reservation? Get(Guid id) => _db.Reservations.FirstOrDefault(x => x.Id == id);

    public IQueryable<Reservation> GetAll() => _db.Reservations;
    public void Add(Reservation value)
    {
        var reserved = _db.Reservations
            .Include(x => x.Vehicle)
            .Where(x => x.Vehicle.Id == value.Vehicle.Id)
            .Any(x => (value.ReservationDate < x.ReservationDate && x.ReservationDate < value.ReservationExpire) ||
                      (value.ReservationDate < x.ReservationExpire && x.ReservationExpire < value.ReservationExpire));
        if (reserved) throw new ArgumentException("Invalid date");
        _db.Reservations.Add(value);
    }

    public void SaveChanges() => _db.SaveChanges();
    public void Remove(Reservation? value)
    {
        if (value is not null) _db.Reservations.Remove(value);
    }
}