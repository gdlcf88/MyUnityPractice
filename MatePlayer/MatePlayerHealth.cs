using Assets.Scripts.Interface;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.MatePlayer
{
    public class MatePlayerHealth : MonoBehaviour, IPlayerHealth
    {
        public int StartingHealth = 100;
        public int CurrentHealth { get; set; }
        public Slider HealthSlider;
        public AudioClip DeathClip;

        private Animator _anim;
        private AudioSource _playerAudio;
        private MatePlayerMovement _playerMovement;
        private MatePlayerShooting _playerShooting;
        private bool _isDead;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _playerAudio = GetComponent<AudioSource>();
            _playerMovement = GetComponent<MatePlayerMovement>();
            _playerShooting = GetComponentInChildren<MatePlayerShooting>();
            CurrentHealth = StartingHealth;
        }


        private void Update()
        {

        }


        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;

            HealthSlider.value = CurrentHealth;

            _playerAudio.Play();

            if (CurrentHealth <= 0 && !_isDead)
            {
                Death();
            }
        }


        private void Death()
        {
            _isDead = true;

            _playerShooting.DisableEffects();

            _anim.SetTrigger("Die");

            _playerAudio.clip = DeathClip;
            _playerAudio.Play();

            _playerMovement.enabled = false;
            _playerShooting.enabled = false;
        }


        public void RestartLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}
