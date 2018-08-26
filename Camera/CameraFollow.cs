using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        public Texture2D Cur;
        public Transform Target;
        public float Smoothing = 5f;

        private Vector3 _offset;

        // Use this for initialization
        private void Start()
        {
            //Cursor.SetCursor(Cur, Vector2.zero, CursorMode.Auto);
            _offset = transform.position - Target.position;
        }

        private void FixedUpdate()
        {
            var targetCamPos = Target.position + _offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, Smoothing * Time.deltaTime);
        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}
