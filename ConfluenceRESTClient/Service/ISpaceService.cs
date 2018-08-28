using ConfluenceRestClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceRESTClient.Service
{

    /// <summary>
    /// Interface providing methods to access resources connected to Space object from Confluence app.
    /// </summary>
    public interface ISpaceService
    {

        /// <summary>
        /// Async method to get <see cref="Space"/> resources like id, key or name.
        /// </summary>
        /// <returns>Task containing deserialized <see cref="Space"/> model class object.</returns>
        Task<Space> GetSpaceByNameAsync(string name);

        /// <summary>
        /// Async method to get a <see cref="List{Space}"/> object containg all <see cref="Space"/> related to currently authenticated user.
        /// </summary>
        /// <returns>Task containing deserialized <see cref="SpaceList"/> model class object.</returns>
        Task<SpaceList> GetAllSpacesAsync();

        /// <summary>
        /// Async method to create <see cref="Space"/> resource.
        /// </summary>
        /// <param name="space">Model class object representing <see cref="Space"/> resource to create.</param>
        void CreateSpaceAsync(Space space);

    }
}
