# MGS-Machinery
- [中文手册](./README_ZH.md)

## Summary
- Unity plugin for binding machinery joint in scene.

## Demand
- Binding rotate joints, example: upcar of crane, external gearing and inner gearing.
- Binding telescopic joints, example: big arm and landing legs of crane, waist articulation of road roller.
- Binding hydraulic cylinder.
- Binding crank roker, example: scraper bucket of loader, bucket of excavator.
- Binding crank slider, example: reciprocating engine, aircraft planetary engine.
- Binding complete construction machinery, example crane, road roller, loader, grader and excavator.

## Environment
- Unity 5.0 or above.
- .Net Framework 3.0 or above.

## Achieve
- Mechanism : Define abstract joint, hinge and mechanism.
- FreeCrank : Free rotate around Z axis.
- LimitCrank : Rotate around Z axis in the angle range. 
- CrankRocker : Crank rocker mechanism.
- CrankSlider : Crank slider mechanism.
- RockerHinge : Hinge of roker, rotate around the axis follow roker.
- RockerJoint : Roker joint, always look at target joint.
- RockerLimiter : Limiter of roker, limit the distance of a pair rokers.
- RockerRivet : Rivet of roker, keep the same position(world space) as the target joint.
- TelescopicJoint : Telescopic joint.
- SequenceTelescopicArm : Sequence telescopic arm, drive from first joint to last joint.
- SynchroMechanism : Synchronous mechanisms, drive multi mechanisms at the same time.
- DifferentialMechanism : Differential mechanisms, drive multi mechanisms by ratio velocity at the same time.
- MeDriver : Universal mechanism driver.

## Demo
- Prefabs in the path "MGS-Machinery/Prefabs" provide reference to you.
- Demos in the path "MGS-Machinery/Scenes" provide reference to you.

## Preview
- CrankRocker

![CrankRocker](./Attachments/README_Image/CrankRocker.gif)

- CrankSlider

![CrankSlider](./Attachments/README_Image/CrankSlider.gif)

- RockerHinge

![RockerHinge](./Attachments/README_Image/RockerHinge.gif)

- InternalGearing

![InternalGearing](./Attachments/README_Image/InternalGearing.gif)

- AirplaneEngine

![AirplaneEngine](./Attachments/README_Image/AirplaneEngine.gif)

- GasEngine

![GasEngine](./Attachments/README_Image/GasEngine.gif)

- Crane

![Crane](./Attachments/README_Image/Crane.gif)

- RoadRoller

![RoadRoller](./Attachments/README_Image/RoadRoller.gif)

- Loader

![Loader](./Attachments/README_Image/Loader.gif)

- Grader

![Grader](./Attachments/README_Image/Grader.gif)

- Excavator

![Excavator](./Attachments/README_Image/Excavator.gif)

## Contact
- If you have any questions, feel free to contact me at mogoson@qq.com.