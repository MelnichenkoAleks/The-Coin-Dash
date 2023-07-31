using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _player;
    public Vector3 offset;
    public float smoothSpeed = 5f;

    void FixedUpdate()
    {
            FollowPlayer();
            Offset();
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(0, _player.position.y, _player.position.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, _player.position.z + offset.z);
        transform.rotation = Quaternion.Euler(14.5f, 0, 0);
    }
    private void Offset()
    {
        if (_player.transform.position.x > 1.7)
        {
            offset.x = 5.5f;
            float newX = Mathf.Lerp(transform.position.x, offset.x, Time.fixedDeltaTime * smoothSpeed);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        if (_player.transform.position.x > -1.7 && _player.transform.position.x < 1.7)
        {
            offset.x = 0;
            float newX = Mathf.Lerp(transform.position.x, offset.x, Time.fixedDeltaTime * smoothSpeed);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        if (_player.transform.position.x < -1.7)
        {
            offset.x = -5.5f;
            float newX = Mathf.Lerp(transform.position.x, offset.x, Time.fixedDeltaTime * smoothSpeed);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}