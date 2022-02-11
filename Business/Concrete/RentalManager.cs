using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            _rentalDal.Add(rental);

            return new SuccessResult(RentalMessages.RentalAdded);
        }

        public IResult Delete(Rental rental)
        {
            
            if (rental.ReturnDate != null)
            {
                _rentalDal.Delete(rental);

                return new SuccessResult(RentalMessages.RentalDeleted);
            }

            return new ErrorResult();
           
        }

        public IDataResult<List<Rental>> GetAll()
        {
            if (DateTime.Now.Hour == 17)
            {
                return new ErrorDataResult<List<Rental>>(CarMessages.MaintenanceTime);
            }
          
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), RentalMessages.RentalsListed);
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(p => p.Id == id));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);

            return new SuccessResult(RentalMessages.RentalUpdated);
        }
    }
}
