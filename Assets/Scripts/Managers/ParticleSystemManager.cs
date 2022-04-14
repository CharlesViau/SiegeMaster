using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;

public enum ParticleType {Blood ,Smoke}
public class ParticleSystemManager : Manager<ParticleSystemSc, ParticleType, ParticleSystemSc.Args, ParticleSystemManager>
{
    protected override string PrefabLocation => "Prefabs/ParticleSystems/";
}
