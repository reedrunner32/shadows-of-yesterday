using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class MouseLook : MonoBehaviour
    {

        public float mouseXSensitivity = 2f;

        public Transform playerBody;

        float xRotation = 0f;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (SettingsManager.Instance != null)
            {
                mouseXSensitivity = SettingsManager.Instance.GetSensitivity();
            }
        }

        // Update is called once per frame
        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseXSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}