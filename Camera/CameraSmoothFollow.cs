using UnityEngine;

namespace Core.Camera
{
    public class CameraSmoothFollow : BaseMonoBehaviour
    {
        public float smoothDampTime = 0.2f;
        public float verticalSmoothDampTime = 0.3f;

        public Vector3 cameraOffset;

        private Transform _target;
        public Vector3 _focusPosition;

        private Vector3 _smoothDampVelocity;
        private Vector3 _verticalSmoothDampVelocity;

        private UnityEngine.Camera _camera;

        public UnityEngine.Camera Camera
        {
            get { return _camera; }
        }

        protected override void Awake()
        {
            base.Awake();
            _camera = CachedTransform.GetComponent<UnityEngine.Camera>();
        }

        public void FollowTarget(Transform target)
        {
            _target = target;
        }

        public void SetFocus(Vector3 focusPosition)
        {
            _focusPosition = focusPosition;
        }

        public void FixedUpdate()
        {
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            if (_target != null)
            {
                _focusPosition = _target.position;
                return;
            }
            
            CachedTransform.position = Vector3.SmoothDamp(CachedTransform.position, _focusPosition + cameraOffset, ref _smoothDampVelocity, smoothDampTime);
        }
    }
}

