using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected bool IsDead;
        
    [SerializeField] private int health;

    private int Health
    {
        get => health;
        set
        {
            health = value;
                
            if(health <= 0) IsDead = true;
        }
    }
        
    public void TakeDamage(int damage) => Health -= damage;
}    