using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfluenceRestClient.Model;
using RestSharp;

namespace ConfluenceRESTClient.Service.Implementation
{
    public class SpaceService : ISpaceService
    {
        private DevDefinedBaseService _baseService;

        public SpaceService()
        {
            this._baseService = DevDefinedBaseService.Instance;
        }

        public void CreateSpace(Space space)
        {
            throw new NotImplementedException();
        }

        public Task<SpaceList> GetAllSpacesAsync()
        {
            return Task.Run(() => {
                var resource = "space";

                return this._baseService.Get<SpaceList>(resource);
            });
        }

        public Space GetSpaceByName(string name)
        {
            throw new NotImplementedException();
        }

    }
}
