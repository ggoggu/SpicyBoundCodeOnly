using UnityEngine;

public class State1AlignToBoss : MonoBehaviour
{
    public GameObject boss;
    public GameObject destoryWall;
    public GameObject startMoveClouds;

    private void Start()
    {
        Boss1 boss1 = boss.GetComponent<Boss1>();
        boss1.OnHalfHp += DestoryWall;
        boss1.OnAfterQuaterHp += StartMoveCloud;
    }

    private void DestoryWall()

    {
        if (destoryWall != null)
        {
            IEnvTrigger envControl = destoryWall.GetComponent<IEnvTrigger>();
            if (envControl != null)
            {
                envControl.Trigger();
            }
            else
            {
                               Debug.LogWarning("IEnvTrigger component not found on destoryWall.");
            }
        }
        else
        {
            Debug.LogWarning("Destroy wall is not assigned in State1AlignToBoss.");
        }
    }

    private  void StartMoveCloud()
    {
        if (startMoveClouds != null)
        {
            IEnvTrigger envControl = startMoveClouds.GetComponent<IEnvTrigger>();
            if (envControl != null)
            {
                envControl.Trigger();
            }
            else
            {
                Debug.LogWarning("IEnvTrigger component not found on startMoveClouds.");
            }
        }
        else
        {
            Debug.LogWarning("Start move clouds is not assigned in State1AlignToBoss.");
        }
    }


}
