using UnityEngine;

public interface ITarget<T>
{
    T targetSign { get; }

    Vector3 position { get; }
}
