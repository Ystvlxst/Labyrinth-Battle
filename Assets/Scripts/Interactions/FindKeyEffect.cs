using System.Collections;
using UnityEngine;

public class FindKeyEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _findKEyWithoutAllStars;
    [SerializeField] private ParticleSystem _findKeyEffectAllStars;
    [SerializeField] private float _durationTime;
    [SerializeField] private float _speed;


    public void FindNotAllStars()
    {
        StartCoroutine(Shooting());
        StartCoroutine(Disable());
    }

    public void FindKey()
    {
        _findKeyEffectAllStars.Play();
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            _findKEyWithoutAllStars.Play();
            _findKEyWithoutAllStars.gameObject.SetActive(true);
            _findKEyWithoutAllStars.transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.World);
            yield return null;
        }
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(_durationTime);
        _findKEyWithoutAllStars.gameObject.SetActive(false);
    }
}