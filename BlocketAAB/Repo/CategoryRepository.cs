using BlocketAAB.Entities;
using BlocketAAB.Interface;
using Dapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocketAAB.Repo
{
    public class CategoryRepository : ICategoryService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        public int Add(Category category)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("CategoryName", category.CategoryName);

                using (var dbConnection = DataConnection.GetDbConnection())
                {
                    // Execute the stored procedure and return the result (CategoryId)
                    return dbConnection.ExecuteScalar<int>("AddCategory", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error adding category '{category.CategoryName}'.");
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("CategoryId", id);

                using (var dbConnection = DataConnection.GetDbConnection())
                {
                    var result = dbConnection.Execute("DeleteCategory", parameters, commandType: CommandType.StoredProcedure);

                    // Check if the category was deleted (result > 0)
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error deleting category with ID '{id}'.");
                throw;
            }
        }

        public List<Category> GetAll()
        {
            try
            {
                using (var dbConnection = DataConnection.GetDbConnection())
                {
                    var categories = dbConnection.Query<Category>("GetCategories", commandType: CommandType.StoredProcedure);

                    if (categories == null || !categories.Any())
                    {
                        logger.Info("No categories found in the database.");
                        return new List<Category>(); 
                    }

                    return categories.ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error getting all categories.");
                throw;
            }
        }
    }
}
