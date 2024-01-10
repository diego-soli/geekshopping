﻿using AutoMapper;
using GeekShopping.CartAPI.Data.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace GeekShopping.CartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client;

        public CouponRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponVO> GetCoupon(string couponCode, string token)
        {
            //"api/v1/coupon"
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"/api/v1/coupon/{couponCode}");//<CouponVO>
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK) return new CouponVO();
            return JsonSerializer.Deserialize<CouponVO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });



        }
    }
}
