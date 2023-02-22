using HexabodyVR.PlayerController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraconHexaInputWrapper : HexaBodyInputWrapper
{
    protected override void Update()
    {
        PlayerInputs.CanMove = CanMove;
        if (!CanMove)
        {
            PlayerInputs.RecalibratePressed = false;
            PlayerInputs.SprintingPressed = false;
            PlayerInputs.MovementAxis = Vector2.zero;
            PlayerInputs.TurnAxis = Vector2.zero;
            PlayerInputs.CrouchPressed = false;
            PlayerInputs.StandPressed = false;
            PlayerInputs.JumpPressed = false;
            return;
        }
        PlayerInputs.RecalibratePressed = OVRInput.GetDown(OVRInput.Button.Start, OVRInput.Controller.LTouch);

        PlayerInputs.SprintingPressed = GetSprinting();

        PlayerInputs.MovementAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        PlayerInputs.TurnAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

        PlayerInputs.CrouchPressed = GetCrouch();
        PlayerInputs.StandPressed = GetStand();
        PlayerInputs.JumpPressed = GetJump();
    }

    protected override bool GetCrouch()
    {
        return false;
    }

    protected override bool GetJump()
    {
        return OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch);
    }

    protected override bool GetSprinting()
    {
        return false;
    }

    protected override bool GetStand()
    {
        return false;
    }
}

