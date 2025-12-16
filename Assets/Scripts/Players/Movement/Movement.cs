using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class Movement : MovementMain
{
    protected override void WalkPerformed(InputAction.CallbackContext ctx)
    {
        base.WalkPerformed(ctx);

        if (walkInput.y < 0)
        {
            print("going left");
            charVisual.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (walkInput.y > 0)
        {
            print("going right");
            charVisual.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}

