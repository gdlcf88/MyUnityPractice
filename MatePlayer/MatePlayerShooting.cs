using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts.MatePlayer
{
    public class MatePlayerShooting : MonoBehaviour
    {
        public const float EffectsDisplayTime = 0.2f;

        public int DamagePerShot = 20;
        public float Range = 100f;
        public float TimeBetweenBullets = 0.15f;
        public bool IsShooting = false;

        private float _timer;
        private int _shootableMask;
        private Ray _shootRay = new Ray();
        private RaycastHit _shootHit;
        private ParticleSystem _gunParticles;
        private LineRenderer _gunLine;
        private AudioSource _gunAudio;
        private Light _gunLight;


        private void Awake ()
        {
            _shootableMask = LayerMask.GetMask ("Shootable");
            _gunParticles = GetComponent<ParticleSystem> ();
            _gunLine = GetComponent <LineRenderer> ();
            _gunAudio = GetComponent<AudioSource> ();
            _gunLight = GetComponent<Light> ();
        }


        private void Update ()
        {
            _timer += Time.deltaTime;

            if(IsShooting && _timer >= TimeBetweenBullets && Time.timeScale != 0)
            {
                Shoot ();
            }

            if(_timer >= TimeBetweenBullets * EffectsDisplayTime)
            {
                DisableEffects ();
            }
        }


        public void DisableEffects ()
        {
            _gunLine.enabled = false;
            _gunLight.enabled = false;
        }


        private void Shoot ()
        {
            _timer = 0f;

            _gunAudio.Play ();

            _gunLight.enabled = true;

            _gunParticles.Stop ();
            _gunParticles.Play ();

            _gunLine.enabled = true;
            _gunLine.SetPosition (0, transform.position);

            _shootRay.origin = transform.position;
            _shootRay.direction = transform.forward;

            if(Physics.Raycast (_shootRay, out _shootHit, Range, _shootableMask))
            {
                var enemyHealth = _shootHit.collider.GetComponent <EnemyHealth> ();
                if(enemyHealth != null)
                {
                    // enemyHealth.TakeDamage (DamagePerShot, _shootHit.point);
                }
                _gunLine.SetPosition (1, _shootHit.point);
            }
            else
            {
                _gunLine.SetPosition (1, _shootRay.origin + _shootRay.direction * Range);
            }
        }
    }
}
