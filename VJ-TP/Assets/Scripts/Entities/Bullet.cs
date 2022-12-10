using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IBullet
{
    public float LifeTime => _lifeTime;
    [SerializeField] private float _lifeTime = 5; //default bullet lifetime

    public float Speed => _speed;
    [SerializeField] private const float _speed = 15; //default bullet speed

    public BaseGun Owner => _owner;
    [SerializeField] private BaseGun _owner;

    public Rigidbody Rigidbody => _rigidbody;
    private Rigidbody _rigidbody;

    public Collider Collider => _collider;
    private Collider _collider;

    [SerializeField] private float bulletPushForce = 1000f; //this could be generic or depend on the weapon
    [SerializeField] private float bulletPushRadius = 3;

    [SerializeField] private List<int> _layerTargets;

		//bullet parabolic motion
    public void Travel() => transform.Translate(new Vector3(0, -_owner.BulletDrop, _speed) * Time.deltaTime);

		//Bullet hit collider
    public void OnTriggerEnter(Collider collider) {
        if (_layerTargets.Contains(collider.gameObject.layer)) {
            if(collider.TryGetComponent<IDamageable>(out IDamageable damageable)){
                damageable.TakeDamage(_owner.Damage);
                if(collider.GetComponent<LifeController>().CurrentLife <= 0){
                    //calculate collision with new colliders spawned by the enemy dying
                    Collider[] bodyparts = Physics.OverlapSphere(transform.position, 1f);
                    foreach(Collider col in bodyparts){
                        if(col.gameObject.layer == 9){
                            if(col.TryGetComponent<Rigidbody>(out Rigidbody rb)){
                                rb.AddExplosionForce(bulletPushForce, transform.position,bulletPushRadius, 0f);
                            }
                        }
                    }
                }
            }else if(collider.TryGetComponent<IExplodable>(out IExplodable explodable)){
                explodable.Explode();
            }
        }
      Destroy(this.gameObject); //always destroy if it hits something
    }

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    private void Update() {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0) Destroy(this.gameObject);
        Travel();
    }

    public void SetOwner(BaseGun owner) => _owner = owner;
}
