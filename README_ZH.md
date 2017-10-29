# MGS-Machinery
- [English Manual](./README.md)

## 概述
- Unity绑定机械关节，铰链，机构插件包。

## 环境
- Unity 5.0 或更高版本。
- .Net Framework 3.0 或更高版本。

## 实现
- Mechanism：机械关节，铰链，机构抽象定义。
- VectorExtension：基于法线计算旋转角度。
- Planimetry：平面几何定义与计算。

- FreeCrank：自由曲柄，绕Z轴自由旋转。
- LimitCrank：受限曲柄，绕Z轴在指定角度范围内旋转。
- CrankRocker：曲柄摇杆机构。
- CrankSlider：曲柄滑块机构
- RockerHinge：摇杆铰链，随摇杆摆动而旋转(单轴向)。
- RockerJoint：摇杆关节，始终朝向目标关节。
- RockerLock：摇杆限位锁，指定一对摇杆之间的距离范围。
- RockerRivet：摇杆铆钉，始终与目标关节位置（世界坐标）相同。
- TelescopicJoint：伸缩关节。
- CeTelescopicJoint：（基于）中心伸缩关节。
- SeTelescopicArm：（序列）伸缩臂。
- SynchroMechanism：同步机构，同时驱动多个机构运转。

- MeDriver：通用机构驱动器。

## 案例
- “MGS-Machinery/Prefabs”目录下存有上述机械关节预制，供读者参考。
- “MGS-Machinery/Scenes”目录下存有上述功能的演示案例，供读者参考。

## 图示
- CrankRocker

![CrankRocker](./Attachments/CrankRocker.png)

- CrankSlider

![CrankSlider](./Attachments/CrankSlider.png)

- RockerHinge

![RockerHinge](./Attachments/RockerHinge.png)

- InternalGearing

![InternalGearing](./Attachments/InternalGearing.png)

- AirplaneEngine

![AirplaneEngine](./Attachments/AirplaneEngine.png)

- GasEngine

![GasEngine](./Attachments/GasEngine.png)

- Crane

![Crane](./Attachments/Crane.png)

- RoadRoller

![RoadRoller](./Attachments/RoadRoller.png)

- Loader

![Loader](./Attachments/Loader.png)

- Grader

![Grader](./Attachments/Grader.png)

- Excavator

![Excavator](./Attachments/Excavator.png)

## 联系
- 如果你有任何问题或者建议，欢迎通过mogoson@qq.com联系我。