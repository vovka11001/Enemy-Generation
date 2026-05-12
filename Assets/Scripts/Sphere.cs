using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Renderer))]

public class Sphere : MonoBehaviour
{
    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.blue;
    }
}
