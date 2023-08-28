using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    [SerializeField] private GameObject _playerKey;
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private ParticleSystem _keyholeOpenEffect;
    [SerializeField] private ParticleSystem _openDoorEffect;

    private const string _openDoor = "OpenDoor";

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement player))
        {
            if(_playerKey.gameObject.activeSelf == true)
            {
                _playerKey.gameObject.SetActive(false);
                _keyholeOpenEffect.Play();
                _openDoorEffect.Play();
                _doorAnimator.SetTrigger(_openDoor);
                gameObject.SetActive(false);
            }
        }
    }
}
