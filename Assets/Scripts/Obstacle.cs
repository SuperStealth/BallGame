using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    private IGlobalSpeedMultiplierContainer speedMultiplier;
    private float bottomYPosition = -10f;

    private void Update()
    {
        var newTransformPositionY = transform.position.y - speedMultiplier.Multiplier * baseSpeed * Time.deltaTime;
        if (newTransformPositionY < bottomYPosition)
        {
            gameObject.SetActive(false);
        }
        transform.position = new Vector3(transform.position.x, newTransformPositionY, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }

    public void SetBottomYPosition(float positionY)
    {
        bottomYPosition = positionY;
    }

    public void SetSpeedMultiplier(IGlobalSpeedMultiplierContainer multiplier)
    {
        speedMultiplier = multiplier;
    }
}
