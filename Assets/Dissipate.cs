using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissipate : MonoBehaviour
{
    public float delay = 2.5f;
    public float lifespan = 10;

    private ParticleSystem.Particle[] particles;
    private ParticleSystem system;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        system =  gameObject.GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        time += Time.deltaTime;

        if (!system.isPaused && time > delay)
        {
            system.Pause();

            particles = new ParticleSystem.Particle[system.particleCount];
            system.GetParticles(particles);
        }

        if (particles != null)
        {
            for (int p = 0; p < particles.Length; p++)
            {
                Color color = particles[p].startColor;
                color.a = ((lifespan - time + delay) / lifespan);

                if (color.a <= 0) Destroy(gameObject);

                particles[p].startColor = color;
            }

            system.SetParticles(particles, particles.Length);
        }
    }
}
