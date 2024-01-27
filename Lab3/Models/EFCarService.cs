using Data.Entities;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Lab3.Models
{
    public class EFCarService : ICarService
    {
        private AppDbContext _context;

        public EFCarService(AppDbContext context)
        {
            _context = context;
        }

        public PagingList<Car> FindPage(int page, int size, List<Car> posts)
        {
            return PagingList<Car>.Create(
                (p, s) => posts.OrderBy(c => c.PublicationDate).Skip((p - 1) * s).Take(s)
                , page, size, posts.Count()
            );
        }

        public int Add(Car model)
        {
            var e = _context.Cars.Add(CarMapper.ToEntity(model));
            _context.SaveChanges();
            return e.Entity.CarId;
        }

        public void Delete(int id)
        {
            CarEntity? find = _context.Cars.Find(id);
            if (find is not null)
            {
                _context.Cars.Remove(find);
                _context.SaveChanges();
            }
        }

        public List<Car> FindAll()
        {
            return _context.Cars.Select(e => CarMapper.FromEntity(e)).ToList(); ;
        }
            
        public Car? FindById(int id)
        {
            return CarMapper.FromEntity(_context.Cars.Find(id));
        }

        public void Update(Car model)
        {
            var entity = CarMapper.ToEntity(model);
            _context.Update(entity);
            _context.SaveChanges();
        }

        public List<OrganizationEntity> FindAllOrganizations()
        {
            return _context.Organizations.ToList();
        }

        public PagingList<Car> FindPage(int page, int size)
        {
            return PagingList<Car>.Create(
                (p, s) => _context.Cars
                    .OrderBy(c => c.Model)
                    .Skip((p - 1) * s)
                    .Take(s)
                    .Select(CarMapper.FromEntity)
                    .ToList()
                , page, size, _context.Cars.Count()
            );
        }
    }
}
