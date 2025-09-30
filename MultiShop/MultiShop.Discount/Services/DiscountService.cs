using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services;

public class DiscountService : IDiscountService
{
    private readonly DapperContext _context;

    public DiscountService(DapperContext context)
    {
        _context = context;
    }

    public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto)
    {
        string query = "INSERT INTO Coupons (Code, Rate, IsActive, ValidDate) VALUES (@Code, @Rate, @IsActive, @ValidDate)";
        var parameters = new DynamicParameters();
        parameters.Add("Code", createCouponDto.Code);
        parameters.Add("Rate", createCouponDto.Rate);
        parameters.Add("IsActive", createCouponDto.IsActive);
        parameters.Add("ValidDate", createCouponDto.ValidDate);
        
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task DeleteDiscountCouponAsync(int id)
    {
        string query = "DELETE FROM Coupons WHERE CouponId = @couponId";
        var parameters = new DynamicParameters();
        parameters.Add("couponId", id);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
    {
        string query = "SELECT * FROM Coupons";
        using (var connection = _context.CreateConnection())
        {
            var coupons = await connection.QueryAsync<ResultDiscountCouponDto>(query);
            return coupons.ToList();
        }
    }

    public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int id)
    {
        string query = "SELECT * FROM Coupons WHERE CouponId = @couponId";
        var parameters = new DynamicParameters();
        parameters.Add("couponId", id);
        using (var connection = _context.CreateConnection())
        {
            var coupon = await connection.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query, parameters);
            return coupon;
        }
    }

    public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateCouponDto)
    {
        string query = "UPDATE Coupons SET Code = @Code, Rate = @Rate, IsActive = @IsActive, ValidDate = @ValidDate WHERE CouponId = @CouponId";
        var parameters = new DynamicParameters();
        parameters.Add("CouponId", updateCouponDto.CouponId);
        parameters.Add("Code", updateCouponDto.Code);
        parameters.Add("Rate", updateCouponDto.Rate);
        parameters.Add("IsActive", updateCouponDto.IsActive);
        parameters.Add("ValidDate", updateCouponDto.ValidDate);
        
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
