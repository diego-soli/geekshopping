using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _repository;
        public CartController(ICartRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindbyId(string id)
        {
            var cart = await _repository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }        
        
        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _repository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _repository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _repository.RemoveFromCart(id);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]        //CartHeaderVO
        public async Task<ActionResult<CartHeaderVO>> ApplyCoupon(CartHeaderVO vo) //CartVO
        {
            var status = await _repository.ApplyCoupon(vo.UserId, vo.CouponCode); //(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
            if (!status) return NotFound();
            return Ok(status);
        }
        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> ApplyCoupn(string userId)
        {
            var status = await _repository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }
    };  
    
}