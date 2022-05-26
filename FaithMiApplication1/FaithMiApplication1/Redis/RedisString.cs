using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaithMiApplication1.Redis
{
    public class RedisString
    {
        private static readonly object _lock = new object();

        private ConnectionMultiplexer redisMultiplexer;

        //private static RedisString _redisClient = null;

        private readonly RedisConfig _redisConfig;

        IDatabase db = null;

        //public static RedisString RedisClient
        //{
        //    get
        //    {
        //        if (_redisClient == null)
        //        {
        //            lock(_lock)
        //            {
        //                _redisClient = new RedisString();
        //            }
        //        }
        //        return _redisClient;
        //    }
        //}

        public RedisString(IOptionsMonitor<RedisConfig> optionsSnapshot)
        {
            _redisConfig = optionsSnapshot.CurrentValue;
            //_redisConfig = optionsSnapshot;
            var RedisConnection = _redisConfig.Value;
            redisMultiplexer = ConnectionMultiplexer.Connect(RedisConnection);
            db = redisMultiplexer.GetDatabase();
        }


        //添加一个key 或者覆盖key的值
        public async Task<Task<bool>> SetKey(string key, string value)
        {
            return db.StringSetAsync(key, value, null, false, StackExchange.Redis.When.Always);
        }


        //只有在键不存在的时候才设置
        public async Task<Task<bool>> SetKeyNx(string key, string value)
        {
            return db.StringSetAsync(key, value, null, false, When.NotExists);
        }

        /// <summary>
        /// 获取一个key的对象
        /// </summary>
        public async Task<T> GetStringKey<T>(string key)
        {
            if (db == null)
            {
                return default;
            }
            var value = await db.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(value);
        }


        //getset一个key，获取旧值并设置新值
        public async Task<T> GetSetKey<T>(string key, string value)
        {
            var oldvalue = await db.StringGetSetAsync(key, value);

            return JsonConvert.DeserializeObject<T>(oldvalue);
        }


        //mset 一次设置多个数据
        public async Task<Task<bool>> MSET(KeyValuePair<RedisKey, RedisValue>[] values)
        {
            return db.StringSetAsync(values, When.NotExists);
        }

        //mget 一次获取多个数据
        public async Task<Task<RedisValue[]>> MGET(RedisKey[] keys)
        {
            return db.StringGetAsync(keys);
        }


        //获取string的length

        public async Task<Task<long>> KeyLen(string key)
        {
            return db.StringLengthAsync(key);
        }


        //append

        public async Task<Task<long>> Append(string key, string appendvalue)
        {
            return db.StringAppendAsync(key, appendvalue);
        }

        //get range

        public async Task<Task<RedisValue>> GetRange(string key, int start, int end)
        {
            return db.StringGetRangeAsync(key, start, end);
        }


        //增加
        public async Task<Task<long>> Incr(string key, int count)
        {
            return db.StringIncrementAsync(key, count);
        }

        //减少
        public async Task<Task<long>> Decr(string key, int count)
        {
            return db.StringDecrementAsync(key, count);
        }

    }
}
