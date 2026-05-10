using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Capsule _capsulePrefab;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _speed;

    private List<Capsule> _addedCapsules = new List<Capsule>();

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
        {
            foreach (var capsule in _addedCapsules)
            {
                Move(capsule);
            }
        }
    }

    private void Move(Capsule capsule)
    {
        if(capsule != null)
            capsule.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
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
            int randomSpawn = Random.Range(0, _spawnPoints.Length);
            SpawnPoint spawnPoint = _spawnPoints[randomSpawn];

            float rotationY = Random.Range(0f, 360f);
            Quaternion rotation = Quaternion.Euler(0, rotationY, 0);

            Capsule capsule = Instantiate(_capsulePrefab, spawnPoint.transform.position, rotation);
            _addedCapsules.Add(capsule);

            yield return _waitForSeconds;
        }
    }
}