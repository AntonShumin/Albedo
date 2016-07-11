using UnityEngine;
using System.Collections;

public class script_Movement : MonoBehaviour {

    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;

    private string m_MovementAxisName;
    private string m_TurnAxisXName;
    private string m_TurnAxisYName;
    private string m_TurnAxisZName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValueX; //pitch
    private float m_TurnInputValueY; //yaw
    private float m_TurnInputValueZ; //rot
    private float m_OriginalPitch;

	// Use this for initialization
	void Awake ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
	}

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValueX = 0f;
        m_TurnInputValueY = 0f;
        m_TurnInputValueZ = 0f;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Start()
    {
        m_MovementAxisName = "Vertical";
        m_TurnAxisXName = "Pitch";
        m_TurnAxisYName = "Horizontal";
        m_TurnAxisZName = "Rotate";

        m_OriginalPitch = m_MovementAudio.pitch;
    }

    private void Update()
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValueX = Input.GetAxis(m_TurnAxisXName);
        m_TurnInputValueY = Input.GetAxis(m_TurnAxisYName);
        m_TurnInputValueZ = Input.GetAxis(m_TurnAxisZName);
        EngineAudio();
    }

    private void EngineAudio()
    {
        float total_rotation = Mathf.Abs(m_TurnInputValueX) + Mathf.Abs(m_TurnInputValueY) + Mathf.Abs(m_TurnInputValueZ);
        if (Mathf.Abs(m_MovementInputValue) < -0.1f && total_rotation < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void Turn()
    {
        float turnY = m_TurnInputValueY * m_TurnSpeed * Time.deltaTime;
        float turnX = m_TurnInputValueX * m_TurnSpeed * Time.deltaTime;
        float turnZ = m_TurnInputValueZ * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(turnX, turnY, turnZ);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }


}
