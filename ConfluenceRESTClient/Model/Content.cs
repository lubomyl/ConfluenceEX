using ConfluenceRESTClient.Model;

namespace ConfluenceRestClient.Model
{
    public class Content
    {

        public int Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }

        public Links _Links { get; set; }

    }
}
