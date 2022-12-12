using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExplodableBarrel : MonoBehaviour, IExplodable
{

    [SerializeField] private ExplodableStats _barrelStats;

    public float Damage => _barrelStats.Damage;
    public float PushbackFactor => _barrelStats.PushbackFactor;
    public float Radius => _barrelStats.Radius;
    public GameObject ExplosionParticlePrefab => _barrelStats.ExplosionParticlePrefab; 

    private Vector3 explosionPosition;
    private Collider[] _colliders;
    private Collider[] _enemyParts;
    private Rigidbody _rigidbody;

    void Start(){
        explosionPosition = transform.position;
    }


    public void Explode(){
        //show particles
        Instantiate(ExplosionParticlePrefab, explosionPosition, transform.rotation);
        //create physics sphere and calculate hits
        _colliders = Physics.OverlapSphere(explosionPosition, Radius);
        //First apply damage
        foreach(Collider hit in _colliders){
            if(hit.TryGetComponent<Enemy>(out Enemy enemy)){
                //apply damage
                //the damage varies inversely proportional with the distance to the center
                hit.GetComponent<LifeController>().TakeDamage(Damage * (1 - Vector3.Distance(explosionPosition, enemy.GetComponent<Transform>().position) / Radius) );
            }
        }
        //damage might have killed an enemy and enabled its ragdoll, we need to recalculate
        _enemyParts = Physics.OverlapSphere(explosionPosition, Radius);
        foreach(Collider hit in _enemyParts){
            if(hit.gameObject.layer == 9){
                Rigidbody[] bodyparts = hit.GetComponentsInChildren<Rigidbody>();
                //push
                foreach(Rigidbody rb in bodyparts){
                    rb.AddExplosionForce(Damage * PushbackFactor, explosionPosition,Radius, 0f);
                }
            }
        }
        Destroy(this.gameObject); //destroy barrel after explosion
    }

}
