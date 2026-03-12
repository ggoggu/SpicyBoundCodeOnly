using UnityEngine;

[CreateAssetMenu(fileName = "Enermy Data", menuName = "Scriptable Object/Enermy Data", order = int.MaxValue)]
public class EnermyData : ScriptableObject
{
    public float maxHp;
    public float damage;
    public float speed;
}
