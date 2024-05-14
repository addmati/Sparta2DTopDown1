using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;

    [SerializeField] private SpriteRenderer characterRenderer;

    private TopDownController _controller;

    private void Awake()
    {
        _controller = GetComponent<TopDownController>();
    }

    void Start()
    {
        
        _controller.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newAimDirection)
    {
        // OnLook
        RotateArm(newAimDirection);
    }

    private void RotateArm(Vector2 direction)
    {
        
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;

        
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        
    }
}