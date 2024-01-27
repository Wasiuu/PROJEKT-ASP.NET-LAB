using Data.Entities;

namespace Lab3.Models
{
    public class CarMapper
    {
        public static Car FromEntity(CarEntity entity)
        {
            return new Car()
            {
                Id = entity.CarId,
                Model = entity.Model,
                Manufacturer = entity.Manufacturer,
                EngineCapacity = entity.EngineCapacity,
                Power = entity.Power,
                EngineType = entity.EngineType,
                RegistrationNumber = entity.RegistrationNumber,
                Owner = entity.Owner
            };
        }

        public static CarEntity ToEntity(Car model)
        {
            return new CarEntity()
            {
                CarId = model.Id,
                Model = model.Model,
                Manufacturer = model.Manufacturer,
                EngineCapacity = model.EngineCapacity,
                Power = model.Power,
                EngineType = model.EngineType,
                RegistrationNumber = model.RegistrationNumber,
                Owner = model.Owner
            };
        }
    }
}
