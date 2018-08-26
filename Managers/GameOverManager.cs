using System.Linq;
using Assets.Scripts.Interface;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameOverManager : MonoBehaviour
    {
        private GameObject[] _players;
        private Animator _anim;


        private void Awake()
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
            _anim = GetComponent<Animator>();
        }


        private void Update()
        {
            if (_players.Any(playerHealth => playerHealth.GetComponent<IPlayerHealth>().CurrentHealth <= 0))
            {
                _anim.SetTrigger("GameOver");
            }
        }
    }
}
