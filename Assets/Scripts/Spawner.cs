using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _capsuleSpawnPoint;
    [SerializeField] private Transform _sphereSpawnPoint;
    [SerializeField] private Transform _cylinderSpawnPoint;
    [SerializeField] private Capsule _capsulePrefab;
    [SerializeField] private Sphere _spherePrefab;
    [SerializeField] private Cylinder _cylinderPrefab;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private Mover _mover;

    private List<Capsule> _addedCapsules = new List<Capsule>();
    private List<Cylinder> _addedCylinders = new List<Cylinder>();
    private List<Sphere> _addedShperes= new List<Sphere>();
    
    private static float _elapsedTime = 2f;
    private bool _isCounting;
    
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(_elapsedTime);
    private Coroutine _coroutine;

    private void Start()
    {
        StartCountDown();
    }

    private void Update()
    {
        if(_addedCapsules != null)
            foreach (var capsule in _addedCapsules)
                _mover.Move(capsule);
        
        if (_addedCylinders != null)
            foreach (var cylinder in _addedCylinders)
                _mover.Move(cylinder);
        
        if (_addedShperes != null)
            foreach (var shpere in _addedShperes)
                _mover.Move(shpere);
    }

    private void StartCountDown()
    {
        if (_isCounting)
            return;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _isCounting = true;
        _coroutine = StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    { 
        while (_isCounting)
        {
            ClonePrefabs(_capsulePrefab, _addedCapsules);
            ClonePrefabs(_spherePrefab, _addedShperes);
            ClonePrefabs(_cylinderPrefab, _addedCylinders);

            yield return _waitForSeconds;
        }
    }

    private void ClonePrefabs<T>(T prefab,List<T> addedObjects) where T : Component
    {
        Transform spawnTransform = null;
        
        if (prefab is Capsule)
            spawnTransform = _capsuleSpawnPoint;
        else if (prefab is Sphere)
            spawnTransform = _sphereSpawnPoint;
        else if (prefab is Cylinder)
            spawnTransform = _cylinderSpawnPoint;
    
        if (spawnTransform == null)
            return;
        
        var @object = Instantiate(prefab, spawnTransform.transform.position, Quaternion.identity, spawnTransform);
        addedObjects.Add(@object);
    }
}