﻿using Assets.Scripts.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour, IPlayerHealth
    {
        public int StartingHealth = 100;
        public int CurrentHealth { get; set; }
        public Slider HealthSlider;
        public Image DamageImage;
        public AudioClip DeathClip;
        public float FlashSpeed = 5f;
        public Color FlashColour = new Color(1f, 0f, 0f, 0.1f);

        private Animator _anim;
        private AudioSource _playerAudio;
        private PlayerMovement _playerMovement;
        private PlayerShooting _playerShooting;
        private bool _isDead;
        private bool _damaged;

        private void Awake ()
        {
            _anim = GetComponent <Animator> ();
            _playerAudio = GetComponent <AudioSource> ();
            _playerMovement = GetComponent <PlayerMovement> ();
            _playerShooting = GetComponentInChildren <PlayerShooting> ();
            CurrentHealth = StartingHealth;
        }


        private void Update ()
        {
            DamageImage.color = _damaged ? FlashColour : Color.Lerp (DamageImage.color, Color.clear, FlashSpeed * Time.deltaTime);
            _damaged = false;
        }


        public void TakeDamage (int amount)
        {
            _damaged = true;

            CurrentHealth -= amount;

            HealthSlider.value = CurrentHealth;

            _playerAudio.Play ();

            if(CurrentHealth <= 0 && !_isDead)
            {
                Death ();
            }
        }


        private void Death ()
        {
            _isDead = true;

            _playerShooting.DisableEffects ();

            _anim.SetTrigger ("Die");

            _playerAudio.clip = DeathClip;
            _playerAudio.Play ();

            _playerMovement.enabled = false;
            _playerShooting.enabled = false;
        }


        public void RestartLevel ()
        {
            SceneManager.LoadScene (0);
        }
    }
}
