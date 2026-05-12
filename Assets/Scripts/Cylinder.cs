using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Renderer))]

public class Cylinder : MonoBehaviour
{
    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.yellow;
    }
}
