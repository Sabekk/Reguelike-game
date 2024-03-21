using static ObjectPooling.ObjectPool;

public interface IPoolable
{
    public PoolObject Poolable { get; }
    public void AssignPoolable(PoolObject poolable);
}
