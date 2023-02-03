using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject lightHitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int enemyWorth = 15;
    [SerializeField] int hitPoint = 100;
    [SerializeField] int damageDealt = 30;
    ScoreBoard scoreBoard;

    public List<ParticleCollisionEvent> collisionEvents;
    
    

    void Start()
    {
          scoreBoard = FindObjectOfType<ScoreBoard>();    
          collisionEvents = new List<ParticleCollisionEvent>(); 
          Rigidbody rb = gameObject.AddComponent<Rigidbody>();
          rb.useGravity = false;
    }
   private void OnParticleCollision(GameObject other) 
   {    
        int numberOfCollisions = ParticlePhysicsExtensions.GetCollisionEvents(other.GetComponent<ParticleSystem>(), this.gameObject, collisionEvents);
        ProcessHit(collisionEvents, numberOfCollisions);
        if(hitPoint < 0)
        {
            ProcessDestroy();
        }
        
        
   }

   void ProcessDestroy()
   {
            GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
            vfx.transform.parent = parent;
            Destroy(gameObject);
   }

   void ProcessHit(List<ParticleCollisionEvent> collisions, int numOfCollisions)
   {    
        int i = 0;
        while(i < numOfCollisions)
        {
          GameObject vfx = Instantiate(lightHitVFX, collisionEvents[i].intersection, Quaternion.LookRotation(collisionEvents[i].normal));
          vfx.transform.parent = parent;
          scoreBoard.IncreaseScore(enemyWorth * damageDealt);
          hitPoint = hitPoint - damageDealt;
          i++;
        }
        
   }
   
}
