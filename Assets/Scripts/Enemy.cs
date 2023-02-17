using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject lightHitVFX;
    [SerializeField] GameObject tankDeathVFX;
    [SerializeField] GameObject destroyedTank;
    [SerializeField] GameObject terrain;
    
    [SerializeField] int enemyWorth = 0;
    [SerializeField] int hitPoint = 100;

    [SerializeField] AudioSource deathSound;
    [SerializeField] float volume = 0.5f;

    int damageDealt;
    ScoreBoard scoreBoard;
    LevelFinished levelDetails;

    public List<ParticleCollisionEvent> collisionEvents;
    GameObject parent;
    bool destroyed;

    void Start()
    {
          destroyed = false;
          if(this.tag == "Static Enemy" || this.tag == "Tank")
          {
               Physics.IgnoreCollision(GetComponent<BoxCollider>(), terrain.GetComponent<TerrainCollider>());
          }
          scoreBoard = FindObjectOfType<ScoreBoard>();    
          levelDetails = FindObjectOfType<LevelFinished>();
          collisionEvents = new List<ParticleCollisionEvent>(); 
          Rigidbody rb = gameObject.AddComponent<Rigidbody>();
          rb.useGravity = false;
          parent = GameObject.FindWithTag("RunTimeJunk");

          switch(this.tag){
            case "HeavyEnemy":
                enemyWorth = 3000;
                break;
            case "MediumEnemy":
                enemyWorth = 2000;
                break;
            case "LightEnemy":
                enemyWorth = 1000;
                break;
            case "Tank":
                enemyWorth = 1500;
                break;
            case "Static Enemy":
                enemyWorth = 2500;
                break;
            default:
                break;
          }
    }
   private void OnParticleCollision(GameObject other) 
   {    
        
        if(other.tag == "HeavyWeapon")
        {
          damageDealt = 100;
        }
        else if(other.tag == "LightWeapon")
        {
          damageDealt = 30;
        }
        ParticleSystem hitParticle = other.GetComponent<ParticleSystem>();
        int numberOfCollisions = ParticlePhysicsExtensions.GetCollisionEvents(hitParticle, this.gameObject, collisionEvents);
        ProcessHit(collisionEvents, numberOfCollisions);
        if(hitPoint <= 0)
        { 
            if(!destroyed){   
                ProcessDestroy();     
            }
            
        }
        
   }

   void ProcessDestroy()
   {        
            destroyed = true;

            foreach (Renderer r in GetComponentsInChildren<Renderer>()){
                r.enabled = false;
            }
            foreach (Collider c in GetComponentsInChildren<Collider>()){
                c.enabled = false;
            }

            GameObject vfxTank;
            GameObject vfxNormal;
            GameObject tankProp;
            scoreBoard.IncreaseScore(enemyWorth);

            switch(enemyWorth){
                case 3000:
                   levelDetails.incrementHeavy();
                   Debug.Log("Heavy shot");
                   break;
                case 2000:
                   levelDetails.incrementMedium();
                   Debug.Log("Medium shot");
                   break;
                case 1000:
                   levelDetails.incrementLight();
                   Debug.Log("Light shot");
                   break;
                case 1500:
                   levelDetails.incrementTank();
                   Debug.Log("Tank shot");
                   break;
                case 2500:
                   levelDetails.incrementStatic();
                   Debug.Log("Static shot");
                   break;
                default:
                   break;
            }

            if(this.tag == "Tank"){
                vfxTank = Instantiate(tankDeathVFX, transform.position + Vector3.up, Quaternion.identity);
                tankProp = Instantiate(destroyedTank, transform.position, transform.rotation);
                tankProp.transform.parent = parent.transform;
            }
            
            vfxNormal = Instantiate(deathVFX, transform.position, Quaternion.identity);
            if(this.tag == "HeavyEnemy"){
                  vfxNormal.transform.localScale = this.transform.localScale;
            }
            
            vfxNormal.transform.parent = parent.transform;

            deathSound.volume = Random.Range(0.90f * volume, 1.1f * volume);
            deathSound.pitch = Random.Range(0.90f, 1.05f);
            if(this.tag == "HeavyEnemy"){
              deathSound.volume = Random.Range(1.90f * volume, 2.10f * volume);
            }
            deathSound.Play();

            
            Destroy(gameObject, 4f);
   }

   void ProcessHit(List<ParticleCollisionEvent> collisions, int numOfCollisions)
   {    
        int i = 0;
        while(i < numOfCollisions)
        {
          GameObject vfx = Instantiate(lightHitVFX, collisionEvents[i].intersection, Quaternion.LookRotation(collisionEvents[i].normal));
          vfx.transform.parent = parent.transform;
          scoreBoard.IncreaseScore(damageDealt * 5);
          hitPoint = hitPoint - damageDealt;
          i++;
        }
        
   }

   
}
