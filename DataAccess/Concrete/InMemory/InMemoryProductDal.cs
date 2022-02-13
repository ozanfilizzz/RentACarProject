using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{   

    public class InMemoryCarDal : ICarDal
    {

        List<Car> _cars;

        public InMemoryCarDal()
        {
            // Oracle,Sql Server, Mongodb...

            _cars = new List<Car>
            {
                new Car {Id= 1 , BrandId= 1, ColorId=1, DailyPrice= 900, Description = "Audi R7 GT Fastback", ModelYear= 2022},
                new Car {Id= 2 , BrandId= 1, ColorId=2, DailyPrice= 600, Description = "Audi R5", ModelYear= 2020},
                new Car {Id= 3 , BrandId= 2, ColorId=3, DailyPrice= 200, Description = "Porsche 911 ", ModelYear= 2019},
                new Car {Id= 4 , BrandId= 2, ColorId=4, DailyPrice= 500, Description = "Porsche 918", ModelYear= 2021},
                new Car {Id= 5 , BrandId= 2, ColorId=4, DailyPrice= 700, Description = "Porsche 920", ModelYear= 2022}

            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(p => p.Id == car.Id);
            _cars.Remove(carToDelete);

        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(p => p.Id == p.Id);

            carToUpdate.Id = car.Id;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;

        }
    }
}
