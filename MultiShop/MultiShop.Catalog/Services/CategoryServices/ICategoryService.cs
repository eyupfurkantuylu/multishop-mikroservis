using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Services.CategoryServices;

public interface ICategoryService
{
    Task<List<ResultCategoryDto>> GetAllCategoryAsync();
    Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
    Task DeleteCategoryAsync(string categoryId);
    Task<GetByIdCategoryDto> GetByIdCategoryAsync(string categoryId);
}
