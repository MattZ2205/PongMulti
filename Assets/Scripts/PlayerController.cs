using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (isLocalPlayer)
        {
            if (transform.position.y > -2.5f)
                if (Input.GetKey(KeyCode.S)) transform.Translate(new Vector2(0, -1 * speed * Time.deltaTime));

            if (transform.position.y < 2.5f)
                if (Input.GetKey(KeyCode.W)) transform.Translate(new Vector2(0, speed * Time.deltaTime));
        }
    }
}
