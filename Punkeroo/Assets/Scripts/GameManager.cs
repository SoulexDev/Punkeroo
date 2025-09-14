using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static LayerMask playerRayIgnoreMask;

    private void Awake()
    {
        Instance = this;

        playerRayIgnoreMask = ~LayerMask.GetMask("Player", "Ignore Raycast", "Ignore Player");
    }
}