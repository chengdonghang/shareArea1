using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject player;
        public float CameraHeight = 3.0f;

        public float RotateSpeed;
        public float maxCameraUpRotate;
        private float _CameraRadius;
        private Vector2 _CameraPos = new Vector2(3.0f, 270);
        private float _CameraUp = 0;
        private bool _MouseBtnDown = false;

        void Start()
        {
            _CameraRadius = CameraHeight;
            _CameraPos.x = _CameraRadius;
            transform.LookAt(player.transform);
            _CameraUp = transform.rotation.eulerAngles.y;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(0)) _MouseBtnDown = true;
            if (Input.GetMouseButtonUp(0)) _MouseBtnDown = false;
            if (_MouseBtnDown)
            {
                float deltaX = Input.mouseScrollDelta.x;
                float deltaY = Input.mouseScrollDelta.y;
                var preY = _CameraPos.y;
                var quaternion = transform.rotation;
                var preX = quaternion.eulerAngles.y;
                preY += deltaX * RotateSpeed;
                preX += deltaY * RotateSpeed;
                if (preY > 360)
                    preY %= 360;
                else if (preY < 0)
                    preY += 360;

                _CameraUp = Mathf.Clamp(preX, -maxCameraUpRotate, maxCameraUpRotate);
            }
            
        }

    }
}

