using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _speed;

    float speed;
    Vector2 movementInput;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = _speed;
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(movementInput.normalized * speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        
    }
}
