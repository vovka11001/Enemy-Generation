using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour, IMoveable
{
    [SerializeField] private Capsule _capsulePrefab;

    private float _speed = 5f;

    private void Start()
    {
        float rotationY = Random.Range(0f, 100f);
        Quaternion rotation = Quaternion.Euler(0, rotationY, 0);
        Capsule capsule = Instantiate(_capsulePrefab,transform.position,rotation);
        Move(capsule);
    }

    public virtual override void Move(Capsule capsule)
    {
        capsule.transform.position = Vector3.MoveTowards(transform.position, Vector3.forward,_speed);
    }
}