using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountsController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCoupon()
    {
        var coupons = await _discountService.GetAllDiscountCouponAsync();
        return Ok(coupons);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdCoupon(int id)
    {
        var coupon = await _discountService.GetByIdDiscountCouponAsync(id);
        return Ok(coupon);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCoupon([FromBody] CreateDiscountCouponDto createCouponDto)
    {
        await _discountService.CreateDiscountCouponAsync(createCouponDto);
        return Ok("Kupon başarıyla oluşturuldu.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCoupon([FromBody] UpdateDiscountCouponDto updateCouponDto)
    {
        await _discountService.UpdateDiscountCouponAsync(updateCouponDto);
        return Ok("Kupon başarıyla güncellendi.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoupon(int id)
    {
        await _discountService.DeleteDiscountCouponAsync(id);
        return Ok("Kupon başarıyla silindi.");
    }

}
