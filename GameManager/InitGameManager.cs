using UnityEngine;

public class InitGameManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.InitGameManager();
    }
}
