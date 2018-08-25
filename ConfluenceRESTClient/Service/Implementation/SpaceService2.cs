using ConfluenceRestClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service.Implementation
{
    public class SpaceService2
    {

        private BaseService2 _baseService2;

        public SpaceService2()
        {
            _baseService2 = BaseService2.Instance;
        }

        public Task<SpaceList> GetAllSpaces2()
        {
            return Task.Run(() => {
                var resource = "space";

                return this._baseService2.Get2<SpaceList>(resource);
            });
        }

    }
}
