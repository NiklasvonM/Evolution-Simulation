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

    private void RotateRigidBodyRandomly()
    {
        float mean = 0f; // Mean of the distribution (centered at 0 degrees)
        float stdDev = 30f; // Standard deviation (controls the spread of the distribution)
        float randomAngle = RandomNormal(mean, stdDev);

        // Apply rotation
        Quaternion rotation = Quaternion.Euler(0, randomAngle, 0);
        rigid.MoveRotation(rigid.rotation * rotation);
    }

    // Function to generate a normally distributed random number
    private float RandomNormal(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value; // Uniform(0,1] random doubles
        float u2 = 1.0f - Random.value;
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                              Mathf.Sin(2.0f * Mathf.PI * u2); // Random normal(0,1)
        return mean + stdDev * randStdNormal; // Random normal(mean,stdDev^2)
    }

    private void Jump()
    {
        Debug.Log("Jumping");
        _TimeSinceLastJump = 0f;
        // Turn in a random direction. The probability density is highest for 0 degrees and falls off to +-180 degrees.
        RotateRigidBodyRandomly();

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
