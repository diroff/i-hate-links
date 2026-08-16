using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Testers.R3Samples
{
    public class T8Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        public Observable<Vector3> CurrentPosition => _currentPosition;
        private ReactiveProperty<Vector3> _currentPosition = new ReactiveProperty<Vector3>();

        private void Start()
        {
            _currentPosition.Value = transform.position;
        }

        private void Update()
        {
            if (Keyboard.current.aKey.IsPressed())
                Move(new Vector2(-1, 0));

            if (Keyboard.current.dKey.IsPressed())
                Move(new Vector2(1, 0));
        }

        private void Move(Vector2 direction)
        {
            transform.position = new Vector3(transform.position.x + direction.x * _speed * Time.deltaTime, transform.position.y, transform.position.z);
            _currentPosition.Value = transform.position;
        }
    }
}