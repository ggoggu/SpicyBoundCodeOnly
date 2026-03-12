using UnityEngine;

public class StraightAttack : WeaponBase
{
    [SerializeField] private float weaponSpeed = 100f;


    public override void OnAttack()
    {
        GameObject weaponInstance = Instantiate(weaponPrefeb, spwanPoint.position, spwanPoint.rotation);
        Rigidbody2D rigidbody2D = weaponInstance.GetComponent<Rigidbody2D>();
        rigidbody2D.linearVelocity = weaponInstance.transform.right * weaponSpeed;
    }

    public void SetWeaponSpeed(float speed)
    {
        weaponSpeed = speed;
    }


}
