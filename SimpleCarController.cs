using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    public WheelCollider frontDriverWheelCollider;
    public WheelCollider frontPassengerWheelCollider;
    public WheelCollider rearDriverWheelCollider;
    public WheelCollider rearPassengerWheelCollider;
    public Transform frontDriverWheelTransform;
    public Transform frontPassengerWheelTransform;
    public Transform rearDriverWheelTransform;
    public Transform rearPassengerWheelTransform;

    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;


    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontDriverWheelCollider.steerAngle = steerAngle;
        frontPassengerWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        rearDriverWheelCollider.motorTorque = verticalInput * motorForce;
        rearPassengerWheelCollider.motorTorque = verticalInput * motorForce;

        brakeForce = isBreaking ? 3000f : 0f;
        frontDriverWheelCollider.brakeTorque = brakeForce;
        frontPassengerWheelCollider.brakeTorque = brakeForce;
        rearDriverWheelCollider.brakeTorque = brakeForce;
        rearPassengerWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontDriverWheelCollider, frontDriverWheelTransform);
        UpdateWheelPos(frontPassengerWheelCollider, frontPassengerWheelTransform);
        UpdateWheelPos(rearDriverWheelCollider, rearDriverWheelTransform);
        UpdateWheelPos(rearPassengerWheelCollider, rearPassengerWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

}