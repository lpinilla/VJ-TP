using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory<T> where T : MonoBehaviour
{
    T Create(T prefab,Transform parent);

    T[] CreateN(T prefab, Transform[] possibleSpawnPoints, int n);
}
