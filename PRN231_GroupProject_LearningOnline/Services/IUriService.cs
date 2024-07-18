namespace PRN231_GroupProject_LearningOnline.Services
{
    public interface IUriService
    {
        /// <summary>
        /// Chức năng: tạo uri với host hiện tại của server
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        Uri GetRouteUri(string route);

        /// <summary>
        /// Chức năng: lấy base uri với host hiện tại
        /// </summary>
        /// <returns></returns>
        string GetBaseUri();

    }
}
