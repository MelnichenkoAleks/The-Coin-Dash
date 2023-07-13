using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, 40 * Time.deltaTime, 0);
    }
}
