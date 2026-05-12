using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private SpawnTarget[] _spawnTargets;
    [SerializeField] private float _speed;
    
    private Dictionary<Component, int> _objectTargets = new Dictionary<Component, int>();
    
    public void Move<T>(T prefab) where T : Component
    {
        if (!_objectTargets.TryGetValue(prefab, out int targetIndex))
        {
            targetIndex = Random.Range(0, _spawnTargets.Length);
            _objectTargets[prefab] = targetIndex;
        }
        
        Vector3 targetPosition = _spawnTargets[targetIndex].transform.position;
        prefab.transform.position = Vector3.MoveTowards(prefab.transform.position, targetPosition, _speed * Time.deltaTime);
        
        if (Vector3.Distance(prefab.transform.position, targetPosition) < 0.1f)
        {
            int newTarget = Random.Range(0, _spawnTargets.Length);
            
            if (_spawnTargets.Length > 1 && newTarget == targetIndex)
                newTarget = (newTarget + 1) % _spawnTargets.Length;
            
            _objectTargets[prefab] = newTarget;
        }
    }
}
