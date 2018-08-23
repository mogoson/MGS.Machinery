# MGS-Machinery
- [English Manual](./README.md)

## 概述
- Unity绑定机械关节，铰链，机构插件包。

## 需求
- 绑定曲柄，即旋转关节，例如：起重机的上车部分以及大臂部分，外啮合齿轮，内啮合齿轮等。
- 绑定伸缩滑块，例如：起重机的伸缩力臂和横向/纵向支腿，压路机的腰部铰接等。
- 绑定液压油缸，例如：挖掘机的力臂驱动油缸，平地机的刮刀偏移驱动油缸等。
- 绑定曲柄摇杆机构，即平面四杆铰链，例如：装载机的铲斗，挖掘机的挖斗等。
- 绑定曲柄滑块机构，例如：往复活塞式内燃机，飞机行星发动机等。
- 绑定完整的工程机械，例如：起重机，压路机，装载机，平地机，挖掘机等。

## 环境
- Unity 5.0 或更高版本。
- .Net Framework 3.0 或更高版本。

## 实现
- Mechanism：机械关节，铰链，机构抽象定义。
- FreeCrank：自由曲柄，绕Z轴自由旋转。
- GearCrank：自由曲柄，绕Z轴自由旋转，可以线速度驱动。
- LimitCrank：受限曲柄，绕Z轴在指定角度范围内旋转。
- CrankRocker：曲柄摇杆机构。
- CrankSlider：曲柄滑块机构。
- RockerHinge：摇杆铰链，随摇杆摆动而旋转(单轴向)。
- RockerJoint：摇杆关节，始终朝向目标关节。
- RockerLimiter：摇杆限位器，限定一对摇杆之间的距离范围。
- RockerRivet：摇杆铆钉，始终与目标关节位置（世界坐标）相同。
- RockerSpring：摇杆弹簧，动态弹簧始终朝向目标关节。
- Slider ：伸缩滑块关节。
- SliderArm：序列伸缩臂。
- Synchronizer：同步机构，同时驱动多个机构等速运转。
- Transmission：差速机构，同时驱动多个机构差速运转。
- MeDriver：通用机构驱动器。

## 案例
- “MGS-Machinery/Prefabs”目录下存有上述机械关节和完整机械绑定的预制，供读者参考。
- “MGS-Machinery/Scenes”目录下存有上述机械关节和完整机械绑定的演示案例，供读者参考。

## 预览
- Crank Rocker

![Crank Rocker](./Attachments/README_Image/CrankRocker.gif)

- Crank Slider

![Crank Slider](./Attachments/README_Image/CrankSlider.gif)

- Rocker Spring

![Rocker Spring](./Attachments/README_Image/RockerSpring.gif)

- Extend Mould

![Extend Mould](./Attachments/README_Image/ExtendMould.gif)

- Cross Extender

![Cross Extender](./Attachments/README_Image/CrossExtender.gif)

- Rocker Hinge

![Rocker Hinge](./Attachments/README_Image/RockerHinge.gif)

- Internal Gearing

![Internal Gearing](./Attachments/README_Image/InternalGearing.gif)

- Airplane Engine

![Airplane Engine](./Attachments/README_Image/AirplaneEngine.gif)

- Gas Engine

![Gas Engine](./Attachments/README_Image/GasEngine.gif)

- Crane

![Crane](./Attachments/README_Image/Crane.gif)

- Road Roller

![Road Roller](./Attachments/README_Image/RoadRoller.gif)

- Loader

![Loader](./Attachments/README_Image/Loader.gif)

- Grader

![Grader](./Attachments/README_Image/Grader.gif)

- Excavator

![Excavator](./Attachments/README_Image/Excavator.gif)

- Dumper

![Dumper](./Attachments/README_Image/Dumper_H.gif)

- Dumper

![Dumper](./Attachments/README_Image/Dumper_P.gif)

- Helicopter

![Helicopter](./Attachments/README_Image/Helicopter.gif)

## 联系
- 如果你有任何问题或者建议，欢迎通过mogoson@outlook.com联系我。