using ConfluenceRestClient.Model;
using ConfluenceRESTClient.Service;
using System.Threading.Tasks;

namespace ConfluenceRestClient.Service
{

    /// <summary>
    /// Interface providing methods to access resources connected to Content object from Confluence app.
    /// </summary>
    public interface IContentService
    {

        /// <summary>
        /// Async method to get <see cref="List{Content}"/> object containg all <see cref="Content"/> related spaceKey parameter.
        /// </summary>
        /// <returns>Task containing deserialized <see cref="ContentList"/> model class object.</returns>
        /// <param name="spaceKey">Short string representing Confluence Space.</param>
        Task<ContentList> GetContentBySpaceKeyAsync(string spaceKey);

        /// <summary>
        /// Async method to get a <see cref="List{Content}"/> object containg all <see cref="Content"/> related to currently authenticated user.
        /// </summary>
        /// <returns>Task containing deserialized <see cref="ContentList"/> model class object.</returns>
        Task<ContentList> GetAllContentAsync();

        /// <summary>
        /// Async method to create <see cref="Content"/> resource.
        /// </summary>
        /// <param name="content">Model class object representing <see cref="Content"/> resource to create.</param>
        void CreateContent(Content content);
    }
}
