using UnityEngine;

public class CrashEventSender : MonoBehaviour
{
    public FirstPersonCamera controller; // drag this in from inspector

    void OnCollisionEnter(Collision collision)
    {
        if (controller != null)
        {
            controller.controlsEnabled = false;
        }
    }
}
