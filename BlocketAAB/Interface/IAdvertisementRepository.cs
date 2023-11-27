using BlocketAAB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocketAAB.Interface
{
    public interface IAdvertisementService
    {
        List<Advertisement> GetAll();
        bool Add(Advertisement ad);
        bool Update(Advertisement ad);
        void Delete(int id);
        List<Advertisement> Search(string searchTerm);
        Advertisement Get(int advertisementId);
    }
}
