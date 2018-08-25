using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;

namespace ConfluenceRESTClient.Service.Implementation
{
    public class SpaceService : ISpaceService
    {

        private BaseService _baseService;

        public SpaceService()
        {
            this._baseService = BaseService.Instance;
        }

        public void CreateSpace(Space space)
        {
            throw new NotImplementedException();
        }
    
        public SpaceList GetAllSpaces()
        {
            var resource = "space";

            return _baseService.Get<SpaceList>(resource);
        }

        /*public Task<SpaceList> GetAllSpacesAsync()
        {
            var request = new RestRequest("space");

            return _baseService.GetAsync<SpaceList>(request);
        }*/

        public Space GetSpaceByName(string name)
        {
            throw new NotImplementedException();
        }

    }
}
