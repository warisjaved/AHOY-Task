using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHOY_Backend_Task.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace AHOY_Backend_Task.DAL
{
    public class HotelDal : IHotelDal
    {
        private IConfiguration _config;

        public HotelDal(IConfiguration config)
        {
            _config = config;
        }
         
        public List<SearchHotelsRaw> GetHotelListing(string filterName, DateTime? filter_FromDate, DateTime? filter_ToDate)
        {
            var hotels = new List<SearchHotelsRaw>(); 
            try
            { 
                using (var con = new SqlConnection(_config.GetConnectionString("DevConnection")))
                {
                    //using (var sqlCommand  = new SqlCommand("[dbo].[GetHotelListing]", con))
                    using (var sqlCommand  = new SqlCommand("[dbo].[SearchHotels]", con))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Filter_Name", string.IsNullOrEmpty(filterName) ? (object)DBNull.Value : filterName);
                        sqlCommand.Parameters.AddWithValue("@Filter_HotelId", DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Filter_FromDate", filter_FromDate.HasValue  ? filter_FromDate.Value : (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Filter_ToDate", filter_ToDate.HasValue ? filter_ToDate.Value : (object)DBNull.Value);
                        con.Open();
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var hotelViewModel = new SearchHotelsRaw();
                                hotelViewModel.Id = Convert.ToInt32(reader["Id"]);
                                hotelViewModel.Name = Convert.ToString(reader["Name"]);
                                hotelViewModel.Description = Convert.ToString(reader["Description"]);
                                hotelViewModel.AvgRatePerNight = Convert.ToDouble(reader["RatePerNight"]);
                                hotelViewModel.AverageRating = Convert.ToDouble(reader["AverageRating"]);
                                hotelViewModel.Latitude = Convert.ToString(reader["Latitude"]);
                                hotelViewModel.Longitude = Convert.ToString(reader["Longitude"]);
                                hotelViewModel.ReviewsCount = Convert.ToInt32(reader["ReviewsCount"]);
                                hotelViewModel.ImagesPath = Convert.ToString(reader["ImagesPath"]);
                                hotelViewModel.AmenityText = Convert.ToString(reader["Amenity"]);
                                hotelViewModel.RoomNumber = Convert.ToString(reader["RoomNumber"]);
                                hotelViewModel.RoomId = Convert.ToInt32(reader["RoomId"]);
                                hotelViewModel.NumberofBeds = Convert.ToInt32(reader["NumberOfBeds"]);
                                hotelViewModel.IsSmookingRoom = Convert.ToBoolean(reader["IsSmokingRoom"]);
                                hotels.Add(hotelViewModel);
                            }
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hotels;
        }

        public List<SearchHotelsRaw> GetHotelWithAvailableRooms(int hotelId, DateTime? filter_FromDate, DateTime? filter_ToDate) 
        {

            var hotels = new List<SearchHotelsRaw>();
            try
            {
                using (var con = new SqlConnection(_config.GetConnectionString("DevConnection")))
                {
                    //using (var sqlCommand  = new SqlCommand("[dbo].[GetHotelListing]", con))
                    using (var sqlCommand = new SqlCommand("[dbo].[SearchHotels]", con))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Filter_HotelId", hotelId);
                        sqlCommand.Parameters.AddWithValue("@Filter_Name", DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Filter_FromDate", filter_FromDate.HasValue ? filter_FromDate.Value : (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Filter_ToDate", filter_ToDate.HasValue ? filter_ToDate.Value : (object)DBNull.Value);
                        con.Open();
                        using (var reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var hotelViewModel = new SearchHotelsRaw();
                                hotelViewModel.Id = Convert.ToInt32(reader["Id"]);
                                hotelViewModel.Name = Convert.ToString(reader["Name"]);
                                hotelViewModel.Description = Convert.ToString(reader["Description"]);
                                hotelViewModel.AvgRatePerNight = Convert.ToDouble(reader["RatePerNight"]);
                                hotelViewModel.AverageRating = Convert.ToDouble(reader["AverageRating"]);
                                hotelViewModel.Latitude = Convert.ToString(reader["Latitude"]);
                                hotelViewModel.Longitude = Convert.ToString(reader["Longitude"]);
                                hotelViewModel.ReviewsCount = Convert.ToInt32(reader["ReviewsCount"]);
                                hotelViewModel.ImagesPath = Convert.ToString(reader["ImagesPath"]);
                                hotelViewModel.AmenityText = Convert.ToString(reader["Amenity"]);
                                hotelViewModel.RoomNumber = Convert.ToString(reader["RoomNumber"]);
                                hotelViewModel.RoomId = Convert.ToInt32(reader["RoomId"]);
                                hotelViewModel.NumberofBeds = Convert.ToInt32(reader["NumberOfBeds"]);
                                hotelViewModel.IsSmookingRoom = Convert.ToBoolean(reader["IsSmokingRoom"]);
                                hotels.Add(hotelViewModel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hotels;

        }

        public HotelViewModel GetHotel(int hotelId)
        {
            var hotel = new HotelViewModel();
            using (var con = new SqlConnection(_config.GetConnectionString("DevConnection")))
            {
                using (var sqlCommand = new SqlCommand("[dbo].[GetHotel]", con))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@HotelId", hotelId);
                    con.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hotel.Id = Convert.ToInt32(reader["Id"]);
                            hotel.Name = Convert.ToString(reader["Name"]);
                            hotel.Description = Convert.ToString(reader["Description"]);
                            hotel.AvgRatePerNight = Convert.ToDouble(reader["RatePerNight"]);
                            hotel.AverageRating = Convert.ToDouble(reader["AverageRating"]);
                            hotel.Latitude = Convert.ToString(reader["Latitude"]);
                            hotel.Longitude = Convert.ToString(reader["Longitude"]);
                            hotel.ReviewsCount = Convert.ToInt32(reader["ReviewsCount"]);
                            hotel.ImagesPath = Convert.ToString(reader["ImagesPath"]);
                        }
                    }
                }
            }
            return hotel;
        }

        public int CreateBooking(int HotelId, int RoomId, DateTime StartDate, DateTime EndDate, int CreatedBy)
        {
            int i = 0;
            try
            {
                using (var con = new SqlConnection(_config.GetConnectionString("DevConnection")))
                {
                    using (var sqlCommand = new SqlCommand("[dbo].[CreateBooking]", con))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@HotelId", HotelId);
                        sqlCommand.Parameters.AddWithValue("@RoomId", RoomId);
                        sqlCommand.Parameters.AddWithValue("@StartDate", StartDate);
                        sqlCommand.Parameters.AddWithValue("@EndDate", EndDate);
                        sqlCommand.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        con.Open();
                        var obj = sqlCommand.ExecuteScalar();
                        if (obj != null)
                        {
                            i = Convert.ToInt32(obj.ToString());
                        }
                    }
                }
                return i;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        
        }
    }

    public interface IHotelDal
    {
        public List<SearchHotelsRaw> GetHotelListing(string filterName, DateTime? filter_FromDate, DateTime? filter_ToDate);
        public List<SearchHotelsRaw> GetHotelWithAvailableRooms(int hotelId, DateTime? filter_FromDate, DateTime? filter_ToDate);
        public HotelViewModel GetHotel(int hotelId);
        public int CreateBooking(int HotelId, int RoomId, DateTime StartDate, DateTime EndDate, int CreatedBy);
    }
}
