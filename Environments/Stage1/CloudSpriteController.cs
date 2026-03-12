using UnityEngine;

[RequireComponent(typeof(MovingPlatform2), typeof(SpriteRenderer))]
public class CloudSpriteController : MonoBehaviour
{
    
    public Sprite baseCloud;
    public Sprite OnPlyerCloud;

    private IMovingPlatform movingPlatform;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        movingPlatform = GetComponent<MovingPlatform2>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        movingPlatform.OnPlyerEnter += CloudOnPlyer;
        movingPlatform.OnPlayerExit += CloudOutPlyer;
    }


    private void CloudOnPlyer()
    {
        spriteRenderer.sprite = OnPlyerCloud;
    }

    private void CloudOutPlyer()
    {
        spriteRenderer.sprite = baseCloud;
    }

    


}
