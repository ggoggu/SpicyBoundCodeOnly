using System;
using UnityEngine;

interface IMovingPlatform
{
    public Action OnPlyerEnter { get; set; }
    public Action OnPlayerExit { get; set; }

    public float speed { get; set; }
}
