using UnityEngine;
using System.Collections;

public class Building : Bolt.EntityBehaviour<IBuildingState> {
  public override void Attached() {
    state.Transform.SetTransforms(transform);
  }
}
