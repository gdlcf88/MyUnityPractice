using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;

        private Text _text;


        private void Awake ()
        {
            _text = GetComponent <Text> ();
            score = 0;
        }


        private void Update ()
        {
            _text.text = "Score: " + score;
        }
    }
}
