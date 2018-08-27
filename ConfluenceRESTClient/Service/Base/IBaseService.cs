using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{
    public interface IBaseService
    {

        T Get<T>(string resource) where T : new();

    }
}
