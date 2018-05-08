using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using RestSharp;

namespace ConfluenceRESTClient.Service.Implementation
{
    class SpaceService : BaseService, ISpaceService
    {

        public void CreateSpace(Space space)
        {
            throw new NotImplementedException();
        }

        public SpaceList GetAllSpaces()
        {
            SpaceList ret;
            var request = new RestRequest("content/{id}");

            ret = Get<SpaceList>(request);

            return ret;
        }

        public Space GetSpaceByName(string name)
        {
            throw new NotImplementedException();
        }

    }
}
