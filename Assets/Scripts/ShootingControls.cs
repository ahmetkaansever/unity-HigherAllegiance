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

    //Sound Delays
    float fireLightDelay = 0.200f;
    float fireHeavyDelay = 0.250f;
    float fireTime = 0.0f;

    //Firing stop delays
    float fireBurstTime = 0.0f;
    float fireLightBurstDelay = 0.3f;
    float fireHeavyBurstDelay = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        hudController.lightWeaponSelected = this.lightWeaponSelected;
        hudController.heavyWeaponSelected = this.heavyWeaponSelected;
        activeWeapons = lightWeapon;
        StopAllEmission();
        hudController.LightHUDActivate();     
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
            if(state){

                if(activeWeapons == lightWeapon)
                {
                    emission.enabled = true;
                    fireBurstTime = Time.time + fireLightBurstDelay;
                }
                else if(activeWeapons == heavyWeapon)
                {
                    emission.enabled = true;
                    fireBurstTime = Time.time + fireHeavyBurstDelay;
                }
                
            }
            else{
                if(Time.time > fireBurstTime){
                    emission.enabled = false;
                }
            }
            
        }
        //Sound Controls
        if(state){
            if(Time.time > fireTime)
            {
                if(activeWeapons == lightWeapon){
                    lightGunSound.volume = Random.Range(0.30f, 0.32f);
                    lightGunSound.pitch = Random.Range(0.95f, 1.03f);
                    lightGunSound.Play();
                    fireTime = Time.time + fireLightDelay;
                }
                else if(activeWeapons == heavyWeapon){
                    heavyGunSound.volume = Random.RandomRange(0.38f, 0.42f);
                    heavyGunSound.pitch = Random.RandomRange(0.95f, 1.03f);
                    heavyGunSound.Play();
                    fireTime = Time.time + fireHeavyDelay;
                }
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
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            switchGunSound.Play();
            Debug.Log("Heavy weapons active");
            StopAllEmission();
            activeWeapons = heavyWeapon;
            hudController.HeavyHUDActivate();
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
