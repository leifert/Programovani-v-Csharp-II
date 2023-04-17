using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Bike = WebApplication.Models.Bike;
using Rental = WebApplication.Models.Rental;
using Employee = WebApplication.Models.Employee;

namespace WebApplication.Services
{
    public class BikeService

    {
        public async void AddBike(Bike bike)
        {
            await DataMapper.Insert(bike);
        }

        public async void DeleteBike(Bike bike)
        {
            await DataMapper.Delete(bike);
        }
        public async void UpdateBike(Bike bike)
        {
            await DataMapper.Update(bike);
        }
        public List<Bike> SelectBike()
        {
            List<Bike> bikes = new List<Bike>(DataMapper.Select<Bike>().Result);
            return bikes;
        }

        public Bike SelectBikeById(Bike bike)
        {
            Bike b = DataMapper.SelectById(bike).Result;
            return b;
        }

        public async void AddRental(Rental rental)
        {
            await DataMapper.Insert(rental);
        }

        public async void DeleteRental(Rental rental)
        {
            await DataMapper.Delete(rental);
        }
        public async void UpdateRental(Rental rental)
        {
            await DataMapper.Update(rental);
        }
        public List<Rental> SelectRentals()
        {
            List<Rental> rentals = new List<Rental>(DataMapper.Select<Rental>().Result);
            return rentals;
        }

        public Rental SelectRentalById(Rental rental)
        {
            Rental rent = DataMapper.SelectById(rental).Result;
            return rent;
        }

        public bool Login(Employee employee)
        {
            return DataMapper.Login(employee).Result;
        }
    }
}
