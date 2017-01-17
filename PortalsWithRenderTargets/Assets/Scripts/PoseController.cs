/*using Tango;
using UnityEngine;

namespace Assets.Scripts
{
    public class PoseController : MonoBehaviour, ITangoPose
    {
        private TangoApplication _mTangoApplication; // Instance for Tango Client
        private Vector3 _mTangoPosition; // Position from Pose Callback
        private Quaternion _mTangoRotation; // Rotation from Pose Callback
        private Vector3 _mStartPosition; // Start Position of the camera
        private Vector3 _mLastPosition; // last position returned in Unity coordinates.

        // Controls movement scale, use 1.0f to be metric accurate
        // For the codelab, we adjust the scale so movement results in larger movements in the
        // virtual world.
        private float m_movementScale = 10.0f;

        // Use this for initialization
        void Start()
        {
            // Initialize some variables
            _mTangoRotation = Quaternion.identity;
            _mTangoPosition = Vector3.zero;
            _mLastPosition = Vector3.zero;
            _mStartPosition = transform.position;
            _mTangoApplication = FindObjectOfType<TangoApplication>();
            if (_mTangoApplication != null)
            {
                RequestPermissions();
            }
            else
            {
                Debug.Log("No Tango Manager found in scene.");
            }
        }

        // Permissions callback
        private void PermissionsCallback(bool success)
        {
            if (success)
            {
                _mTangoApplication.InitApplication(); // Initialize Tango Client
                _mTangoApplication.InitProviders(string.Empty); // Initialize listeners
                _mTangoApplication.ConnectToService(); // Connect to Tango Service
            }
            else
            {
                AndroidHelper.ShowAndroidToastMessage("Motion Tracking Permissions Needed", true);
            }

        }

        private void RequestPermissions()
        {
            // Request Tango permissions
            _mTangoApplication.RegisterPermissionsCallback(PermissionsCallback);
            _mTangoApplication.RequestNecessaryPermissionsAndConnect();
            _mTangoApplication.Register(this);
        }

        // Pose callbacks from Project Tango
        public void OnTangoPoseAvailable(Tango.TangoPoseData pose)
        {


            // Do nothing if we don't get a pose
            if (pose == null)
            {
                Debug.Log("TangoPoseData is null.");
                return;
            }
            // The callback pose is for device with respect to start of service pose.
            if (pose.framePair.baseFrame == TangoEnums.TangoCoordinateFrameType.TANGO_COORDINATE_FRAME_START_OF_SERVICE &&
                pose.framePair.targetFrame == TangoEnums.TangoCoordinateFrameType.TANGO_COORDINATE_FRAME_DEVICE)
            {
                if (pose.status_code == TangoEnums.TangoPoseStatusType.TANGO_POSE_VALID)
                {
                    // Cache the position and rotation to be set in the update function.
                    _mTangoPosition = new Vector3((float)pose.translation[0],
                        (float)pose.translation[1],
                        (float)pose.translation[2]);

                    _mTangoRotation = new Quaternion((float)pose.orientation[0],
                        (float)pose.orientation[1],
                        (float)pose.orientation[2],
                        (float)pose.orientation[3]);
                }
                else // if the current pose is not valid we set the pose to identity
                {
                    _mTangoPosition = Vector3.zero;
                    _mTangoRotation = Quaternion.identity;
                }
            }
        }

        /// <summary>
        /// Transforms the Tango pose which is in Start of Service to Device frame to Unity coordinate system.
        /// </summary>
        /// <returns>The Tango Pose in unity coordinate system.</returns>
        /// <param name="translation">Translation.</param>
        /// <param name="rotation">Rotation.</param>
        /// <param name="scale">Scale.</param>
        Matrix4x4 TransformTangoPoseToUnityCoordinateSystem(Vector3 translation,
            Quaternion rotation, Vector3 scale)
        {

            // Matrix for Tango coordinate frame to Unity coordinate frame conversion.
            // Start of service frame with respect to Unity world frame.
            Matrix4x4 m_uwTss;
            // Unity camera frame with respect to device frame.
            Matrix4x4 m_dTuc;

            m_uwTss = new Matrix4x4();
            m_uwTss.SetColumn(0, new Vector4(1.0f, 0.0f, 0.0f, 0.0f));
            m_uwTss.SetColumn(1, new Vector4(0.0f, 0.0f, 1.0f, 0.0f));
            m_uwTss.SetColumn(2, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));
            m_uwTss.SetColumn(3, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));

            m_dTuc = new Matrix4x4();
            m_dTuc.SetColumn(0, new Vector4(1.0f, 0.0f, 0.0f, 0.0f));
            m_dTuc.SetColumn(1, new Vector4(0.0f, 1.0f, 0.0f, 0.0f));
            m_dTuc.SetColumn(2, new Vector4(0.0f, 0.0f, -1.0f, 0.0f));
            m_dTuc.SetColumn(3, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));

            Matrix4x4 ssTd = Matrix4x4.TRS(translation, rotation, scale);
            return m_uwTss * ssTd * m_dTuc;
        }


        // FixedUpdate is called at a fixed rate
        void FixedUpdate()
        {
            // Convert position and rotation from Tango's coordinate system to Unity's.
            Matrix4x4 uwTuc = TransformTangoPoseToUnityCoordinateSystem(_mTangoPosition,
                _mTangoRotation, Vector3.one);
            Vector3 newPosition = (uwTuc.GetColumn(3)) * m_movementScale;
            Quaternion newRotation = Quaternion.LookRotation(uwTuc.GetColumn(2),
                uwTuc.GetColumn(1));

            // Calculate the difference in the poses received.  This allows us
            // to recover when we hit something in the virtual world.
            Vector3 delta = newPosition - _mLastPosition;
            _mLastPosition = newPosition;
            Vector3 destination = rigidbody.position + delta;
            Vector3 vectorToTargetPosition = destination - transform.position;
            // If there is motion, move the player around the scene.
            if (vectorToTargetPosition.magnitude > 0.1f)
            {
                vectorToTargetPosition.Normalize();
                // Set the movement vector based on the axis input.
                Vector3 movement = vectorToTargetPosition;
                // Normalise the movement vector and make it proportional to the speed per second.
                movement = movement.normalized * 5f * Time.deltaTime;

                // Move the player to it's current position plus the movement.
                rigidbody.MovePosition(transform.position + movement);
            }
            else
            {
                rigidbody.velocity = Vector3.zero;
            }
            // always rotate, even if we don't move.
            rigidbody.MoveRotation(newRotation);
            // finally, let the game manager know the position of the player.
            // GameManager.Instance.PlayerPosition = transform.position;
        }
    }
}*/