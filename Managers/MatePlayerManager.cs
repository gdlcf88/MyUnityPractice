using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class MatePlayerManager : MonoBehaviour
    {

        public GameObject MatePlayer;
        public Transform[] SpawnPoints;

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        public void Spawn()
        {
            var spawnPointIndex = Random.Range(0, SpawnPoints.Length);

            Instantiate(MatePlayer, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
        }
    
    }
}
