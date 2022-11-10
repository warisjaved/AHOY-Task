using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using AHOY_Backend_Task.BAL;
//using System.Web.Http;

namespace AHOY_Backend_Task.Controllers
{
    [Route("api")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHotelBal _hotelBal;

        private static readonly string[] Summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<HotelController> _logger;

        public HotelController(ILogger<HotelController> logger, IConfiguration configuration, IHotelBal hotelBal)
        {
            _logger = logger;
            _configuration = configuration;
            _hotelBal = hotelBal;
        }

        [HttpGet]
        [Route("Listing")]
        public IActionResult GetHotelListing(string filterName, DateTime? filterFromDate, DateTime? filterToDate)
        {
            try
            {
                var hotelListing = _hotelBal.GetHotelListing(filterName, filterFromDate, filterToDate);

                var _response = new
                {
                    status = "Success",
                    message = "",
                    hotels = hotelListing
                };

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Ok(new
                {
                    status = "Failure",
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetHotel")]
        public IActionResult GetHotel(int hotelId)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection"));
                var hotel = _hotelBal.GetHotel(hotelId);

                var _response = new
                {
                    status = "Success",
                    message = "",
                    hotels = hotel
                };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Ok(new
                {
                    status = "Failure",
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetHotelWothAvailableRooms")]
        public IActionResult GetHotelListingWithRooms(int filterHotelid, DateTime? filterFromDate, DateTime? filterToDate)
        {
            try
            {
                var hotelListing = _hotelBal.GetHotelWithAvailableRooms(filterHotelid, filterFromDate, filterToDate);
                var _response = new
                {
                    status = "Success",
                    message = "",
                    hotels = hotelListing
                };

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Ok(new
                {
                    status = "Failure",
                    message = ex.Message
                });
            }
        }


        [HttpPost]
        [Route("MakeBooking")]
        public IActionResult CreateBooking(int HotelId, int RoomId, DateTime StartDate, DateTime EndDate, int CreatedBy)
        {
            try
            {
                var insertedRow = _hotelBal.CreateBooking(HotelId, RoomId, StartDate, EndDate, CreatedBy);
                if (insertedRow != 0)
                {
                    var _response = new
                    {
                        status = "Success",
                        message = "",
                    };
                    return Ok(_response);
                }
                else
                {
                    var _response = new
                    {
                        status = "Failure",
                        message = "",
                    };
                    return Ok(_response);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return Ok(new { status = "Failure", message = ex.Message });
            }


        }
    }
}
