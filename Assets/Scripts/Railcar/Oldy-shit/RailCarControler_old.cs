using Nectunia.Utility;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class RailCarControler : MonoBehaviour{

	#region ATTRIBUTES _________________________________________________________
    private CharacterController _railCarController;
    public GameObject _checkpoint1;
    public GameObject _checkpoint2;
    public Vector3 _forward;

    public float _speed = 5f;
    public float _rotateSpeed = 0.5f;
    public float _delayToRotate;

    private CountDown _countDown;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;

    // Bezier calculation
    private float BezierTime = 0;
    private float ControlPointX ;
    private float ControlPointZ ;
	#endregion

	
	#region EVENTS _____________________________________________________________
	void Start (){
		this._railCarController = gameObject.GetComponent<CharacterController>();
        this._targetPosition = this._checkpoint1.transform.position;
        this._countDown = new CountDown(this._delayToRotate);
        this._countDown.start();
        this._startPosition = this.transform.position; 
        ControlPointX = this._startPosition.x;
        ControlPointZ = this._targetPosition.z;
    }


    void Update(){
        // Rotate to Target position
        //if (this._countDown.IsUp) { this.RotateToTarget(); }

        this.RotateToTarget2();
		
		// Move the RailCar forward
		_railCarController.Move( this.transform.forward * Time.deltaTime * _speed);
        this._forward = this.transform.forward;
    }

	void OnTriggerEnter (Collider other) {
		if(other.gameObject == this._checkpoint1) { 
            this._targetPosition = this._checkpoint2.transform.position;
            this._startPosition = this.transform.position;
            if(this.transform.forward.x > 0.5) {            
                ControlPointX = this._startPosition.x;
                ControlPointZ = this._targetPosition.z;
		    } else {            
                ControlPointX = this._targetPosition.x;
                ControlPointZ = this._startPosition.z;
		    }
            BezierTime = 0;
            this._countDown.reset();
            this._countDown.start();
        }

		if(other.gameObject == this._checkpoint2) { 
            this._targetPosition = this._checkpoint1.transform.position;
            this._startPosition = this.transform.position;
            if(this.transform.forward.x > 0.5) {            
                ControlPointX = this._startPosition.x;
                ControlPointZ = this._targetPosition.z;
		    } else {            
                ControlPointX = this._targetPosition.x;
                ControlPointZ = this._startPosition.z;
		    }
            BezierTime = 0;
            this._countDown.reset();
            this._countDown.start();
        }
        
	}
	#endregion


	#region METHODS ____________________________________________________________
	private void RotateToTarget () {
        // Determine which direction to rotate towards
        Vector3 targetDirection = this._targetPosition - this.transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = this._rotateSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, targetDirection, singleStep, 0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        this.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void RotateToTarget2 () {
        float CurveX;
        float CurveZ;
        if (BezierTime < 2) { BezierTime = BezierTime + Time.deltaTime * this._rotateSpeed ; }     
     
        CurveX = (((1-BezierTime)*(1-BezierTime)) * this._startPosition.x) + (2 * BezierTime * (1 - BezierTime) * ControlPointX) + ((BezierTime * BezierTime) * this._targetPosition.x);
        CurveZ = (((1-BezierTime)*(1-BezierTime)) * this._startPosition.z) + (2 * BezierTime * (1 - BezierTime) * ControlPointZ) + ((BezierTime * BezierTime) * this._targetPosition.z);
        this.transform.rotation = Quaternion.LookRotation(new Vector3(CurveX, 0, CurveZ));     
    }
	#endregion
}
