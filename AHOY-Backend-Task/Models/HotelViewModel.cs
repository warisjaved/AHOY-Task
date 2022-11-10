using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHOY_Backend_Task.Models
{

    public class HotelViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string  Description { get; set; }

        public double AvgRatePerNight { get; set; }

        public double AverageRating { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int ReviewsCount { get; set; }

        public string ImagesPath { get;set;}

        public string AmenityText { get; set; }
        public List<HotelAmenities> Amenities { get; set; }

        public List<HotelRoom> Rooms { get; set; }

        public HotelViewModel() {
            Amenities = new List<HotelAmenities>();
            Rooms = new List<HotelRoom>();
        }
    }
     
    public class HotelRoom
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public string RoomNumber { get; set; }

        public int NumberofBeds { get; set; }

        public bool IsSmookingRoom { get; set; }

        public double Rate { get; set; }
    }

    public class HotelRating
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public double Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public string Message { get; set; }
    }

    public class HotelAmenities
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public string Amenity { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }
    }

    public class HotelBookings
    {
        public int Id { get; set; }

        public int HotelId { get; set; }
        
        public string HotelName { get; set; }

        public int RoomId { get; set; }

        public DateTime StartDatetime { get; set; }
        
        public DateTime EndDatetime { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
        
        public int CreatedBy { get; set; }

    }

    public class SearchHotelsRaw
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double AvgRatePerNight { get; set; }

        public double AverageRating { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public int ReviewsCount { get; set; }

        public string ImagesPath { get; set; }

        public string AmenityText { get; set; }

        public int RoomId { get; set; }

        public string RoomNumber { get; set; }

        public int NumberofBeds { get; set; }

        public bool IsSmookingRoom { get; set; }

        public double Rate { get; set; }
    }
}
