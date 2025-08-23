using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }
        /// <summary>
        /// Retrieves all coupons from the database.
        /// </summary>
        /// <returns>
        /// A <see cref="ResponseDto"/> containing a list of <see cref="CouponDto"/> objects if successful; 
        /// otherwise, an error message and <c>IsSuccess</c> set to <c>false</c>.
        /// </returns>
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objects = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objects);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon objects = _db.Coupons.First(u=>u.CouponId==id);
                _response.Result =  _mapper.Map<CouponDto>(objects);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon objects = _db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if(objects==null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = _mapper.Map<CouponDto>(objects);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        /// <summary>
        /// Creates a new coupon in the database.
        /// </summary>
        /// <param name="couponDto">
        /// The <see cref="CouponDto"/> object containing the details of the coupon to create.
        /// </param>
        /// <returns>
        /// A <see cref="ResponseDto"/> containing the created <see cref="CouponDto"/> if successful;
        /// otherwise, an error message and <c>IsSuccess</c> set to <c>false</c>.
        /// </returns>
        [HttpPost]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        /// <summary>
        /// Updates an existing coupon in the database.
        /// </summary>
        /// <param name="couponDto">
        /// The <see cref="CouponDto"/> object containing the updated details of the coupon.
        /// </param>
        /// <returns>
        /// A <see cref="ResponseDto"/> containing the updated <see cref="CouponDto"/> if successful;
        /// otherwise, an error message and <c>IsSuccess</c> set to <c>false</c>.
        /// </returns>
        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                _db.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        /// <summary>
        /// Deletes a coupon from the database by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the coupon to delete.
        /// </param>
        /// <returns>
        /// A <see cref="ResponseDto"/> indicating whether the deletion was successful. 
        /// If an error occurs, <c>IsSuccess</c> is set to <c>false</c> and <c>Message</c> contains the error details.
        /// </returns>
        [HttpDelete]
        public ResponseDto Delete(int id)
        {
            try
            {
                var coupon = _db.Coupons.First(u=>u.CouponId==id);
                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
