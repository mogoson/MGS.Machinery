# MGS-Machinery
- [中文手册](./README_ZH.md)
- [Alibaba Cloud](https://www.aliyun.com/minisite/goods?userCode=0fgf4qk9)

## Summary
- Unity plugin for binding machinery joint in scene.

## Demand
- Binding rotate joints, example: upcar of crane, external gearing and inner gearing.
- Binding slider joints, example: big arm and landing legs of crane, waist articulation of road roller.
- Binding hydraulic cylinder.
- Binding dynamic spring.
- Binding crank roker, example: scraper bucket of loader, bucket of excavator.
- Binding crank slider, example: reciprocating engine, aircraft planetary engine.
- Binding complete construction machinery, example crane, road roller, loader, grader and excavator.
- Binding mesh Gear.
- Binding worm gear.
- Binding belt flywheel.
- Binding vibrator.
- Binding differential.
- Binding transmission.

## Environment
- Unity 5.0 or above.
- .Net Framework 3.0 or above.

## Achieve
- Mechanism : Define abstract joint, hinge and mechanism.
- FreeCrank : Free rotate around Z axis.
- GearCrank : Free rotate around Z axis, can be drived by linear velocity.
- LimitCrank : Rotate around Z axis in the angle range. 
- CrankRocker : Crank rocker mechanism.
- CrankSlider : Crank slider mechanism.
- RockerHinge : Hinge of roker, rotate around the axis follow roker.
- RockerJoint : Roker joint, always look at target joint.
- RockerLimiter : Limiter of roker, limit the distance of a pair rokers.
- RockerRivet : Rivet of roker, keep the same position(world space) as the target joint.
- RockerSpring : Rocker spring look at joint.
- Slider : Slider joint.
- SliderArm : Sequence slider arm, drive from first joint to last joint.
- Synchronizer : Synchronous mechanisms, drive multi mechanisms at the same time.
- Transmission : Differential mechanisms, drive multi mechanisms by ratio velocity at the same time.
- MechanismDriver : Universal mechanism driver.
- Gear : Gear rotate around axis Z.
- Axle : Axle rotate around axis Z.
- CoaxialGear : Coaxial gear with the same axis as another gear.
- WormGear : Worm gear mechanism.
- WormShaft : Worm shaft mechanism.
- Belt : Move texture UV on X axis.
- LinearVibrator : Reciprocating motion on Z axis.
- CentrifugalVibrator : Eccentric motion around Z axis.
- Motor : Motor provide power to drive axle. 
- Differential : Ordinary differential.

## Demo
- Prefabs in the path "MGS-Machinery/Prefabs" provide reference to you.
- Demos in the path "MGS-Machinery/Scenes" provide reference to you.

## Preview
- Crank Rocker

![Crank Rocker](./Attachment/README_Image/CrankRocker.gif)

- Crank Slider

![Crank Slider](./Attachment/README_Image/CrankSlider.gif)

- Rocker Spring

![Rocker Spring](./Attachment/README_Image/RockerSpring.gif)

- Extend Mould

![Extend Mould](./Attachment/README_Image/ExtendMould.gif)

- Cross Extender

![Cross Extender](./Attachment/README_Image/CrossExtender.gif)

- Rocker Hinge

![Rocker Hinge](./Attachment/README_Image/RockerHinge.gif)

- Internal Gearing

![Internal Gearing](./Attachment/README_Image/InternalGearing.gif)

- Airplane Engine

![Airplane Engine](./Attachment/README_Image/AirplaneEngine.gif)

- Gas Engine

![Gas Engine](./Attachment/README_Image/GasEngine.gif)

- Crane

![Crane](./Attachment/README_Image/Crane.gif)

- Road Roller

![Road Roller](./Attachment/README_Image/RoadRoller.gif)

- Loader

![Loader](./Attachment/README_Image/Loader.gif)

- Grader

![Grader](./Attachment/README_Image/Grader.gif)

- Excavator

![Excavator](./Attachment/README_Image/Excavator.gif)

- Dumper

![Dumper](./Attachment/README_Image/Dumper_H.gif)

- Dumper

![Dumper](./Attachment/README_Image/Dumper_P.gif)

- Helicopter

![Helicopter](./Attachment/README_Image/Helicopter.gif)

- Mesh Gears

![Mesh Gears](./Attachment/README_Image/MeshGears_E.gif)

- Mesh Gears

![Mesh Gears](./Attachment/README_Image/MeshGears_C.gif)

- Belt

![Belt](./Attachment/README_Image/Belt.gif)

- Worm Gear

![Worm Gear](./Attachment/README_Image/WormGear.gif)

- Vibrosieve

![Vibrosieve](./Attachment/README_Image/Vibrosieve.gif)

- Differential

![Differential](./Attachment/README_Image/Differential.gif)

- Transmission

![Transmission](./Attachment/README_Image/Transmission.gif)

## Contact
- If you have any questions, feel free to contact me at mogoson@outlook.com.
