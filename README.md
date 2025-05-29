# University-Course-with-Internship-in-ICB-Capston-Project

## ICB and Telerik are great dudes because the API and gauge are still working in 2025.

![image](https://github.com/user-attachments/assets/28963cbb-19ef-4ba3-80de-5f290b9d725e)


## - I have two course projects, both of which contain technical errors that, if removed, would stop them from being functional. Neither of them is easy to migrate, and that’s part of their charm. They were written manually—one even in Notepad—and were the most fun projects to work on. Artificial intelligence and migrating to .NET 8 would certainly be a drawn-out task for this particular project, which is the only one whose creativity I would share. Good old-fashioned programming, without being overwhelmed by libraries and strict principles, has its own charm.

" I'm sure you can protect PDFs so they can't be edited. You could also just refuse to do the "homework" and link to a portfolio of work instead.

If you want to be petty, you can also call them out on their social media posts that they stole from you. Or you can rate then negatively on Google/Glassdoor, etc. as others suggested.

If you do review them on those sites, I wouldn't say directly that they stole your ideas, but use phrasing like "the posts they made after my interview appeared to closely resemble a draft I submitted as part of an assignment"."

https://www.reddit.com/r/recruitinghell/comments/16qi5df/companies_stealing_work/

## "Private software companies use several methods to hide their source code and create closed-source software. Here are some key strategies:

1. Obfuscation

Code Obfuscation: This is a technique where the source code is transformed into a version that is difficult to understand. While the functionality remains the same, the code is made less readable to prevent reverse engineering.
Minification: This process reduces the size of the code (removing whitespace, comments, etc.) while maintaining functionality, making it harder to interpret.
2. Compiling to Binary

Software is written in high-level programming languages and then compiled into binary code. This binary format is not easily readable or modifiable by humans, making it more difficult to reverse engineer the original source code.
3. Licensing Agreements

Companies often use legal measures, such as End User License Agreements (EULAs), that restrict how users can interact with the software, including prohibiting reverse engineering or redistribution of the software.
4. Access Control

Companies can restrict access to the source code by keeping it on secure servers and limiting who can view or modify it. This includes using version control systems with strict access permissions.
5. Cloud-Based Solutions

Many companies offer software as a service (SaaS), where the software runs on the company’s servers, and users access it via the internet. This model keeps the source code on the provider's servers, away from users.
6. Proprietary Formats

Some software may use proprietary file formats that are not publicly documented, making it difficult for others to develop compatible software or to understand the inner workings of the software.
7. Use of Third-Party Libraries

Companies may rely on closed-source third-party libraries, which can further obscure the code. While the main application might be closed-source, the use of these libraries can complicate reverse engineering efforts.
8. Intellectual Property Protections

Companies may patent certain algorithms or techniques used in their software, providing legal protection against unauthorized use or reproduction.
Conclusion

By combining these techniques, private software companies can effectively protect their source code and maintain the competitive advantage that comes with closed-source software. The goal is to safeguard intellectual property while providing users with software that meets their needs without exposing the underlying code."

https://www.quora.com/How-do-private-software-companies-hide-their-source-code-How-do-they-make-closed-source-software

Sofia University "St. Kliment Ohridski" Faculty of Mathematics and Informatics Course Project Title: Information System for Monitoring and Analysis of Living Conditions in Student Dormitories
Student: Kristiana Asenova Asenova, 
Academic Year: 2020/2021 
Supervisors: Dr. Evgenii Krastev
Table of Contents
1.	Introduction 
2.	Overview of the Field 
3.	Design 
4.	Implementation, Testing/Experiments 
	 4.1 Technologies, Platforms, and Libraries Used 
	 4.2 Implementation 
7.	Conclusion 
8.	References 

  
1. Introduction
The project presents the implementation of an information system that collects data from various types of sensors installed in student dormitories worldwide. The requirements for the project include:
•	Sensors of the following types:
o	Temperature: measured in degrees
o	Humidity: measured in percentage
o	Electricity consumption: measured in watts
o	Door/Window Sensor: state measured as "open/closed"
o	Noise Sensor: measured in decibels
ICB provides a Web API to retrieve the data.


2. Overview of the Field
Environmental data collection sensors are widely used in the field of the Internet of Things (IoT). The article "Top 15 Sensor Types Used By IoT Application Development Companies (finoit.com)" clearly explains the significant role of sensors in various industries and organizations, as well as which sensors are most commonly used. The use of sensors has significantly increased following the rise of IoT technology, making them more prevalent than ever.


3. Design
The project includes the following classes:
•	Class Coordinates – used for creating objects of type Coordinates.

![image](https://github.com/user-attachments/assets/5149ff27-c2a7-4f69-8b94-bcb38300cf7f)

 
•	Class Range – creates objects of type Range. In addition to standard properties and constructors, this class includes an event NotifyPropertyChanged, as well as a method to check whether the minimum and maximum sensor values fall within acceptable user-defined limits. The class also contains an overridden ToString() method, which returns the minimum and maximum values separated by a hyphen.

 ![image](https://github.com/user-attachments/assets/c092b3ac-5d33-4270-88c8-f0935f02162e)

![image](https://github.com/user-attachments/assets/c5196ef8-f7d3-4966-bf10-275d2fddc210)

 ![image](https://github.com/user-attachments/assets/5cc080c3-bc73-499d-8ba1-9c8dd6d2d2bc)

![image](https://github.com/user-attachments/assets/a9f2d812-754d-4e4a-9543-26f9e8040709)

 
 
•	Class ReportSensor – inherits from Sensor and is used to create ReportSensor objects. These objects are stored in a collection that consists of sensors with values outside the user-defined limits and are used for representation in the Reports tab.

![image](https://github.com/user-attachments/assets/e2ed237a-4bd5-464d-a1ad-ca33f9968e58)

 
•	Class Sensor – created according to the requirements for constructors and class properties. It includes validation for incorrect user input during sensor creation. These sensors are displayed in the DataGrid named AllSensors, where incorrect data is visually marked in red.

![image](https://github.com/user-attachments/assets/05f9f6fc-76af-47f1-9c22-11fbf8bed786)

![image](https://github.com/user-attachments/assets/6449dece-2d14-41b8-9e91-22b7e046c421)
 
 
•	Class WebApiClient – responsible for retrieving real-time sensor values. It also formats data to be user-friendly, especially when values exceed user-defined limits. Different sensor types have separate validation methods, which are then handled using a switch statement.

![image](https://github.com/user-attachments/assets/daca454d-f9af-4382-a472-14d8e2fa3a6b)

 ![image](https://github.com/user-attachments/assets/d5008ec4-5227-4b72-ad52-ecf5497e721a)


•	Class XmlSerializer – used for storing sensor data in an XML file using XDocument.

![image](https://github.com/user-attachments/assets/3e415358-e6f3-4143-bca0-907da27eb723)

  
•	Class MainWindow – after the mandatory InitializeComponent(), sensors from the XML file are loaded into an ObservableCollection list, then presented visually in a DataGrid. Additionally, the method for mapping sensor coordinates is applied to display their locations on a map.
 
 
 ![image](https://github.com/user-attachments/assets/0979bfef-4da8-433f-8947-e62d6353b659)

![image](https://github.com/user-attachments/assets/a29f7d89-c2a1-416f-a010-65a300ae9c69)

![image](https://github.com/user-attachments/assets/965d8497-a95f-4509-9a85-f195ced124d8)

![image](https://github.com/user-attachments/assets/d977cb11-4adf-44a0-8d0d-19c2f1ef42b3)

![image](https://github.com/user-attachments/assets/84852c82-98f9-47e8-8246-33e0d00d7b55)

![image](https://github.com/user-attachments/assets/dd5c3e89-c779-44ba-bd32-619af9be8247)

![image](https://github.com/user-attachments/assets/8422821a-8190-4e0b-aaca-8c31f5335f52)

![image](https://github.com/user-attachments/assets/0389a728-dd6a-47da-acaa-bae1010d6c6f)

![image](https://github.com/user-attachments/assets/ac466d9d-8310-4482-82a5-1cae720ba797)

![image](https://github.com/user-attachments/assets/650d210f-f819-40e8-b96e-e18ead2cd07f)

 ![image](https://github.com/user-attachments/assets/d31823c7-4f4a-4e53-96ab-f945cdaf1234)

 ![image](https://github.com/user-attachments/assets/5a7370f4-ea90-4846-a724-a8e8cffed386)

 ![image](https://github.com/user-attachments/assets/e1c57027-8917-4bd3-835e-eb8beda12f68)

![image](https://github.com/user-attachments/assets/de80bc70-ab95-45f9-892c-b5adf9c50b32)

![image](https://github.com/user-attachments/assets/3096a5d7-78c3-483a-a289-d6cbd30678ec)


Requirements:
The user interface must provide the following functionalities:
•	Home page with system information.
•	Map view displaying all sensors (a reduced-size map) with access to a full-scale map.
•	Sensor management – register new sensors, modify existing ones, and delete sensors.
•	Graphical sensor representation – including:
o	A digital meter for electricity consumption.
o	Telerik RadialGauge for noise, temperature, and humidity representation.
o	Traffic light indicator for door/window state (open/closed).



4. Implementation, Testing/Experiments
   
4.1 Technologies, Platforms, and Libraries Used
•	Visual Studio 2017
•	Telerik RadGauge
•	Microsoft.Maps.MapControl.WPF


4.2 Implementation/Experiments
The application consists of multiple tabs with functionalities, including:

•	Home Tab – displays project details and objectives.

![image](https://github.com/user-attachments/assets/08dd94c1-af3c-4516-bcb7-d51b2514f1db)
 
•	Map Tab – provides a full-sized map of sensor locations and a refresh button.

![image](https://github.com/user-attachments/assets/3b7b7f8b-06e5-44a6-b538-2bbb14d273bc)
 
•	SensorList Tab – contains a table of sensors with links to their graphical representation. Includes buttons for adding, deleting, and updating sensors.

 ![image](https://github.com/user-attachments/assets/75c59c1e-a688-41ce-9de9-6d42ee835dc4)

•	Graphical Representation Tab – presents sensors visually using:

![image](https://github.com/user-attachments/assets/6c2a9d2a-4509-4522-9cbd-03f7ceeaa3be)
 
o	Telerik RadGauge for noise, temperature, and humidity sensors.

![image](https://github.com/user-attachments/assets/e206b459-4f98-40c3-842c-2da8537893c9)

o	Traffic light representation for doors/windows.
                
![image](https://github.com/user-attachments/assets/67a41432-10aa-470a-9716-3063d61556d1)

![image](https://github.com/user-attachments/assets/9d8c374a-aca3-4d98-9351-15cf966eebda)

o	Digital meter for electricity consumption.

![image](https://github.com/user-attachments/assets/56f51dd4-a218-4c29-8a66-3ff7af607855)

•	Reports Tab – lists sensors with values exceeding normal ranges. The table refreshes every 4 seconds using a DispatcherTimer. The mentor suggested running it on a separate thread for better performance, which remains as a future improvement.

 ![image](https://github.com/user-attachments/assets/e2fd0a71-1964-417f-94da-4e804cf609ff)

 ![image](https://github.com/user-attachments/assets/038871c1-cdab-41c8-b61b-8d72c449f640)
 
•	Performance Testing: Running the application with 1000 sensors caused performance issues, indicating potential optimization needs.

"I tried drawing a large amount of lines on a panel in winforms and the same dt on a canvas in using WPF and found the winforms creted the drwing in less than second, wheres WPF took many seconds, even though I ws using Pathgeometry in WPF rather than shpes"

https://stackoverflow.com/questions/19642320/is-there-a-performance-difference-between-wpf-and-winforms

“I don’t get it.  Why is this bad for performance?
The first thing you may have noticed is that it took three draw calls to render one ellipse.  Over those three draw calls, the same vertex buffer was used twice.  To explain the inefficiency, I need to explain a little on how GPUs work.  First, today’s GPUs process data VERY fast and run asynchronously with the CPU.  Also, there is costly user-mode to kernel mode transitions that happen with certain operations.  In the case that that a vertex buffer is filled, it must be locked.  If the buffer currently is used by the GPU, this causes the GPU to sync with the CPU, which can cause a performance hit.  The vertex buffer is created with a D3DUSAGE_WRITEONLY | D3DUSAGE_DYNAMIC, but when it is locked (which happens quite a bit), the D3DLOCK_DISCARD is not used.  This could cause a stall (a sync of the CPU and GPU) of the GPU if the buffer is in use by the GPU.  In the case of lots of draw calls, we have possibly a lot of kernel transitions and driver load.  The goal for good performance is to send as much work as possible to the GPU, or else your CPU will be busy and your GPU will be idle.  Also, do not forget that in this example, I’m only talking about 1 frame.  Typical WPF UI tries to execute at 60 frames every second!  If you’ve ever wondered what that high CPU usage on your render thread was from, you’ll find a lot (most?) is coming from your GPU driver”
https://jeremiahmorrill.wordpress.com/2011/02/14/a-critical-deep-dive-into-the-wpf-rendering-system/


5. Conclusion
The project was developed in accordance with the requirements. Future improvements include enhancing the graphical representation of sensors using Telerik and refining functionalities mentioned above.


6. References
•	Top 15 Sensor Types Used By IoT Application Development Companies (finoit.com)
•	Beck, K.; Fowler, M. (2018) "Refactoring: Improving the Design of Existing Code"

