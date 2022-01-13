using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    //Current speed to refer to for traveling
    float Speed = 1.5f;

    /// <summary>
    /// Current speed refrence to this tile object
    /// </summary>
    public float _currentSpeed
    {
        get
        {
            return Speed;
        }
        set
        {
            Speed = value;
        }
    }

    public void _modifySpeed(float speed)
    {
        Speed = speed;
    }


}
