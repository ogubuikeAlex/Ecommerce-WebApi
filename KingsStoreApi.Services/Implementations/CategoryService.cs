using AutoMapper;
using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Helpers.Implementations;
using KingsStoreApi.Model.DataTransferObjects.CategoryServicesDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using System.Threading.Tasks;

namespace KingsStoreApi.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IServiceFactory serviceFactory, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _serviceFactory = serviceFactory;
            _repository = unitOfWork.GetRepository<Category>();
            _mapper = mapper;
        }
        public async Task<ReturnModel> CreateCategory(CreateCategoryDTO model)
        {
            var newCategory = _mapper.Map<Category>(model);

            var exactReplicaOfNewCategory = _repository.GetSingleByCondition(c => c.Summary == newCategory.Summary && c.Title == newCategory.Title);
            var categoryWithSameSummaryOrTitle = _repository.GetSingleByCondition(c => c.Summary == newCategory.Summary || c.Title == newCategory.Title);

            string message;

            if (exactReplicaOfNewCategory is not null || categoryWithSameSummaryOrTitle is not null)
            {
                message = exactReplicaOfNewCategory is null ? "A category with either the same title or summary you provided already exists." : "An exact replica of the category you are trying to create already exists!!";
                return new ReturnModel { Message = message, Success = false };
            }
            await _repository.AddAsync(newCategory);
            return new ReturnModel { Success = true, Message = $"Category {newCategory.Title} added successfully" };
        }

        public ReturnModel GetAllCategories()
        {
            var categories = _repository.GetAllByCondition();

            if (categories is null)
                return new ReturnModel { Message = "No categories in database", Success = false};

            return new ReturnModel { Object = categories, Success = true };
        }

        public ReturnModel GetCategory(string categoryName)
        {
            var category = _repository.GetSingleByCondition(c => c.Title == categoryName);

            if (category is null)
                return new ReturnModel { Message = "No such category in database", Success = false };

            return new ReturnModel { Success = true, Message = "", Object = category };
        }

        public async Task<ReturnModel> ToggleSoftDeleteCategory(string title)
        {
            var category = _repository.GetSingleByCondition(c => c.Title == title);

            var result = await _repository.ToggleSoftDeleteAsync(category);

            return new ReturnModel
            {
                Success = result,
                Message = result ? "Category has been set to deleted" : "Category is Now Undeleted",
            };
        }

        public async Task<ReturnModel> UpdateCategorySummary(UpdateCategoryDTO model)
        {           
                var category = _repository.GetSingleByCondition(p => p.Title == model.Name);

                if (category is null)
                    return new ReturnModel { Message = "Category does not exist", Success = false };

                category.Summary = model.NewValue;
                await _repository.UpdateDBAsync();

                return new ReturnModel { Message = $"Category:\n {category.Title} Summary Updated Successfully", Success = true };           
        }

        public async Task<ReturnModel> UpdateCategoryTitle(UpdateCategoryDTO model)
        {
            var category = _repository.GetSingleByCondition(p => p.Title == model.Name);

            if (category is null)
                return new ReturnModel { Message = "Category does not exist", Success = false };

            category.Title = model.NewValue;
            await _repository.UpdateDBAsync();

            return new ReturnModel { Message = $"Category:\n {category.Title} Updated Successfully", Success = true };
        }      
    }
}
