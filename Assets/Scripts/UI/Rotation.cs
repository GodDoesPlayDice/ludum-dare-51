using UnityEngine;

namespace UI
{
    public class Rotation : MonoBehaviour
    {
        public float speedMod = 1.0f;
        private Vector3 _point;

        private void Start()
        {
            _point = new Vector3(0.0f, 1.0f, 0.0f);
            transform.LookAt(_point);
        }

        private void Update()
        {
            transform.RotateAround(_point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
        }
    }
}