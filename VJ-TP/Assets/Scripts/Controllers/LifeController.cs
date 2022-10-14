using UnityEngine;

[RequireComponent(typeof(Actor))]
public class LifeController : MonoBehaviour, IDamageable
{
    public float MaxLife => GetComponent<Actor>().ActorStats.MaxLife;
    [SerializeField] private float _currentLife;

    private void Start()
    {
        _currentLife = MaxLife;
        if(name == "Character")
            EventsManager.instance.CharacterLifeChange(_currentLife, MaxLife);
    }

    public void TakeDamage(float damage)
    {
        _currentLife -= damage;
        if (name == "Character")
            EventsManager.instance.CharacterLifeChange(_currentLife, MaxLife);
        if (_currentLife <= 0) Die();
    }

    public void Die(){
			if(tag == "Enemy"){
				GetComponent<Animator>().Play("Die");
				//Die will be called via event once the animation completes
			}else{
				Destroy(this.gameObject);
			}
		}

    private void OnDestroy()
    {
        if(name == "Character") EventsManager.instance.EventGameOver(false);
    }
}
