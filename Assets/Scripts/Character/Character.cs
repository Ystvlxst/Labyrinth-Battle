using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;

    private ICharacterInputSource _inputSource;

    private void Awake()
    {
        _inputSource = (ICharacterInputSource)_inputSourceBehaviour;
    }

    private void Update()
    {
        var movement = new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y);
        movement *= _speed;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, _speed * Time.deltaTime);

        if (movement.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(movement);
    }

    private void OnValidate()
    {
        if (_inputSourceBehaviour && _inputSourceBehaviour is ICharacterInputSource == false)
        {
            Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(ICharacterInputSource));
            _inputSourceBehaviour = null;
        }
    }
}