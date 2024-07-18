namespace PRN231_GroupProject_LearningOnline.Services
{
    public sealed class UriService : IUriService
    {
        #region Properties
        private readonly string _baseUri;
        #endregion

        #region Constructor
        public UriService(string baseUri)
        {
            this._baseUri = baseUri;
        }

        public string GetBaseUri() => _baseUri; 
        #endregion

        #region Method
        public Uri GetRouteUri(string route) => new Uri(string.Concat(_baseUri, route));
        #endregion
    }
}
