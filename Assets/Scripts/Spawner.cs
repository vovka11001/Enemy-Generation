using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;
    [SerializeField] private SpawnTarget[] _spawnTargets;

    protected List<T> _addedPrefabs = new List<T>();
    
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
        foreach (var prefab in _addedPrefabs)
        {
            if(prefab != null)
            {
                if (prefab.TryGetComponent(out Mover mover))
                {
                    if (mover.ReachedTarget())
                    {
                        int randomTargetIndex = Random.Range(0, _spawnTargets.Length);
                        Vector3 newTargetPosition = _spawnTargets[randomTargetIndex].transform.position;
                        mover.SetTarget(newTargetPosition);
                    }
                }
            }
        }
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
            ClonePrefabs(_prefab, _addedPrefabs);
   
            yield return _waitForSeconds;
        }
    }

    private void ClonePrefabs(T prefab,List<T> addedObjects)
    {
        if (_prefab == null)
            return;

        if (_spawnTargets == null || _spawnTargets.Length == 0)
            return;

        T newObject = Instantiate(_prefab, transform.position, Quaternion.identity);

        int randomTargetIndex = Random.Range(0, _spawnTargets.Length);
        Vector3 targetPosition = _spawnTargets[randomTargetIndex].transform.position;

        newObject.TryGetComponent(out Mover mover);
        {
            if (mover != null)
                mover.SetTarget(targetPosition);
        }

        _addedPrefabs.Add(newObject);
    }
}