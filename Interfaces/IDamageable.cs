using System;

public interface IDamageable
{
    bool TakeDamage(int amount);
    void Die();

    Action OnDeath { get; set; }
}
