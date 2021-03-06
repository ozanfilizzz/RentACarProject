using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Performance;
using Core.Aspect.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {

        ICarDal _carDal;
        ICategoryService _categoryService;

        public CarManager(ICarDal carDal, ICategoryService categoryService)
        {
            _carDal = carDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(ChekIfCarNameExists(car.CarName),
                ChekIfCarCountOfCategoryCorrect(car.CategoryId), CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _carDal.Add(car);

            return new SuccessResult(CarMessages.CarAdded);

        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);

            return new SuccessResult(CarMessages.CarDeleted);

        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Car>>(CarMessages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), CarMessages.CarsListed);
        }

        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.DailyPrice >= min && p.DailyPrice <= max));
        }

        public IDataResult<Car> GetById(int id)
        {

            return new SuccessDataResult<Car>(_carDal.Get(p => p.Id == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == id));
        }

        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);

            return new SuccessResult(CarMessages.CarUpdated);
        }

        public IDataResult<List<Car>> GetCarsByCategoryId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.CategoryId == id));
        }

        #region BusinessRulesParticle
        private IResult ChekIfCarNameExists(string carName)
        {
            var result = _carDal.GetAll(p => p.CarName == carName).Any();
            if (result)
            {
                return new ErrorResult(CarMessages.CarNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult ChekIfCarCountOfCategoryCorrect(int id)
        {
            var result = _carDal.GetAll(p => p.CategoryId == id).Count();
            if (result >= 10)
            {
                return new ErrorResult(CarMessages.CarCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count >= 9)
            {
                return new ErrorResult(CarMessages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }
        #endregion

      
    }
}
