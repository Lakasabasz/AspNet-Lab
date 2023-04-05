using System.Collections;
using Podstawy_widoków.Models;

namespace Podstawy_widoków.Services;

public interface IRepository<T>
{
    T? Get(Guid id);
    IQueryable<T> GetAll();
    void Add(T value);
    void SaveChanges();
    void Remove(T? value);
}