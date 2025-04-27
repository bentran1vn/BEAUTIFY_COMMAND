using BEAUTIFY_COMMAND.DOMAIN.Abstractions;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.Locking;
public class RedisDistributedLockService : IDistributedLockService
{
    private readonly ILogger<RedisDistributedLockService> _logger;
    private readonly IConnectionMultiplexer _redis;

    public RedisDistributedLockService(IConnectionMultiplexer redis, ILogger<RedisDistributedLockService> logger)
    {
        _redis = redis ?? throw new ArgumentNullException(nameof(redis));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IDisposable> AcquireLockAsync(string key, TimeSpan expiry, TimeSpan waitTime,
        CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;
        var lockKey = $"lock:{key}";
        var db = _redis.GetDatabase();
        var lockValue = Guid.NewGuid().ToString();

        while (DateTime.UtcNow - startTime < waitTime)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var acquired = await db.StringSetAsync(lockKey, lockValue, expiry, When.NotExists);
            if (acquired)
            {
                _logger.LogInformation("Acquired lock for key {Key} with value {Value}", lockKey, lockValue);
                return new RedisLock(db, lockKey, lockValue);
            }

            // Wait a bit before trying again
            await Task.Delay(100, cancellationToken);
        }

        throw new TimeoutException($"Failed to acquire lock for key {key} within the specified wait time.");
    }

    public async Task<(bool Acquired, IDisposable LockHandle)> TryAcquireLockAsync(string key, TimeSpan expiry,
        CancellationToken cancellationToken = default)
    {
        var lockKey = $"lock:{key}";
        var db = _redis.GetDatabase();
        var lockValue = Guid.NewGuid().ToString();

        var acquired = await db.StringSetAsync(lockKey, lockValue, expiry, When.NotExists);
        if (acquired)
        {
            _logger.LogInformation("Acquired lock for key {Key} with value {Value}", lockKey, lockValue);
            var lockHandle = new RedisLock(db, lockKey, lockValue);
            return (true, lockHandle);
        }

        return (false, null);
    }

    private class RedisLock : IDisposable
    {
        private readonly IDatabase _db;
        private readonly string _key;
        private readonly string _value;
        private bool _disposed;

        public RedisLock(IDatabase db, string key, string value)
        {
            _db = db;
            _key = key;
            _value = value;
            _disposed = false;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;

            // Use Lua script to ensure we only delete the lock if it still has our value
            var script = @"
                    if redis.call('get', KEYS[1]) == ARGV[1] then
                        return redis.call('del', KEYS[1])
                    else
                        return 0
                    end";

            _db.ScriptEvaluate(script, new RedisKey[] { _key }, new RedisValue[] { _value });
        }
    }
}