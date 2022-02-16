using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attacks { Flame, Slash }
public interface IPlayerDamage
{
    void ApplyDamage(float damage, List<Attacks> attacks);
}