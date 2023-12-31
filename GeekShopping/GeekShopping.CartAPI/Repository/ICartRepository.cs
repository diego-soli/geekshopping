﻿using GeekShopping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartVO> FindCartByUserId(string userid);
        Task<CartVO> SaveOrUpdateCart(CartVO cart);
        Task<bool> RemoveFromCart(long cartDetaisId);
        Task<bool> ApplyCoupon(string userId, string  couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);

    }
}
