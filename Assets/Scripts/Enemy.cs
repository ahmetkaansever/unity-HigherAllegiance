using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject lightHitVFX;
    [SerializeField] GameObject terrain;
    
    [SerializeField] int enemyWorth = 15;
    [SerializeField] int hitPoint = 100;

    int damageDealt;
    ScoreBoard scoreBoard;

    public List<ParticleCollisionEvent> collisionEvents;
    GameObject parent;
    

    void Start()
    {
          if(this.tag == "Static Enemy")
          {
               Physics.IgnoreCollision(GetComponent<BoxCollider>(), terrain.GetComponent<TerrainCollider>());
          }
          scoreBoard = FindObjectOfType<ScoreBoard>();    
          collisionEvents = new List<ParticleCollisionEvent>(); 
          Rigidbody rb = gameObject.AddComponent<Rigidbody>();
          rb.useGravity = false;
          parent = GameObject.FindWithTag("RunTimeJunk");
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
            ProcessDestroy();
        }
        
   }

   void ProcessDestroy()
   {
            GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
            vfx.transform.parent = parent.transform;
            Destroy(gameObject);
   }

   void ProcessHit(List<ParticleCollisionEvent> collisions, int numOfCollisions)
   {    
        int i = 0;
        while(i < numOfCollisions)
        {
          GameObject vfx = Instantiate(lightHitVFX, collisionEvents[i].intersection, Quaternion.LookRotation(collisionEvents[i].normal));
          vfx.transform.parent = parent.transform;
          scoreBoard.IncreaseScore(enemyWorth * damageDealt);
          hitPoint = hitPoint - damageDealt;
          i++;
        }
        
   }

   
}
