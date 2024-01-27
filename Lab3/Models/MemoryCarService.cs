﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Lab3.Models;

public class MemoryCarService : ICarService
{
    private Dictionary<int, Car> _items = new Dictionary<int, Car>();

    private IDateTimeProvider _dateTimeProvider;

    public MemoryCarService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public int Add(Car item)
    {
        int id = _items.Keys.Count != 0 ? _items.Keys.Max() : 0;
        item.Id = id + 1;
        item.PublicationDate = _dateTimeProvider.dateNow();
        _items.Add(item.Id, item);
        return item.Id;
    }

    public void Delete(int id)
    {
        _items.Remove(id);
    }

    public List<OrganizationEntity> FindAllOrganizations()
    {
        throw new NotImplementedException();
    }

    public PagingList<Car> FindPage(int page, int size)
    {
        throw new NotImplementedException();
    }

    public List<Car> FindAll()
    {
        return _items.Values.ToList();
    }

    public Car? FindById(int id)
    {
        return _items.ContainsKey(id) ? _items[id] : null;
    }

    public void Update(Car model)
    {
        if (_items.ContainsKey(model.Id))
        {
            model.PublicationDate = _items[model.Id].PublicationDate;
            _items[model.Id] = model;
        }
    }
}
