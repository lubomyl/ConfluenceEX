using ConfluenceRestClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{
    public interface ISpaceService
    {

        Space GetSpaceByName(string name);

        SpaceList GetAllSpaces();

        //Task<SpaceList> GetAllSpacesAsync();

        void CreateSpace(Space space);

    }
}
