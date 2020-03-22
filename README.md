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
- MeDriver : Universal mechanism driver.

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

## Contact
- If you have any questions, feel free to contact me at mogoson@outlook.com.
