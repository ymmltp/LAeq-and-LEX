## EHS 噪声显示

#### 1、筛选项：

- 设备
- 开始时间
- 结束时间

#### 2、API说明：

Web：https://env.jingkongyun.com/dashboard

> https://env.jingkongyun.com:9089/api/sensorData/getDataByDeviceidAndTime?deviceid=87222427524978660667FF50&start=2020-10-18 09:11:31&end=2020-10-20 09:11:31

**地址参数:**

​	deviceid:87222427524978660667FF50

​	start:2020-10-18 09:11:31

​	end:2020-10-20 09:11:31

**参数介绍：**

​     deviceid-设备ID

​     start-开始时间

​     end-结束时间

#### 3、显示结果

- 工作时间
- T8小时等效

#### 4、设计界面

早7.15-晚7.15; 晚7.15-次日7.15

|                        |          | 精确到15分钟 |          |               |                                  |                     |
| :--------------------: | :------: | :----------: | :------: | ------------- | :------------------------------: | ------------------- |
|                        | 采样时间 |   开始时间   | 结束时间 | 工作时间Te(h) | 工作时间段内的等效声级（LAeq,T） | T 8小时等效(LEX,8H) |
| 1.设置工作时间起始时间 |  选择框  |    选择框    |  选择框  | 自动计算      |             自动计算             | 8h                  |
| 1.设置工作时间起始时间 |          |              |          |               |                                  |                     |
|                        |          |              |          |               |                                  |                     |

#### 5、计算方法

![image-20210512170840948](C:\Users\1382919\AppData\Roaming\Typora\typora-user-images\image-20210512170840948.png)

$$
L_{Aeq,T} =10lg(1/T \sum_{i=1}^nT_i10^{0.1L_{Aeq,T_i}} )
$$
![image-20210512170903813](C:\Users\1382919\AppData\Roaming\Typora\typora-user-images\image-20210512170903813.png)

### Update

#### 2021.05.20

- Noise Analysis     add formula picture.
- Noise Analysis     add excel export function.
- Add Monthly Lex     chart , and you can download the picture of the chart
- Select all    Sample information by default

