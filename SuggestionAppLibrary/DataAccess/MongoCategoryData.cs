
using Microsoft.Extensions.Caching.Memory;

namespace SuggestionAppLibrary.DataAccess;

public class MongoCategoryData : ICategoryData
{
   private readonly string _name; IMongoCollection<CategoryModel> _categories;
   private readonly IMemoryCache _cache;
   private const string CacheName = "CategoryData";

   public MongoCategoryData(IDBConnection db, IMemoryCache cache)
   {
      _cache = cache;
      _categories = db.CategoryCollection;
   }

   public async Task<List<CategoryModel>> GetCategories()
   {
      var output = _cache.Get<List<CategoryModel>>(CacheName);
      if (output is null)
      {
         var results = await _categories.FindAsync(_ => true);
         output = results.ToList();

         _cache.Set(CacheName, output, TimeSpan.FromDays(1));
      }

      return output;
   }

   public Task CreateCategory(CategoryModel category)
   {
      return _categories.InsertOneAsync(category);
   }



}
