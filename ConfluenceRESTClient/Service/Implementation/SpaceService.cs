using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using RestSharp;

namespace ConfluenceRESTClient.Service.Implementation
{
    public class SpaceService : BaseService, ISpaceService
    {

        public SpaceService(string username, string password) : base(username, password) { }

        public void CreateSpace(Space space)
        {
            throw new NotImplementedException();
        }

        public SpaceList GetAllSpaces()
        {
            SpaceList ret;
            var request = new RestRequest("space");

            ret = Get<SpaceList>(request);

            return ret;
        }

        public Space GetSpaceByName(string name)
        {
            throw new NotImplementedException();
        }

    }
}
