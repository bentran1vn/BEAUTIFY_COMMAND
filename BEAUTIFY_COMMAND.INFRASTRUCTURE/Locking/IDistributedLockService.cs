namespace BEAUTIFY_COMMAND.INFRASTRUCTURE.Locking;
public interface IDistributedLockService
{
    /// <summary>
    ///     Acquires a distributed lock with the specified key
    /// </summary>
    /// <param name="key">The lock key</param>
    /// <param name="expiry">The lock expiry time</param>
    /// <param name="waitTime">The time to wait for the lock if it's not available</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A lock handle that should be disposed to release the lock</returns>
    Task<IDisposable> AcquireLockAsync(string key, TimeSpan expiry, TimeSpan waitTime,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Tries to acquire a distributed lock with the specified key
    /// </summary>
    /// <param name="key">The lock key</param>
    /// <param name="expiry">The lock expiry time</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A tuple containing a boolean indicating success and the lock handle if successful</returns>
    Task<(bool Acquired, IDisposable LockHandle)> TryAcquireLockAsync(string key, TimeSpan expiry,
        CancellationToken cancellationToken = default);
}