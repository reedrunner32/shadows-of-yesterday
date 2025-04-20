using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public float swayAmount = 0.1f;         // How far it moves
    public float swaySpeed = 1.5f;          // How fast it moves

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float swayX = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        float swayY = Mathf.Cos(Time.time * swaySpeed * 0.8f) * swayAmount;

        transform.localPosition = initialPosition + new Vector3(swayX, swayY, 0f);
    }
}
