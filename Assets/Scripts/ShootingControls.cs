using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingControls : MonoBehaviour
{

    [SerializeField] GameObject [] lightWeapon;
    [SerializeField] GameObject [] heavyWeapon;

    GameObject [] activeWeapons;

    // Start is called before the first frame update
    void Start()
    {
        activeWeapons = lightWeapon;
        StopAllEmission();
    }

    // Update is called once per frame
    void Update()
    {
        GetActiveWeapons();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            FireActiveWeapons(true);
        }
        else
        {
            FireActiveWeapons(false);
        }
    }

    private void FireActiveWeapons(bool state)
    {
        foreach(GameObject item in activeWeapons)
        {
            ParticleSystem particleSystem = item.GetComponent<ParticleSystem>();
            var emission = particleSystem.emission;
            emission.enabled = state;
        }
        
    }

    private void GetActiveWeapons()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Light weapons active");
            StopAllEmission();
            activeWeapons = lightWeapon;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Heavy weapons active");
            StopAllEmission();
            activeWeapons = heavyWeapon;
        }
    }

    private void StopAllEmission()
    {
        foreach(GameObject item in lightWeapon)
        {
            ParticleSystem particleSystem = item.GetComponent<ParticleSystem>();
            var emission = particleSystem.emission;
            emission.enabled = false;
        }
        foreach(GameObject item in heavyWeapon)
        {
            ParticleSystem particleSystem = item.GetComponent<ParticleSystem>();
            var emission = particleSystem.emission;
            emission.enabled = false;
        }
    }
}
