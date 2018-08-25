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

        public SpaceService() : base() { }

        public void CreateSpace(Space space)
        {
            throw new NotImplementedException();
        }
    
        public SpaceList GetAllSpaces()
        {
            var request = new RestRequest("space");

            return Get<SpaceList>(request);
        }

        public Task<SpaceList> GetAllSpacesAsync()
        {
            var request = new RestRequest("space");

            return GetAsync<SpaceList>(request);
        }

        public Space GetSpaceByName(string name)
        {
            throw new NotImplementedException();
        }

    }
}
