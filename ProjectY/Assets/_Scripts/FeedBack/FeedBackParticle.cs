using UnityEngine;

namespace ProjectY
{
    public class FeedBackParticle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;

        //Listener
        public void Burst(FeedBackParticleData data)
        {
            ParticleSystem.MainModule main = _particle.main;

            _particle.transform.position = data.Position;

            main.startColor = data.Colour;

            ParticleSystem.EmissionModule emission = _particle.emission;
            emission.rateOverTime = data.Amount;

            _particle.Play();
        }
    }
}