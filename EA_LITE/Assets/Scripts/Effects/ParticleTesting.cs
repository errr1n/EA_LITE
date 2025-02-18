using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is purely for testing the particle systems that do not loop

public class ParticleTesting : MonoBehaviour
{
    public List<ParticleSystem> particleSystemsToTest = new List<ParticleSystem>();
    
    // Start is called before the first frame update
    void Update(){
        if (Input.GetKey("g")){
            if (particleSystemsToTest.Count > 0){
                foreach(ParticleSystem ps in particleSystemsToTest){
                    if(ps.isStopped){
                        ps.Play();
                    }
                }
            }

        }
        
    }
}
