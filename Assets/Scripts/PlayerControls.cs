using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] float xFlyingSpeed = 25f;
    [SerializeField] float yFlyingSpeed = 20f;
    [SerializeField] float xRange = 20f;
    [SerializeField] float yRange = 10f;

    [Header("Player Input based tuning")]
    [SerializeField] float pitchThrowFactor = -12f;
    [SerializeField] float rollThrowFactor = -30f;

    [Header("Position based tuning")]
    [SerializeField] float pitchPositionFactor = -2f;
    [SerializeField] float yawPositionFactor = 1f;

    [Tooltip("Modifies how fast the plane completes it's rotation.")]
    [SerializeField] float rotationFactor = 1f;

    [SerializeField] GameObject [] lightWeapon;

    float yThrow;
    float xThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateTranslation();
        CalculateRotation();
        ProcessFiring();

    }

    private void CalculateTranslation()
    {
        xThrow = Input.GetAxis("Horizontal") ;
        yThrow = Input.GetAxis("Vertical") ;

        float xOffSet = xThrow * xFlyingSpeed * Time.deltaTime;
        float yOffSet = yThrow * yFlyingSpeed * Time.deltaTime;

        float rawX = transform.localPosition.x + xOffSet;
        float rawY = transform.localPosition.y + yOffSet;
        float rawZ = transform.localPosition.z;

        float newX = Mathf.Clamp(rawX, -xRange, xRange);
        float newY = Mathf.Clamp(rawY, -yRange + 3, yRange + 7);

        transform.localPosition = new Vector3(newX, newY, rawZ);
    }

    private void CalculateRotation()
    {
        float pitch = (yThrow * pitchThrowFactor + transform.localPosition.y * pitchPositionFactor);
        float yaw = transform.localPosition.x * yawPositionFactor;
        float roll = xThrow * rollThrowFactor;
        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationFactor);
    }

    private void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            SetLightWeaponsActive(true);
        }
        else
        {
            SetLightWeaponsActive(false);
        }
    }

    private void SetLightWeaponsActive(bool state)
    {
        foreach(GameObject item in lightWeapon)
        {
            var emissionModule = item.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = state;
        }
    }

}
