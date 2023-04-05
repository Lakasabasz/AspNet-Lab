using Podstawy_widoków.Models;

namespace Podstawy_widoków.Services;

public class LocalizationRepositoryInMemory: IRepository<Localization>
{
    private readonly ApplicationDatabaseInMemory _db;

    public LocalizationRepositoryInMemory(ApplicationDatabaseInMemory db)
    {
        _db = db;
        _db.Database.EnsureCreated();
    }

    public Localization? Get(Guid id) => _db.Localizations.FirstOrDefault(x => x.Id == id);

    public IQueryable<Localization> GetAll() => _db.Localizations;

    public void Add(Localization value) => _db.Localizations.Add(value);

    public void SaveChanges() => _db.SaveChanges();
    public void Remove(Localization? value)
    {
        if (value != null) _db.Localizations.Remove(value);
    }
}