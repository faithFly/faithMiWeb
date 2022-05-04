namespace FaithMiApplication1.Models
{
    public class LoginMsgDTO
    {
        /// <summary>
        /// 登录状态码
        /// </summary>
        public int LoginCode { get; set; }
        /// <summary>
        /// 登录状态信息
        /// </summary>
        public string LoginMsg { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }
}
