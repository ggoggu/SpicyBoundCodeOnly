using UnityEngine;

public interface IWeaponInterface
{
    bool isUeable { get; set; }
    void Init(Transform target, float damage, float coolTime);
    void TryAttack();
}
