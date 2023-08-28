using Source.EnemyView;
using UnityEngine;

public class FieldOfVisionLateUpdate : MonoBehaviour
{
    [SerializeField] private FieldOfVisionView _fieldOfVisionView;

    private void LateUpdate()
    {
        _fieldOfVisionView.Render();
    }
}
