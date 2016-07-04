using UnityEngine;
using System.Collections;

public class RobotBehaviour : Bolt.EntityBehaviour<IRobotState>
{
    public override void Attached() //equivalent du start
    {
        state.Transform.SetTransforms(transform);
        state.SetAnimator(GetComponent<Animator>());
        state.Animator.applyRootMotion = entity.isOwner;
        base.Attached();
    }

    public override void SimulateOwner()//equivalent du update
    {
        var speed = state.Speed;
        var angularSpeed = state.AngularSpeed;

        if (Input.GetKey(KeyCode.Z))
        {
            speed += 0.025f;
        }
        else {
            speed -= 0.025f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            angularSpeed -= 0.025f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            angularSpeed += 0.025f;
        }
        else {
            if (angularSpeed < 0)
            {
                angularSpeed += 0.025f;
                angularSpeed = Mathf.Clamp(angularSpeed, -1f, 0);
            }
            else if (angularSpeed > 0)
            {
                angularSpeed -= 0.025f;
                angularSpeed = Mathf.Clamp(angularSpeed, 0, +1f);
            }
        }

        state.Speed = Mathf.Clamp(speed, 0f, 1.5f);
        state.AngularSpeed = Mathf.Clamp(angularSpeed, -1f, +1f);   

        base.SimulateOwner();
    }
}
