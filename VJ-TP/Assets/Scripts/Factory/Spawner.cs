using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner<T> : IFactory<T> where T : MonoBehaviour
{
    public T Create(T prefab, Transform parent)
    {
        float xPos = Random.Range(-10f, 10f);
        float zPos = Random.Range(-10f, 10f);

        return GameObject.Instantiate(prefab, 
            new Vector3(parent.position.x + xPos,parent.position.y,parent.position.z + zPos),
            Quaternion.identity,
            parent);
    }
}
