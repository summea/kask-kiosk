using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kask.Services.DAO;
using Kask.Services.Interfaces;

namespace KaskUnitTests.MockServices
{
    public class MockMCOptionService : IMCOptionService
    {
        List<MCOptionDAO> MCOptions = new List<MCOptionDAO>();

        internal void SetUp()
        {
            MCOptionDAO mcOpt0 = new MCOptionDAO() { ID = 0, MCOptionID = 0, MCOptionDescription = "Description 0" };
            MCOptionDAO mcOpt1 = new MCOptionDAO() { ID = 1, MCOptionID = 1, MCOptionDescription = "Description 1" };
            MCOptionDAO mcOpt2 = new MCOptionDAO() { ID = 2, MCOptionID = 2, MCOptionDescription = "Description 2" };

            MCOptions.Add(mcOpt0);
            MCOptions.Add(mcOpt1);
            MCOptions.Add(mcOpt2);
        }
        public MCOptionDAO GetMCOptionByID(int id)
        {
            foreach (var mcoption in MCOptions)
                if (mcoption.MCOptionID == id)
                    return mcoption;
            throw new Exception("No MCOption Found");
        }

        public IList<MCOptionDAO> GetMCOptions()
        {
            return MCOptions;
        }

        public bool CreateMCOption(MCOptionDAO e)
        {
            MCOptions.Add(e);
            return true;
        }

        public bool UpdateMCOption(MCOptionDAO newMCOption)
        {
            foreach (var mcoption in MCOptions)
                if (mcoption.MCOptionID == newMCOption.MCOptionID)
                {
                    MCOptions.Remove(mcoption);
                    MCOptions.Add(newMCOption);
                    return true;
                }
            return false;
        }

        public bool DeleteMCOption(int id)
        {
            foreach (var mcoption in MCOptions)
                if (mcoption.MCOptionID == id)
                {
                    MCOptions.Remove(mcoption);
                    return true;
                }
            return false;
        }

        private void Initialize( MCOptionDAO mcOption, int id, int mcOptionID, string mcOptionDescription )
        {
            mcOption.ID = id;
            mcOption.MCOptionID = mcOptionID;
            mcOption.MCOptionDescription = mcOptionDescription;
        }
    }
}
