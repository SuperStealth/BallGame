using System.Collections;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private float baseAnimationRate = -0.5f;

    private Vector2 uvOffset = Vector2.zero;
    private Vector2 uvAnimationRate;

    private IGlobalSpeedMultiplierContainer speedMultiplier;
    
    int materialIndex = 0;
    string textureName = "_MainTex";

    private void Awake()
    {
        uvAnimationRate = new Vector2(0f, baseAnimationRate);
    }

    private void Update()
    {
        uvOffset += (uvAnimationRate * speedMultiplier.Multiplier * Time.deltaTime);
        if (renderer.enabled)
        {
            renderer.materials[materialIndex].SetTextureOffset(textureName, uvOffset);
        }
    }

    public void SetAnimationRateMultiplier(IGlobalSpeedMultiplierContainer multiplierContainer)
    {
        speedMultiplier = multiplierContainer;   
    }
}
