using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    //what we are following
    public Transform target;

    //zeros out the velocity
    Vector3 velocity = Vector3.zero;

    //time to follow target
    public float smoothTime = .15f;

    //enable and set the maximum Y value
    public bool YMaxEnabled = false;
    public float YMaxValue = 0;

    //enable and set the min Y value
    public bool YMinEnabled = false;
    public float YMinValue = 0;

    //enable and set the max X value
    public bool XMaxEnabled = false;
    public float XMaxValue = 0;

    //enable and set the min X value
    public bool XMinEnabled = false;
    public float XMinValue = 0;

    private void FixedUpdate()
    {

        //target position
        Vector3 targetPos = target.position;

        //vertical
        if (YMinEnabled && YMaxEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue);
        else if (YMinEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
        else if (YMaxEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, YMaxValue);

        //horizontal
        if (XMinEnabled && XMaxEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
        else if (XMinEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
        else if (XMaxEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, XMaxValue);


        //allign the camera and the target z position
        targetPos.z = transform.position.z;

        //using smooth damp we will gradually change the camera transform position to the target position based on the cameras transform velocity and our smooth time
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
