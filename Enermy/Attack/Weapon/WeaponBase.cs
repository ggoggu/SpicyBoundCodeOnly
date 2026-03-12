using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeaponInterface
{
    [SerializeField]
    protected GameObject weaponPrefeb;
    [SerializeField]
    protected Transform spwanPoint;

    protected Transform target;
    protected float damage;

    private float maxCoolTime;
    private float currentCoolTime = 0f;
    private bool isSkillAvailable = true;

    public bool isUeable { get; set; } = true;

    public void Init(Transform target, float damage, float coolTime)
    {
        this.target = target;
        this.damage = damage;
        maxCoolTime = coolTime;
    }

    private void Update()
    {
        if (isSkillAvailable == false && Time.time - currentCoolTime > maxCoolTime)
        {
            isSkillAvailable = true;
        }
    }

    public void TryAttack()
    {
        if (isSkillAvailable == true && isUeable == true)
        {
            OnAttack();
            isSkillAvailable = false;
            currentCoolTime = Time.time;
        }
    }


    public abstract void OnAttack();

    public void ChnageWeaponPrefebScale(Vector3 scale)
    {
        if (weaponPrefeb != null)
        {
            weaponPrefeb.transform.localScale = scale;
        }
    }

    public void SetSpawnPoint(Transform spawnPoint)
    {
        this.spwanPoint = spawnPoint;
    }

    public void SetProjecTile(GameObject weaponPrefeb)
    {
        this.weaponPrefeb = weaponPrefeb;
    }
}
