using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Renderer))]
[RequireComponent(typeof(Mover))]

public class Capsule : MonoBehaviour
{
    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.red;
    }
}