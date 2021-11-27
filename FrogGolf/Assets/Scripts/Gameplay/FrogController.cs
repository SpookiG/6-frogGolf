using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public float PredictionStepSize = 5f;

    private Rigidbody _bod;
    private CameraController _camController;
    private LineRenderer _lineRenderer;

    private bool _stuck;
    private Vector3 _shotVelocity;
    private Vector3 _shotAccelleration;

    private Vector3 _startPos;
    private Quaternion _startRot;
    private float _timeSinceShot;

    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.toggleControls += ToggleControls;
        SequenceManager.Current.Events.resetLevel += ResetFrog;
        _bod = GetComponent<Rigidbody>();
        _camController = Camera.main.GetComponent<CameraController>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();

        _bod.isKinematic = true;
        _stuck = true;
        
        _startPos = transform.position;
        _startRot = transform.rotation;
        _timeSinceShot = 0;

        enabled = false;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.toggleControls -= ToggleControls;
        SequenceManager.Current.Events.resetLevel -= ResetFrog;
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

    void ResetFrog()
    {
        _bod.isKinematic = true;
        _stuck = true;

         transform.position = _startPos;
         transform.rotation = _startRot;
        _timeSinceShot = 0;

        _camController.ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_bod.detectCollisions)
        {
            _timeSinceShot += Time.deltaTime;

            if (_timeSinceShot >= .03f)
            {
                _bod.detectCollisions = true;
            }

            
        }
        

        // if stuck you can take a swing with the mouse
        // if not you can move the camera but no swing

        if (_stuck)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // lock camera controls
                _camController.enabled = false;

                _shotVelocity = (_camController.transform.forward * 15) + (Vector3.up * 20);

                

                // display shot prediction
                DisplayShotPrediction();
                _lineRenderer.enabled = true;
            }

            if (Input.GetMouseButton(0))
            {
                // edit shot velocity etc
                if (Vector3.Angle(_camController.transform.up, Vector3.up) <= 90)
                {
                    _shotVelocity += Vector3.up * Input.GetAxis("Mouse Y");
                    
                }
                else
                {
                    _shotVelocity -= Vector3.up * Input.GetAxis("Mouse Y");
                }

                _shotVelocity += _camController.transform.right * Input.GetAxis("Mouse X");



                // display shot prediction
                DisplayShotPrediction();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _lineRenderer.enabled = false;

                // free camera controls
                _camController.enabled = true;

                // apply shot velocity
                _bod.isKinematic = false;
                _bod.velocity = _shotVelocity;
                _bod.angularVelocity = -_camController.transform.right * _shotVelocity.magnitude * .2f;                                                     // roll backwards
                _bod.angularVelocity += _camController.transform.forward * _camController.transform.InverseTransformDirection(_shotVelocity).x * .3f;       // roll sideways based on angle of hit

                // change frog state
                _stuck = false;

                _bod.detectCollisions = false;
                _timeSinceShot = 0f;
                
            }
        }
        

        if (Input.GetButtonDown("Reset"))
        {
            SequenceManager.Current.CallSequence(SequenceManager.Current.Sequences.Restart);
        }
        else if (transform.position.y < 0)
        {
            SequenceManager.Current.CallSequence(SequenceManager.Current.Sequences.Die);
        }
    }

    private void FixedUpdate()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        //_camController.CollisionNormal = _collisionNormal;
        _bod.isKinematic = true;
        _stuck = true;

        //transform.position += _collisionNormal * .05f;
    }


    // I forgot LineRenderer is terrible and just twists around for optimisation reasons making it p much unusable
    // and it doesn't have an option to switch off the optimization?
    // oh well, it'll do for this game, just try making a custom one next game
    private void DisplayShotPrediction()
    {
        Vector3[] positions = new Vector3[_lineRenderer.positionCount];
        Vector3 prevPos = new Vector3(0, 0, 0);

        for (int i = 0; i < positions.Length; i++)
        {
            //positions[i] = transform.position + (i * PredictionStepSize * i * PredictionStepSize * ((.5f * Physics.gravity * i * PredictionStepSize) + _shotVelocity));
            positions[i].x = _shotVelocity.x * i * PredictionStepSize;
            positions[i].y = _shotVelocity.y * i * PredictionStepSize + .5f * Physics.gravity.y * i * PredictionStepSize * i * PredictionStepSize;
            positions[i].z = _shotVelocity.z * i * PredictionStepSize;

            positions[i] += transform.position;
            //prevPos = positions[i];
        }

        _lineRenderer.SetPositions(positions);
    }
}
