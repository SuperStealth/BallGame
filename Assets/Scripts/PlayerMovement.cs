using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float leftPositionConstrain;
    [SerializeField] private float rightPositionConstrain;
    [SerializeField] private float speed = 1;
    
    private Vector3 direction = Vector3.right;

    private void Update()
    {         
        if (Input.GetMouseButtonDown(0))
        {
            if (direction == Vector3.left)
            {
                direction = Vector3.right;
            }
            else
            {
                direction = Vector3.left;
            }
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        float newPositionX = transform.position.x + direction.x * speed * Time.deltaTime;
        if (newPositionX > leftPositionConstrain && newPositionX < rightPositionConstrain)
        {
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        }
        
    }
}
