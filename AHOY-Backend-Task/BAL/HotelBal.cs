using AHOY_Backend_Task.DAL;
using AHOY_Backend_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOY_Backend_Task.BAL
{
    public class HotelBal : IHotelBal
    {
        private IHotelDal _hoteldal;

        public HotelBal(IHotelDal hoteldal) 
        {
            _hoteldal = hoteldal;
        }

        public List<HotelViewModel> GetHotelListing(string filterName, DateTime? filter_FromDate, DateTime? filter_ToDate)
        { 
            var result = _hoteldal.GetHotelListing(filterName,filter_FromDate,filter_ToDate).GroupBy(h => new
            {
                h.Id,
                h.Name,
                h.Description,
                h.AvgRatePerNight,
                h.AverageRating,
                h.Latitude,
                h.Longitude,
                h.ReviewsCount,
                h.ImagesPath
            }).Select(model => new HotelViewModel()
            {
                Id = model.Key.Id,
                Name = model.Key.Name,
                Description = model.Key.Description,
                AvgRatePerNight = model.Key.AvgRatePerNight,
                AverageRating = model.Key.AverageRating,
                Latitude = model.Key.Latitude,
                Longitude = model.Key.Longitude,
                ReviewsCount = model.Key.ReviewsCount,
                ImagesPath = model.Key.ImagesPath,
                Amenities = model.Select(a => new HotelAmenities()
                {
                    Amenity = a.AmenityText
                }).ToList()//,
                //Rooms = model.Select(r => new HotelRoom() {
                //    Id = r.Id,
                //    RoomNumber = r.RoomNumber,
                //    Rate = r.Rate,
                //    IsSmookingRoom = r.IsSmookingRoom,
                //    NumberofBeds = r.NumberofBeds,
                //    HotelId= r.Id
                //}).ToList()
            });
            return result.ToList();        
        }

        public List<HotelViewModel> GetHotelWithAvailableRooms(int hotelId, DateTime? filter_FromDate, DateTime? filter_ToDate)
        {
            var result = _hoteldal.GetHotelWithAvailableRooms(hotelId, filter_FromDate, filter_ToDate).GroupBy(h => new
            {
                h.Id,
                h.Name,
                h.Description,
                h.AvgRatePerNight,
                h.AverageRating,
                h.Latitude,
                h.Longitude,
                h.ReviewsCount,
                h.ImagesPath
            }).Select(model => new HotelViewModel()
            {
                Id = model.Key.Id,
                Name = model.Key.Name,
                Description = model.Key.Description,
                AvgRatePerNight = model.Key.AvgRatePerNight,
                AverageRating = model.Key.AverageRating,
                Latitude = model.Key.Latitude,
                Longitude = model.Key.Longitude,
                ReviewsCount = model.Key.ReviewsCount,
                ImagesPath = model.Key.ImagesPath,
                Amenities = model.Select(a => new HotelAmenities()
                {
                    Amenity = a.AmenityText
                }).ToList(),
                Rooms = model.Select(r => new HotelRoom()
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    Rate = r.Rate,
                    IsSmookingRoom = r.IsSmookingRoom,
                    NumberofBeds = r.NumberofBeds,
                    HotelId = r.Id
                }).ToList()
            });
            return result.ToList();
        }

        public HotelViewModel GetHotel(int hotelId)
        {
            return _hoteldal.GetHotel(hotelId);
        }

        public int CreateBooking(int HotelId, int RoomId, DateTime StartDate, DateTime EndDate, int CreatedBy) 
        {
            return _hoteldal.CreateBooking(HotelId, RoomId, StartDate, EndDate, CreatedBy);
        }
    }

    public interface IHotelBal 
    {
        public List<HotelViewModel> GetHotelListing(string filterName, DateTime? filter_FromDate, DateTime? filter_ToDate);
        public List<HotelViewModel> GetHotelWithAvailableRooms(int hotelId, DateTime? filter_FromDate, DateTime? filter_ToDate);
        public HotelViewModel GetHotel(int hotelId);
        public int CreateBooking(int HotelId, int RoomId, DateTime StartDate, DateTime EndDate, int CreatedBy);
    }

}
