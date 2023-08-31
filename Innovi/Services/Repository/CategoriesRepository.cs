using Innovi.Models;
using Innovi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Innovi.Data;
using AutoMapper;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;
using Innovi.Models.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;

namespace Innovi.Services.Repository
{
    public class CategoriesRepository : ICategorieRepository 
    {
        private readonly EcommerceConetxt DbSet;
        private readonly IMapper _mapper;

        public CategoriesRepository(EcommerceConetxt dbContext, IMapper mapper)
        {
            DbSet = dbContext;
            _mapper = mapper;
        }

        //Create Category
        public async Task<CategoryDto> CreateAsync(CategoryDto category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            CategoryDto categorie = new CategoryDto
            {
                CreatedOn = DateTime.Now.AddHours(-2),
                Code = category.Code,
                NameEn = category.NameEn,
                NameAr = category.NameAr,
                IsDeleted = false,
                //ToVerf
                ParentCategoryId = category.ParentCategoryId,
                AddToHomePage = category.AddToHomePage,
                CreatedBy = category.CreatedBy,
                IsActive = category.IsActive,
                DisplayOrder = category.DisplayOrder,
                //ToVerf
            };

            var categ = _mapper.Map<Category>(categorie);

            await DbSet.Categories.AddAsync(categ);
            await DbSet.SaveChangesAsync();
            return categorie;
        }

        //Get All Category
        public async Task<ICollection<CategoryDto>> GetAllAsync()
        {
            try
            {
                var categori = await DbSet.Categories.Where(categ => categ.IsDeleted == false).OrderByDescending(categ => categ.CreatedOn).ToListAsync();
                var categories = categori.ToList();
                List<CategoryDto> cats = new List<CategoryDto>();
                foreach (var item in categories)
                {
                        CategoryDto c = new CategoryDto();
                        c = _mapper.Map<CategoryDto>(item);
                    if (item.ParentCategoryId != null)
                    {
                        var a = await GetByIdAsync((int)c.ParentCategoryId);
                        c.ParentCategory = a;
                    }
                    cats.Add(c);
                }
                if (cats.Count == 0)
                {
                    return null;
                }
                return cats;
            }
            catch(Exception e)
            {
                throw new Exception($"An error occurred: {e.Message}");
            }     
        }

        //Filter All Category
        public async Task<ICollection<CategoryDto>> GetAllAsync(CategoryDto filter)
        {
            var categoriesQuery = DbSet.Categories.Where(categ => categ.IsDeleted == false).OrderByDescending(r => r.CreatedOn).AsQueryable();
            if (filter.Code != null)
            {
                string upperCode = filter.Code.ToUpper();
                categoriesQuery = categoriesQuery.Where(c => c.Code.ToUpper() == upperCode);
            }
            if (filter.NameEn != null)
            {
                string upperNameEn = filter.NameEn.ToUpper();
                categoriesQuery = categoriesQuery.Where(c => c.NameEn.ToUpper() == upperNameEn);
            }
            if (filter.NameAr != null)
            {
                string upperNameAr = filter.NameAr.ToUpper();
                categoriesQuery = categoriesQuery.Where(c => c.NameAr.ToUpper() == upperNameAr);
            }
            
            var categories =  categoriesQuery.ToList();

            List<CategoryDto> cats = new List<CategoryDto>();

            foreach (var item in categories)
            {
                if (!item.IsDeleted)
                {
                    CategoryDto c = new CategoryDto();
                    c = _mapper.Map<CategoryDto>(item);
                    if (item.ParentCategoryId != null)
                    {
                        var a = await GetByIdAsync((int)c.ParentCategoryId);
                        c.ParentCategory = a;
                    }
                    cats.Add(c);
                }

            }
            if (cats.Count == 0)
            {
                return null;
            }
            return cats;
        }

        //Filter One Category
        public async Task<CategoryDto> GetAsync(CategoryDto filter)
        {
            var categoriesQuery = DbSet.Categories.AsQueryable();
            if (filter.Code != null)
            {
                string upperCode = filter.Code.ToUpper();
                categoriesQuery = categoriesQuery.Where(c => c.Code.ToUpper() == upperCode);
            }
            if (filter.NameEn != null)
            {
                string upperNameEn = filter.NameEn.ToUpper();
                categoriesQuery = categoriesQuery.Where(c => c.NameEn.ToUpper() == upperNameEn);
            }
            if (filter.NameAr != null)
            {
                string upperNameAr = filter.NameAr.ToUpper();
                categoriesQuery = categoriesQuery.Where(c => c.NameAr.ToUpper() == upperNameAr);
            }
            var categories =  categoriesQuery.FirstOrDefault();
            if (!categories.IsDeleted) {
                var categ = _mapper.Map<CategoryDto>(categories);
                if (categories.ParentCategoryId != null)
                {
                    var a = await GetByIdAsync((int)categ.ParentCategoryId);
                    categ.ParentCategory = a;
                }
                return categ;
            }
            return null;
        }
        //Filter With Pagination
        public async Task<CountListData<CategoryDto>> GetWithPagination(CategoryFilterDto PaginationFilter)
        {
            var categoriesByPage = await DbSet.Categories.Where(categ => categ.IsDeleted == false).ToListAsync();
            var categories = categoriesByPage.ToList();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Code", PaginationFilter.Code);
            dic.Add("NameAr", PaginationFilter.NameAr);
            dic.Add("NameEn", PaginationFilter.NameEn);

            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "Code":
                        if(item.Value != null)
                            categories = categories.Where(c => c.Code.ToUpper() == PaginationFilter.Code.ToUpper()).ToList();
                        break;
                    case "NameAr":
                        if (item.Value != null)
                            categories = categories.Where(c => c.NameAr.ToUpper() == PaginationFilter.NameAr.ToUpper()).ToList();
                        break;
                    case "NameEn":
                        if (item.Value != null)
                            categories = categories.Where(c => c.NameEn.ToUpper() == PaginationFilter.NameEn.ToUpper()).ToList();
                        break;
                    default:
                        break;
                }
            }
                int totalCount = await DbSet.Categories.CountAsync();
                categories = categories.Skip(PaginationFilter.ItemsPerPage * (PaginationFilter.PageNumber - 1))
                                          .Take(PaginationFilter.ItemsPerPage).OrderByDescending(r => r.CreatedOn).ToList();
                List<CategoryDto> cats = new List<CategoryDto>();
                foreach (var item in categories)
                {
                    CategoryDto c = new CategoryDto();
                    c = _mapper.Map<CategoryDto>(item);
                    if (item.ParentCategoryId != null)
                    {
                        var a = await GetByIdAsync((int)c.ParentCategoryId);
                        c.ParentCategory = a;
                    }
                    cats.Add(c);
                }
                if (cats.Count == 0)
                {
                    return null;
                }
                var countListData = new CountListData<CategoryDto>(cats, totalCount);
                return countListData;
        }

        //Get One Category
        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var entityToFind = await DbSet.Categories.FindAsync(id);
            if (entityToFind != null && !entityToFind.IsDeleted)
            {
                if (entityToFind.ParentCategoryId != null)
                {
                    var parentCategory = await DbSet.Categories.FindAsync((int)entityToFind.ParentCategoryId);
                    entityToFind.ParentCategory = parentCategory;
                }
                var categoryDto = _mapper.Map<CategoryDto>(entityToFind);
                return categoryDto;
            }
            return null;
        }

        //Get Category By ParentCategoryId(fils)
        public async Task<ICollection<CategoryDto>> GetByParentCategoryIdAsync(int parentCategoryId)
        {
            var categoriesToFind =  await DbSet.Categories
                .Where(category => category.ParentCategoryId == parentCategoryId && !category.IsDeleted).ToListAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categoriesToFind);
            return categoryDtos;
        }

        //Delete Category
        public async Task<bool> RemoveAsync(int id)
        {
            var entityToRemove = await DbSet.Categories.FindAsync(id);
            if (entityToRemove != null)
            {
                entityToRemove.IsDeleted = true;
                await DbSet.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //Update Category
        public async Task<CategoryDto> UpdateAsync(CategoryDto category)
        {
            category.ModifiedOn = DateTime.Now.AddHours(-2);
            var cat = _mapper.Map<Category>(category);
            DbSet.Entry(cat).State = EntityState.Modified;
            await DbSet.SaveChangesAsync();
            return category;
        }
    }
}
