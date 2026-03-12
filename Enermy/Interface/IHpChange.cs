using System;
using UnityEngine;

public interface IHpChange
{
    Action OnHalfHp { get; set; }
}
