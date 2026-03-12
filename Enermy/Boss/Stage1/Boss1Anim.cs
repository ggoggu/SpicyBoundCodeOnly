using System;
using UnityEngine;

public class Boss1Anim : MonoBehaviour
{
    public Boss1 boss1;

    public Action OnSmash;
    public Action EndSmash;

    public Action OnMove;
    public Action EndMove;

    public Action OnEndTurnArrund;

    public void OnSmashAttack()
    {
        OnSmash();
    }

    public void EndSmashAttack()
    {
        EndSmash();
    }

    public void MoveStart()
    {
        OnMove();
    }

    public void MoveEnd()
    {
        EndMove();
    }

    public void EndTurnArround()
    {
        OnEndTurnArrund.Invoke();
    }
}
