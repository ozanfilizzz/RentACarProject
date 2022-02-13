using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarDbContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RentACarDbContext context = new RentACarDbContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join color in context.Colors on car.ColorId equals color.Id
                             join category in context.Categories on car.CategoryId equals category.Id
                             select new CarDetailDto
                             {
                                 CarId = car.Id,
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 CategoryName = category.CategoryName,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice
                             };

                return result.ToList();


            }
        }
    }
}
