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
        Debug.Log("DAMAGEEE");
        
        _currentLife -= damage;
        if (name == "Character")
            EventsManager.instance.CharacterLifeChange(_currentLife, MaxLife);
        if (_currentLife <= 0) Die();
    }

    public void Die(){
			if(tag == "Enemy"){
				GetComponent<Animator>().Play("Die");
				//Die will be called via event once the animation completes
			}
            else
            {
                endGame();
            }
            //else{
			//	Destroy(this.gameObject);
			//}
		}

    private void endGame()
    {
        Debug.Log("IM DEAD");
        EventsManager.instance.EventGameOver(false);
    }
}
