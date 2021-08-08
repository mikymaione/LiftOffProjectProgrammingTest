/*
MIT License

Copyright (c) 2021 Michele Maione - mikymaione@hotmail.it

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour
{

    [Range(0, 10)]
    public float MovementForce = 5;

    [Range(0, 10)]
    public float JumpForce = 10;

    public Rigidbody RigidBody;
    public Collider PlayerCollider;

    private bool _isJumping;


    void Start()
    {
        Assert.IsNotNull(RigidBody, "Field RigidBody need to be setted");
        Assert.IsNotNull(PlayerCollider, "Field PlayerCollider need to be setted");
    }

    void Update()
    {
        // get horizontal direction
        var horizontalDirection = Input.GetAxis("Horizontal");
        var verticalDirection = Input.GetAxis("Vertical");

        if (!_isJumping && verticalDirection > 0)
        {
            _isJumping = true;
            RigidBody.AddForce(0, JumpForce, 0, ForceMode.Impulse);
        }

        // move the player
        transform.Translate(horizontalDirection * MovementForce * Time.deltaTime, 0, 0);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Ground" && RigidBody.velocity.y == 0)
            _isJumping = false;
    }

}