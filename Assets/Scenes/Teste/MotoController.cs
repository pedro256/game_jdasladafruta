using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoController : MonoBehaviour
{
    
    public WheelCollider FrontWheel, BackWhell;
    public Rigidbody rb;
    public float MotorForce, SteerForce, BrakeForce;
    public float BalanceForce; // Força de equilíbrio para corrigir a inclinação
    public float StillThreshold = 0.1f;
    public bool playerInside = false;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void MoveBike()
    {
        float v = Input.GetAxis("Vertical") * MotorForce;
        float h = Input.GetAxis("Horizontal") * SteerForce;


        BackWhell.motorTorque = v;
        FrontWheel.steerAngle = h;


        if (Input.GetKey(KeyCode.Space))
        {
            BackWhell.brakeTorque = BrakeForce;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BackWhell.brakeTorque = 0;
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            BackWhell.brakeTorque = BrakeForce;
        }
        else
        {
            BackWhell.brakeTorque = 0;
        }


        //var targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);

        //// Verificar se a moto está inclinada
        if (Mathf.Abs(transform.rotation.eulerAngles.z) > 60f)
        {
            // Calcular o ângulo de inclinação desejado como 0
            float targetAngle = 0f;

            // Calcular a diferença entre o ângulo atual e o ângulo de inclinação desejado
            float angleDiff = Mathf.DeltaAngle(transform.rotation.eulerAngles.z, targetAngle);

            // Calcular o torque corretivo
            float balanceTorque = angleDiff * BalanceForce;



            if (transform.rotation.eulerAngles.z > 0f)
            {
                balanceTorque = -balanceTorque;
            }


            Debug.Log("Força: " + balanceTorque);

            // Aplicar o torque corretivo no rigidbody
            GetComponent<Rigidbody>().AddTorque(Vector3.back * balanceTorque);
        }
        //if (Mathf.Abs(transform.rotation.eulerAngles.z) > 80f)
        //{
        //    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside)
        {
            MoveBike();
        }
    }

    public void PlayerInteractionBike(bool insideBike)
    {
        playerInside = insideBike;

    }
}
