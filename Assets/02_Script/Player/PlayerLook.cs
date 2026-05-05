using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class PlayerLook : MonoBehaviour
{
    [Header("Sensitivity")]
    public float mouseSensivity = 0.15f;

    [Header("Pitch")]
    public float minPitch = -10f;
    public float maxPitch = 30f;
    private float camaraY = 0f;
    public Vector2 lookInput;
    private float pitch;
    private Transform cameraTransform;
    private Camera currentCamera;
    public bool inspeccionar = false;
    public float zoomSpeed = 150f;
    public float minFOV = 20f;
    public float maxFOV = 60f;

    [Header("cámaras")]
    public List<Camera> camaras;
    private int indiceAtual = 0;

    void Start()
    {
        UpdateActiveCamera();
        // centra el cursor y lo pone invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTransform == null)
        {
            UpdateActiveCamera();
            if (cameraTransform == null) return; // Si aún no hay cámara, salimos del Update para evitar el error
        }
        if (!GameManager.instancia.menuPrincipal)
        {
            // Rotacion camara
            pitch -= lookInput.y * mouseSensivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);// capear el movimiento
            cameraTransform.localRotation = Quaternion.Euler(pitch, camaraY, 0);
            // Rotacion horizontal
        
            float yaw = lookInput.x * mouseSensivity;
            transform.Rotate(0, yaw, 0, Space.Self);// Space,Self es para que gire sobre si mismo

        }
        else
        {
            // Si el juego ha terminado, aseguramos que el cursor esté desbloqueado y visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    // se ejecuta antes que el start se usa para centrar la camara
    private void OnEnable()
    {
        lookInput = Vector2.zero;
    }
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }
    public void UpdateActiveCamera()
    {
        if (Camera.main != currentCamera)
        {
            currentCamera = Camera.main;
            if (currentCamera != null)
            {
                cameraTransform = currentCamera.transform;
                pitch = cameraTransform.localEulerAngles.x;
                //if (pitch > 180) pitch -= 360;
            }
        }
    }
    public void AcivarCamara(int index)
    {
        for (int i = 0; i < camaras.Count; i++)
        {
            camaras[i].gameObject.SetActive(i == index);
            if (i == index)
            {
                camaras[i].gameObject.tag = "MainCamera";
            }
            else
            {
                camaras[i].gameObject.tag = "Untagged";
            }
        }
        UpdateActiveCamera();
    }
    public void OnCamara(InputValue Value)
    {
        if (Value.isPressed)
        {
            indiceAtual += 1;
            if (indiceAtual >= camaras.Count)
            {
                indiceAtual = 0;
            }
            AcivarCamara(indiceAtual);
        }
    }
}
