using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner<T> : IFactory<T> where T : MonoBehaviour
{
    public T Create(T prefab, Transform parent){
        float xPos = Random.Range(-10f, 10f);
        float zPos = Random.Range(-10f, 10f);

        return GameObject.Instantiate(prefab, 
            new Vector3(parent.position.x + xPos,parent.position.y,parent.position.z + zPos), Quaternion.identity, parent);
    }

    public T[] CreateN(T prefab, Transform[] possibleSpawnPoints, int n){
        T[] ret = new T[n];
        for(int i = 0; i < n; i++){
            ret[i] = Create(prefab, possibleSpawnPoints[Random.Range(0,possibleSpawnPoints.Length)]);
        }
        return ret;
    }
}
