using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitParticleDistance : MonoBehaviour
{
    public Transform planet;
    public float maxDistance;

    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.main.maxParticles];
        int numParticlesAlive = ps.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 directionToPlanet = (planet.position - particles[i].position).normalized;
            float distanceToPlanet = Vector3.Distance(planet.position, particles[i].position);

            if (distanceToPlanet > maxDistance)
            {
                particles[i].position = planet.position + directionToPlanet * maxDistance;
            }
        }

        ps.SetParticles(particles, numParticlesAlive);
    }
}
