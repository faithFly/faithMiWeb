namespace FaithMiApplication1.Redis
{

    /// <summary>
    /// 返回结果类
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool ImplementationResults { get; set; }
        /// <summary>
        /// Value的实时值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 缓存的Value值
        /// </summary>
        public string CacheValue { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error { get; set; }

    }
}
