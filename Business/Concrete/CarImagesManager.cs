using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarImagesManager : ICarImagesService
    {
        ICarImagesDal _carImagesDal;
        IFileHelperService _fileHelperService;

        public CarImagesManager(ICarImagesDal carImagesDal, IFileHelperService fileHelperService)
        {
            _carImagesDal = carImagesDal;
            _fileHelperService = fileHelperService;
        }

        public IResult Add(IFormFile file, CarImages carImages)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageLimit(carImages.CarId));
            if (result != null)
            {
                return result;
            }

            carImages.ImagePath = _fileHelperService.Upload(file, PathConstants.ImagesPath);
            carImages.Date = DateTime.Now;
            _carImagesDal.Add(carImages);
            return new SuccessResult("Resim başarıyla yüklendi.");
        }

        public IResult Delete(CarImages carImages)
        {
            _fileHelperService.Delete(PathConstants.ImagesPath + carImages.ImagePath);
            _carImagesDal.Delete(carImages);
            return new SuccessResult("Resim başarıyla silindi.");
        }

        public IDataResult<List<CarImages>> GetAll()
        {
            return new SuccessDataResult<List<CarImages>>(_carImagesDal.GetAll());
        }

        public IDataResult<List<CarImages>> GetByCarId(int carId)
        {
            var result = BusinessRules.Run(CheckCarImage(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarImages>>(GetDefaultImage(carId).Data);
            }
            return new SuccessDataResult<List<CarImages>>(_carImagesDal.GetAll(p => p.CarId == carId));
        }

        public IDataResult<CarImages> GetByImageId(int imageId)
        {
            return new SuccessDataResult<CarImages>(_carImagesDal.Get(p => p.CarImageId == imageId));
        }

        public IResult Update(IFormFile file, CarImages carImages)
        {
            carImages.ImagePath = _fileHelperService.Update(file, PathConstants.ImagesPath + carImages.ImagePath, PathConstants.ImagesPath);
            _carImagesDal.Update(carImages);
            return new SuccessResult("Resim başarıyla güncellendi.");
        }

        private IResult CheckIfCarImageLimit(int carId)
        {
            var result = _carImagesDal.GetAll(p => p.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IDataResult<List<CarImages>> GetDefaultImage(int carId)
        {
            List<CarImages> carImages = new List<CarImages>();
            carImages.Add(new CarImages { CarId = carId, Date = DateTime.Now, ImagePath = "DefaultImage.jpg" });
            return new SuccessDataResult<List<CarImages>>(carImages);
        }

        private IResult CheckCarImage(int carId)
        {
            var result = _carImagesDal.GetAll(p => p.CarId == carId).Count;
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
