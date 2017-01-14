using System.Collections;

using UnityEngine;

public class BaseObject : Bolt.EntityBehaviour<IBaseObjectState>
{
  public override void Attached()
  {
    state.Name = name;
    state.AddCallback("Name", NameChanged);

    base.Attached();
  }

  private void NameChanged()
  {
    name = state.Name;
  }
}
