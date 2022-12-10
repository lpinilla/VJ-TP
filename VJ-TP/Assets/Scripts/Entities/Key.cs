using UnityEngine;

public class Key : MonoBehaviour{

    [SerializeField] private float angularSpeed = 30f;

    private Vector3 _rotation;

    void Update(){
        _rotation = transform.localEulerAngles;
        _rotation.y += Time.deltaTime * angularSpeed;
        transform.localEulerAngles = _rotation;
    }
}
