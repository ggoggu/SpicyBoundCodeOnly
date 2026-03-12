using System.Collections.Generic;
using UnityEngine;

public class Boss2Rain : PooledObject
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Release();
    }
}
