using UnityEngine;
using System;
using System.Collections;

public class CubemanController : MonoBehaviour 
{
	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("Whether the cubeman is allowed to move vertically or not.")]
	public bool verticalMovement = true;

	[Tooltip("Whether the cubeman is facing the player or not.")]
	public bool mirroredMovement = false;

	[Tooltip("Rate at which the cubeman will move through the scene.")]
	public float moveRate = 1f;

    public float yposition = 0.0f;

    public GameObject Hip_Center;
	public GameObject Spine;
	public GameObject Neck;
	public GameObject Head;
	public GameObject Shoulder_Left;
	public GameObject Elbow_Left;
	public GameObject Wrist_Left;
	public GameObject Hand_Left;
	public GameObject Shoulder_Right;
	public GameObject Elbow_Right;
	public GameObject Wrist_Right;
	public GameObject Hand_Right;
	public GameObject Hip_Left;
	public GameObject Knee_Left;
	public GameObject Ankle_Left;
	public GameObject Foot_Left;
	public GameObject Hip_Right;
	public GameObject Knee_Right;
	public GameObject Ankle_Right;
	public GameObject Foot_Right;
	public GameObject Spine_Shoulder;
    public GameObject Hand_Tip_Left;
    public GameObject Thumb_Left;
    public GameObject Hand_Tip_Right;
    public GameObject Thumb_Right;
	
	public LineRenderer skeletonLine;
	public LineRenderer debugLine;

	private GameObject[] bones;
	private LineRenderer[] lines;

	private LineRenderer lineTLeft;
	private LineRenderer lineTRight;
	private LineRenderer lineFLeft;
	private LineRenderer lineFRight;

    private Vector3 initialPosition;
	private Quaternion initialRotation;

    private Vector3 offsetPosition = new Vector3(0.0f, -1000.0f, 0.0f);

    void Start () 
	{
		//store bones in a list for easier access
		bones = new GameObject[] {
			Hip_Center,
            Spine,
            Neck,
            Head,
            Shoulder_Left,
            Elbow_Left,
            Wrist_Left,
            Hand_Left,
            Shoulder_Right,
            Elbow_Right,
            Wrist_Right,
            Hand_Right,
            Hip_Left,
            Knee_Left,
            Ankle_Left,
            Foot_Left,
            Hip_Right,
            Knee_Right,
            Ankle_Right,
            Foot_Right,
            Spine_Shoulder,
            Hand_Tip_Left,
            Thumb_Left,
            Hand_Tip_Right,
            Thumb_Right
		};

        initialRotation = transform.rotation;

        lines = new LineRenderer[bones.Length];
	}
	
    public void ResetAt(Vector3 position)
    {
        KinectManager manager = KinectManager.Instance;
        Vector3 posPointMan = manager.GetUserPosition(manager.GetPrimaryUserID());

        position.z = -position.z;

        offsetPosition = position - posPointMan;
        offsetPosition.z = -offsetPosition.z;
        offsetPosition.y = 0.0f;

        yposition = 0.0f;
    }

    void Update () 
	{
        KinectManager manager = KinectManager.Instance;

        if (manager.GetUsersCount() == 0) return;
        if (Input.GetKey(KeyCode.W)) yposition += 0.1f * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) yposition -= 0.1f * Time.deltaTime;

        Int64 userID = manager.GetPrimaryUserID();
		
		Vector3 posPointMan = manager.GetUserPosition(manager.GetPrimaryUserID());
		Vector3 posPointManMP = new Vector3(posPointMan.x, posPointMan.y, !mirroredMovement ? -posPointMan.z : posPointMan.z);
		        
        Vector3 Relpos = posPointMan;
        Relpos.z = -Relpos.z;

        offsetPosition.y = yposition;

        transform.position = (Relpos * moveRate) + offsetPosition;
        //Debug.Log("Von Kinect: " + Relpos + " - Offset: " + offsetPosition + " =  Endposition: " + transform.position + " = Indikator: " + indicator.transform.position);
       
        // update the local positions of the bones
        for (int i = 0; i < bones.Length; i++) 
		{
			if(bones[i] != null)
			{
				int joint = !mirroredMovement ? i : (int)KinectInterop.GetMirrorJoint((KinectInterop.JointType)i);

				if(joint < 0)
					continue;
				
				if(manager.IsJointTracked(userID, joint))
				{
					bones[i].gameObject.SetActive(true);
					
					Vector3 posJoint = manager.GetJointPosition(userID, joint);
					posJoint.z = !mirroredMovement ? -posJoint.z : posJoint.z;
					
					Quaternion rotJoint = manager.GetJointOrientation(userID, joint, !mirroredMovement);
					rotJoint = initialRotation * rotJoint;

					posJoint -= posPointManMP;
					
					if(mirroredMovement)
					{
						posJoint.x = -posJoint.x;
						posJoint.z = -posJoint.z;
					}

					bones[i].transform.localPosition = posJoint;
					bones[i].transform.rotation = rotJoint;
					
					if(lines[i] == null && skeletonLine != null) 
					{
						lines[i] = Instantiate((i == 22 || i == 24) && debugLine ? debugLine : skeletonLine) as LineRenderer;
						lines[i].transform.parent = transform;
					}

					if(lines[i] != null)
					{
						lines[i].gameObject.SetActive(true);
						Vector3 posJoint2 = bones[i].transform.position;
						
						Vector3 dirFromParent = manager.GetJointDirection(userID, joint, false, false);
						dirFromParent.z = !mirroredMovement ? -dirFromParent.z : dirFromParent.z;
						Vector3 posParent = posJoint2 - dirFromParent;
						
						//lines[i].SetVertexCount(2);
						lines[i].SetPosition(0, posParent);
						lines[i].SetPosition(1, posJoint2);
					}

				}
				else
				{
					bones[i].gameObject.SetActive(false);
					
					if(lines[i] != null)
					{
						lines[i].gameObject.SetActive(false);
					}
				}
			}	
		}
	}

}
