using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingControls : MonoBehaviour
{

    [SerializeField] GameObject [] lightWeapon;
    [SerializeField] GameObject [] heavyWeapon;

    [SerializeField] GameObject lightWeaponSelected;
    [SerializeField] GameObject heavyWeaponSelected;

    [SerializeField] AudioSource switchGunSound;
    [SerializeField] AudioSource lightGunSound;
    [SerializeField] AudioSource heavyGunSound;

    AudioSource selectedGunSound;
    GameObject [] activeWeapons;
    public HudController hudController;

    float fireDelay = 0.200f;
    float fireTime = 0.0f;
    float fireBurstTime = 0.0f;
    float fireBurstDelay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        hudController.lightWeaponSelected = this.lightWeaponSelected;
        hudController.heavyWeaponSelected = this.heavyWeaponSelected;
        activeWeapons = lightWeapon;
        StopAllEmission();
        hudController.LightHUDActivate();  
        selectedGunSound = lightGunSound;    
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
            Debug.Log("Fired one bullet");
        }
        else
        {
            FireActiveWeapons(false);
            Debug.Log("Not firing");
        }
    }

    private void FireActiveWeapons(bool state)
    {
        foreach(GameObject item in activeWeapons)
        {
            ParticleSystem particleSystem = item.GetComponent<ParticleSystem>();
            var emission = particleSystem.emission;
            if(state){
                emission.enabled = true;
                fireBurstTime = Time.time + fireBurstDelay;
            }
            if(!state && Time.time > fireBurstTime){
                emission.enabled = false;
            }
            
        }

        if(state){
            if(Time.time > fireTime)
            {
                lightGunSound.pitch = Random.Range(0.93f, 1.02f);
                lightGunSound.volume = Random.Range(0.90f, 1.10f);
                lightGunSound.Play();
                fireTime = Time.time + fireDelay;
            }  
            
        } 
    }

    private void GetActiveWeapons()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            switchGunSound.Play();
            Debug.Log("Light weapons active");
            StopAllEmission();
            activeWeapons = lightWeapon;
            hudController.LightHUDActivate();
            selectedGunSound = lightGunSound;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchGunSound.Play();
            Debug.Log("Heavy weapons active");
            StopAllEmission();
            activeWeapons = heavyWeapon;
            hudController.HeavyHUDActivate();
            selectedGunSound = heavyGunSound;
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
