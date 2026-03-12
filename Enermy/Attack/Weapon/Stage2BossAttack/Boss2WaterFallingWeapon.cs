using UnityEngine;

public class Boss2WaterFallingWeapon : WeaponBase
{
    public override void OnAttack()
    {
        const float spawnHeight = 19.3f;
        spwanPoint.position = new Vector3(target.position.x, spawnHeight, target.position.z);

        Instantiate(weaponPrefeb, spwanPoint.position, Quaternion.identity);
    }
}
