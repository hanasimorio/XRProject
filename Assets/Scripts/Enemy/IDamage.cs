using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { Flame,Thunder,Water,Explosion}
public interface IDamage
{
    void ApplyDamage(float damage,List<Type> types);
}