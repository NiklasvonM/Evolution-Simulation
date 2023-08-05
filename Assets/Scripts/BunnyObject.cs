using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyObject : MonoBehaviour
{
    private Bunny Bunny;
    private Dictionary<string, float> Characteristics;

    protected Rigidbody rigid;

    private float _TimeSinceLastJump = 0f;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        Bunny = new Bunny(sex: Utils.RandomEnumValue<Sex>());
        Characteristics = Bunny.GetModifiedCharacteristics();
        rigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        _TimeSinceLastJump += Time.deltaTime;
        if (isGrounded && _TimeSinceLastJump > Characteristics["jumping_frequency"])
        {
            Jump();
        }
    }

    private void Jump()
    {
        Debug.Log("Jumping");
        _TimeSinceLastJump = 0f;
        rigid.AddForce((Quaternion.Euler(0, 90, 0) * transform.forward + new Vector3(0, 1, 0)) * Characteristics["jumping_strength"]);
        rigid.angularVelocity = Vector3.zero;
    }

    private void SetIsGrounded()
    {
        isGrounded = Physics.Raycast(
            origin: transform.position + (Vector3.up * 0.1f),
            direction: Vector3.down,
            maxDistance: 0.15f
        );
    }

}
