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
