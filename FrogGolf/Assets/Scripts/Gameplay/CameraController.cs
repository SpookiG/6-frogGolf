using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 TurningSpeed = new Vector2(200f, 50f);

    private Vector3 _startPos;
    private Quaternion _startRot;

    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.toggleControls += ToggleControls;

        _startPos = transform.position;
        _startRot = transform.rotation;
        enabled = false;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.toggleControls -= ToggleControls;
    }

    void ToggleControls(string controlMode)
    {
        if (controlMode == "player")
        {
            enabled = true;
        }
        else
        {
            enabled = false;
        }
    }

    public void ResetCamera()
    {
        transform.position = _startPos;
        transform.rotation = _startRot;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.up, -Input.GetAxis("Horizontal") * TurningSpeed.x * Time.deltaTime);
        transform.RotateAround(transform.parent.position, transform.right, Input.GetAxis("Vertical") * TurningSpeed.y * Time.deltaTime);        // not going to limit vertical rotation in case the frog lands upside down lol
    }
}
