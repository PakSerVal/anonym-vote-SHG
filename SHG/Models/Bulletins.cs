using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SHG.Data;
using SHG.Models.Entites;

namespace SHG.Models
{
    public class Bulletins
    {
        private ElectContext electContext;

        public Bulletins(ElectContext electContext)
        {
            this.electContext = electContext;
        }

        public bool saveBulletin (int voterId, string bulletinData)
        {
            if (electContext.Bulletins.SingleOrDefault(b => b.VoterId == voterId) == null)
            {
                Bulletin bulletin = new Bulletin();
                bulletin.VoterId = voterId;
                bulletin.Data = bulletinData;
                electContext.Bulletins.Add(bulletin);
                electContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
