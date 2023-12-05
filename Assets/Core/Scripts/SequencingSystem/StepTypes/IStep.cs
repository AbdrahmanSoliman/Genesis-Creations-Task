using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStep
{
    public int Id {get; set;}
    public void BeginStep(Step step);
}
