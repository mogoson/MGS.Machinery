==========================================================================
  Copyright © 2017-2020 Mogoson. All rights reserved.
  Name: MGS-Machinery
  Author: Mogoson   Version: 1.0.0   Date: 3/22/2020
==========================================================================
  [Summary]
    Unity plugin for binding machinery joint in scene.
--------------------------------------------------------------------------
  [Demand]
    Binding rotate joints, example: upcar of crane, external gearing and
    inner gearing.

    Binding slider joints, example: big arm and landing legs of crane,
    waist articulation of road roller.

    Binding hydraulic cylinder.

    Binding dynamic spring.

    Binding crank roker, example: scraper bucket of loader, bucket of
    excavator.

    Binding crank slider, example: reciprocating engine, aircraft
    planetary engine.

    Binding complete construction machinery, example crane, road roller,
    loader, grader and excavator.

    Binding mesh Gear.

    Binding worm gear.

    Binding belt flywheel.

    Binding vibrator.

    Binding differential.

    Binding transmission.
--------------------------------------------------------------------------
  [Environment]
    Unity 5.0 or above.
    .Net Framework 3.0 or above.
--------------------------------------------------------------------------
  [Achieve]
    Mechanism : Define abstract joint, hinge and mechanism.

    FreeCrank : Free rotate around Z axis.

    GearCrank : Free rotate around Z axis, can be drived by linear velocity.

    LimitCrank : Rotate around Z axis in the angle range.

    CrankRocker : Crank rocker mechanism.

    CrankSlider : Crank slider mechanism.

    RockerHinge : Hinge of roker, rotate around the axis follow roker.

    RockerJoint : Roker joint, always look at target joint.

    RockerLimiter : Limiter of roker, limit the distance of a pair rokers.

    RockerRivet : Rivet of roker, keep the same position(world space) as
    the target joint.

    RockerSpring : Rocker spring look at joint.

    Slider : Slider joint.

    SliderArm : Sequence slider arm, drive from first joint to last joint.

    Synchronizer : Synchronous mechanisms, drive multi mechanisms at
    the same time.

    Transmission : Differential mechanisms, drive multi mechanisms
    by ratio velocity at the same time.

    MechanismDriver : Driver for test mechanism quickly.

    Gear : Gear rotate around axis Z.

    Axle : Axle rotate around axis Z.

    CoaxeGear : Coaxe gear with the same axis as another gear.

    WormGear : Worm gear mechanism.

    WormShaft : Worm shaft mechanism.

    Belt : Move texture UV on X axis.

    LinearVibrator : Reciprocating motion on Z axis.

    CentrifugalVibrator : Eccentric motion around Z axis.

    Engine : Unified engine drive all mechanisms. 

    Differential : Ordinary differential.
--------------------------------------------------------------------------
  [Usage]
    Reference the prefabs and demos to binding machinery joint in your
    project and use the components.
    
    If you start binding CrankRocker or CrankSlider, you can select the
    "Free" mode of "Hinge Editor" in the lower right corner of Unity
    Scene window to config the joints and change their position and
    rotation.

    If you have binding a CrankRocker or CrankSlider and you need to fine
    tune it, you can select the "Hinge" mode of "Hinge Editor" to modify
    base on binding state.

    Always select the "Lock" mode of "Hinge Editor" when you finish
    binding a CrankRocker or CrankSlider.

    You can attach the MechanismDriver component to a mechanism to
    test it when you finished binding.
--------------------------------------------------------------------------
  [Suggest]
    The MechanismDriver component only used to test drive mechanisms
    in demos. you can use it to test your binding mechanisms, but should
    not use in your project development.

    In fact, you should write a controller component for your machinery
    to unified control all the mechanisms. example: write CraneController
    component for crane to manage and drive the upcar, big arm and
    landing legs.

    The radius of gear should be set precisely.

    UV of belt model should be transverse arrangement, the texture of
    belt is preferably all sides continuous.

    Make sure the gear engages perfectly with the worm when building
    model.

    The amplitude radius of CentrifugalVibrator or LinearVibrator
    usually set a small value.
--------------------------------------------------------------------------
  [Demo]
    Prefabs in the path "MGS-Machinery/Prefabs" provide reference to you.
    Demos in the path "MGS-Machinery/Scenes" provide reference to you.
--------------------------------------------------------------------------
  [Resource]
    https://github.com/mogoson/MGS-Machinery.
--------------------------------------------------------------------------
  [Contact]
    If you have any questions, feel free to contact me at mogoson@outlook.com.
--------------------------------------------------------------------------