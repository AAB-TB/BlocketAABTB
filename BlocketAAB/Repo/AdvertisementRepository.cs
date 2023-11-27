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
    public class AdvertisementRepository : IAdvertisementService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        public bool Add(Advertisement ad)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Title", ad.Title);
                parameters.Add("Description", ad.Description);
                parameters.Add("Price", ad.Price);
                parameters.Add("CategoryId", ad.CategoryId);

                using (var dbConnection = DataConnection.GetDbConnection())
                {
                    // Execute the stored procedure and get the result
                    var result = dbConnection.QueryFirstOrDefault<int>("AddAdvertisement", parameters, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error adding advertisement '{ad.Title}'.");
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("AdvertisementId", id);

                using (var dbConnection = DataConnection.GetDbConnection())
                {
                    // Execute the stored procedure to delete the advertisement
                    var result = dbConnection.QueryFirstOrDefault<int>("DeleteAdvertisement", parameters, commandType: CommandType.StoredProcedure);

                    // Check the result to determine if the deletion was successful
                    if (result == 1)
                    {
                        logger.Info($"Advertisement with ID '{id}' deleted successfully.");
                    }
                    else
                    {
                        logger.Info($"Advertisement with ID '{id}' does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error deleting advertisement with ID '{id}'.");
                throw;
            }
        }

        public List<Advertisement> GetAll()
        {
            try
            {
                using (var dbConnection = DataConnection.GetDbConnection())
                {
                    var advertisements = dbConnection.Query<Advertisement>("GetAdvertisements", commandType: CommandType.StoredProcedure);

                    // Check if advertisements is null or has no items
                    return advertisements?.Any() == true ? advertisements.ToList() : new List<Advertisement>();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error getting all advertisements.");
                throw;
            }
        }

        public List<Advertisement> Search(string searchTerm)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("SearchTerm", searchTerm);

                using (var dbConnection = DataConnection.GetDbConnection())
                {
                    var advertisements = dbConnection.Query<Advertisement>("SearchAdvertisements", parameters, commandType: CommandType.StoredProcedure);
                    return advertisements.ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error searching advertisements with term '{searchTerm}'.");
                throw;
            }
        }

        public bool Update(Advertisement ad)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("AdvertisementId", ad.AdvertisementId);
                parameters.Add("Title", ad.Title);
                parameters.Add("Description", ad.Description);
                parameters.Add("Price", ad.Price);
                parameters.Add("CategoryId", ad.CategoryId);

                using (var dbConnection = DataConnection.GetDbConnection())
                {
                    var result = dbConnection.Execute("UpdateAdvertisement", parameters, commandType: CommandType.StoredProcedure);

                    // Check if the advertisement was updated (result > 0)
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error updating advertisement with ID '{ad.AdvertisementId}'.");
                throw;
            }
        }

        public Advertisement Get(int advertisementId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("AdvertisementId", advertisementId);

                using (var dbConnection = DataConnection.GetDbConnection())
                {
                    var advertisement = dbConnection.QueryFirstOrDefault<Advertisement>("GetAdvertisementById", parameters, commandType: CommandType.StoredProcedure);

                    // Check if advertisement is null before returning
                    return advertisement ?? new Advertisement(); // Provide a default value if null
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error getting advertisement with ID '{advertisementId}'.");
                throw;
            }
        }

    }
}
