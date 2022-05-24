using UnityEngine;
/// <summary>
/// The sign to the target
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITarget<T>
{
    T targetSign { get; }

    Vector3 position { get; }
}
