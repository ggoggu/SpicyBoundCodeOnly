using UnityEngine;

public interface IHeadRotate
{
    void Init(Transform headTrnsform, Transform target, float prefixAngle = 90.0f);
    bool isRotate { get; set; }
    bool bIsFlip { get; set; }
}
