using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyesColorStates : MonoBehaviour
{
    private const int MaterialIndexGlow = 2;
    private const int MaterialIndex = 1;

    [SerializeField] private List<Pair> _pairs;

    private MeshRenderer _meshRenderer;
    private Material[] _defaultMaterials;
    private Coroutine _coroutine;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultMaterials = _meshRenderer.materials;
    }

    private void OnEnable()
    {
        foreach (Pair pair in _pairs) 
            pair.State.Entered += OnEnteredState;
    }
    
    private void OnDisable()
    {
        foreach (Pair pair in _pairs) 
            pair.State.Entered -= OnEnteredState;
    }

    private void OnEnteredState(State state)
    {
        state.Exited += OnExitedState;
        Pair pair = _pairs.Find(pair => pair.State == state);
        SetMaterials(pair.GlowMaterial, pair.EyeMaterial);
    }

    private void OnExitedState(State state)
    {
        state.Exited -= OnExitedState;
        SetMaterials(_defaultMaterials[2], _defaultMaterials[1]);
    }

    private void SetMaterials(Material pairGlowMaterial, Material material)
    {
        Material[] materials = _meshRenderer.materials;
        materials[MaterialIndex] = material;
        materials[MaterialIndexGlow] = pairGlowMaterial;
        _meshRenderer.materials = materials;
    }

    private IEnumerator ExitState()
    {
        yield return new WaitForSeconds(2);
        SetMaterials(_defaultMaterials[2], _defaultMaterials[1]);
    }
}

[Serializable]
public struct Pair
{
    [field: SerializeField] public State State { get; private set; }
    [field: SerializeField] public Material GlowMaterial { get; private set; }
    [field: SerializeField] public Material EyeMaterial { get; private set; }
}