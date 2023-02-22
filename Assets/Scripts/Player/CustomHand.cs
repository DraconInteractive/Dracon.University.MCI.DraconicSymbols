/************************************************************************************

Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.  

See SampleFramework license.txt for license terms.  Unless required by applicable law 
or agreed to in writing, the sample code is provided “AS IS” WITHOUT WARRANTIES OR 
CONDITIONS OF ANY KIND, either express or implied.  See the license for specific 
language governing permissions and limitations under the license.

************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using OVRTouchSample;
#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

public class CustomHand : MonoBehaviour
{
    public const string ANIM_LAYER_NAME_POINT = "Point Layer";
    public const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
    public const string ANIM_PARAM_NAME_FLEX = "Flex";
    public const string ANIM_PARAM_NAME_POSE = "Pose";
    public const float THRESH_COLLISION_FLEX = 0.9f;

    public const float INPUT_RATE_CHANGE = 20.0f;

    public const float COLLIDER_SCALE_MIN = 0.01f;
    public const float COLLIDER_SCALE_MAX = 1.0f;
    public const float COLLIDER_SCALE_PER_SECOND = 1.0f;

    public const float TRIGGER_DEBOUNCE_TIME = 0.05f;
    public const float THUMB_DEBOUNCE_TIME = 0.15f;

    [SerializeField]
    private OVRInput.Controller m_controller = OVRInput.Controller.None;
    [SerializeField]
    private Animator m_animator = null;
    [SerializeField]
    private HandPose m_defaultGrabPose = null;
    private Interactor m_interactor = null;
    List<Renderer> m_showAfterInputFocusAcquired;

    private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexFlex = -1;
    private int m_animParamIndexPose = -1;

    private bool m_isPointing = false;
    private bool m_isGivingThumbsUp = false;
    private float m_pointBlend = 0.0f;
    private float m_thumbsUpBlend = 0.0f;

    private bool m_restoreOnInputAcquired = false;


    private void Start()
    {
        if (m_controller == OVRInput.Controller.LTouch)
        {
            m_interactor = Interactor.left;
        }
        else if (m_controller == OVRInput.Controller.RTouch)
        {
            m_interactor = Interactor.right;
        }

        m_showAfterInputFocusAcquired = new List<Renderer>();

        // Get animator layer indices by name, for later use switching between hand visuals
        m_animLayerIndexPoint = m_animator.GetLayerIndex(ANIM_LAYER_NAME_POINT);
        m_animLayerIndexThumb = m_animator.GetLayerIndex(ANIM_LAYER_NAME_THUMB);
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        m_animParamIndexPose = Animator.StringToHash(ANIM_PARAM_NAME_POSE);

        OVRManager.InputFocusAcquired += OnInputFocusAcquired;
        OVRManager.InputFocusLost += OnInputFocusLost;
#if UNITY_EDITOR
        OVRPlugin.SendEvent("custom_hand", (SceneManager.GetActiveScene().name == "CustomHands").ToString(), "sample_framework");
#endif
    }

    private void OnDestroy()
    {
        OVRManager.InputFocusAcquired -= OnInputFocusAcquired;
        OVRManager.InputFocusLost -= OnInputFocusLost;
    }

    private void Update()
    {
        UpdateCapTouchStates();

        m_pointBlend = InputValueRateChange(m_isPointing, m_pointBlend);
        m_thumbsUpBlend = InputValueRateChange(m_isGivingThumbsUp, m_thumbsUpBlend);

        float flex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);

        UpdateAnimStates();
    }

    // Just checking the state of the index and thumb cap touch sensors, but with a little bit of
    // debouncing.
    private void UpdateCapTouchStates()
    {
        m_isPointing = !OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, m_controller);
        m_isGivingThumbsUp = !OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, m_controller);
    }

    // Simple Dash support. Just hide the hands.
    private void OnInputFocusLost()
    {
        if (gameObject.activeInHierarchy)
        {
            m_showAfterInputFocusAcquired.Clear();
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; ++i)
            {
                if (renderers[i].enabled)
                {
                    renderers[i].enabled = false;
                    m_showAfterInputFocusAcquired.Add(renderers[i]);
                }
            }

            m_restoreOnInputAcquired = true;
        }
    }

    private void OnInputFocusAcquired()
    {
        if (m_restoreOnInputAcquired)
        {
            for (int i = 0; i < m_showAfterInputFocusAcquired.Count; ++i)
            {
                if (m_showAfterInputFocusAcquired[i])
                {
                    m_showAfterInputFocusAcquired[i].enabled = true;
                }
            }
            m_showAfterInputFocusAcquired.Clear();

            m_restoreOnInputAcquired = false;
        }
    }

    private float InputValueRateChange(bool isDown, float value)
    {
        float rateDelta = Time.deltaTime * INPUT_RATE_CHANGE;
        float sign = isDown ? 1.0f : -1.0f;
        return Mathf.Clamp01(value + rateDelta * sign);
    }

    private void UpdateAnimStates()
    {
        bool grabbing = m_interactor.state == Interactor.State.Holding;
        HandPose grabPose = m_defaultGrabPose;
        if (grabbing)
        {
            HandPose customPose = m_interactor.Target.GetComponent<HandPose>();
            if (customPose != null) grabPose = customPose;
        }
        // Pose
        HandPoseId handPoseId = grabPose.PoseId;
        m_animator.SetInteger(m_animParamIndexPose, (int)handPoseId);

        // Flex
        // blend between open hand and fully closed fist
        float flex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);
        m_animator.SetFloat(m_animParamIndexFlex, flex);

        // Point
        bool canPoint = !grabbing || grabPose.AllowPointing;
        float point = canPoint ? m_pointBlend : 0.0f;
        m_animator.SetLayerWeight(m_animLayerIndexPoint, point);

        // Thumbs up
        bool canThumbsUp = !grabbing || grabPose.AllowThumbsUp;
        float thumbsUp = canThumbsUp ? m_thumbsUpBlend : 0.0f;
        m_animator.SetLayerWeight(m_animLayerIndexThumb, thumbsUp);

        float pinch = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller);
        m_animator.SetFloat("Pinch", pinch);
    }
}

