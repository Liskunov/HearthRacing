using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace TheKiwiCoder
{
    [System.Serializable]
    public class MassSearch : ActionNode
    {
        private Rigidbody BoxMass;
        public float MaxMass;

        protected override void OnStart()
        {
            BoxMass = context.body;
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            if (BoxMass.mass > MaxMass)
            {
                return State.Success;
            }

            return State.Failure;
        }
    }
}