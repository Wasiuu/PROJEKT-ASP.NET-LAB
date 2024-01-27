using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Lab3.Models
{
    public interface ICarService
    {
        int Add(Car model);
        Car? FindById(int id);
        List<Car> FindAll();
        void Delete(int id);
        void Update(Car model);
        List<OrganizationEntity> FindAllOrganizations();
        PagingList<Car> FindPage(int page, int size);
    }
}
