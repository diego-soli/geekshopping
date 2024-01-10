using GeekShopping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        private ICouponRepository _couponRepository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository cartRepository, ICouponRepository couponRepository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _cartRepository = cartRepository;
            _couponRepository = couponRepository;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindbyId(string id)
        {
            var cart = await _cartRepository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }        
        
        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }
        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _cartRepository.RemoveFromCart(id);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]        //CartHeaderVO
        public async Task<ActionResult<CartHeaderVO>> ApplyCoupon(CartHeaderVO vo) //CartVO
        {
            var status = await _cartRepository.ApplyCoupon(vo.UserId, vo.CouponCode); //(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
            if (!status) return NotFound();
            return Ok(status);
        }
        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(string userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }
        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            //string token = Request.Headers["Authorization"]; versao NET 5.0
            var token = await HttpContext.GetTokenAsync("access_token"); //versao NET 6.0 >
            if (vo?.UserId == null) return BadRequest();
            var cart = await _cartRepository.FindCartByUserId(vo.UserId);
            if (cart == null) return NotFound();
            if(!string.IsNullOrWhiteSpace(vo.CouponCode))
            {
                CouponVO coupon = await _couponRepository.GetCoupon(vo.CouponCode, token);
                if (vo.DiscountAmount != coupon.DiscountAmount)
                { //precondition failed, mudou as condicoes entre a primeira requisicao e a confirmacao de compra.
                    return StatusCode(412);
                    
                }
            }

            vo.CartDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;
            
            _rabbitMQMessageSender.SendMessage(vo, "checkoutqueue");
            await _cartRepository.ClearCart(vo.UserId);
            return Ok(vo);
        }
    }    
}