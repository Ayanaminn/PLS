# PLS (Zebrafish Embryo Photomotor Response and Larval Predator response system)
PLS (Zebrafish Embryo Photomotor Response and Larval Predator response system) is developed for rapid analysis of large cohorts of specimens in PMR and SPR neurotoxicity bioassays. 

PLS includes applications of custom embryo arrays in conjunction with an open-source and inexpensive infrared video imaging system coupled with a dedicated bioinformatic approach that facilitates high-throughput analysis of behavioural phenotypes.

![PLS_overview](https://user-images.githubusercontent.com/49441654/146858458-b18fd8b9-6895-4b48-8c6e-9d7b1c72e19a.png)

The photic stimulus is controlled by PMR Control Panel, a custom software application that enables programmable photic stimulus control through interfacing with Arduino-based microcontroller.

![image](https://user-images.githubusercontent.com/49441654/137485205-9662aee2-a87d-471b-9ff2-5248a4b0aa1b.png)



**Please cite our manuscript:**

Henry, J., Bai, Y., Kreuder, F., Mawdsley, D., Kaslin, J., & Wlodkowic, D. (2022). Accelerating Chemobehavioral Phenotypic Screening in Neurotoxicology Using a Living Embryo Array System. Zebrafish, 19(1), 32-35. https://doi.org/10.1089/zeb.2021.0072 


Installation:
------------
### Requirements

* Windows operating system.

* [Key Componect List](https://github.com/Ayanaminn/PLS/blob/master/Hardware/Assembley%20Instructions%20and%20Component%20List.docx).


### Software

1. Download the latest release from [Releases](https://github.com/Ayanaminn/PLS/releases).

2. Unzip file and open "PMR control panel" folder.

3. Double click to run "PMR control panel.exe".

### Hardware

1. Following the [Assembly instructions](https://github.com/Ayanaminn/PLS/blob/master/Hardware/Assembley%20Instructions%20and%20Component%20List.docx).

2. Download the latest release from [Releases](https://github.com/Ayanaminn/PLS/releases).

3. Unzip file and open "PMR control panel Arduino script" folder.

4. Using [Arduino IDE](https://www.arduino.cc/en/software) to open the "PMR_control_panel_Arduino_script.ino" and upload it to board.

5. Wire LED circuit to Arduino pin 9 (default pin), if user prefer to use other pins, change pin number in the script accordingly and re-upload.


Getting Started:
------------

1. Connect the Arduino with the computer.

2. In the PMR control panel, select the Arduino port, then click "Connect".

3. Use manual or auto mode to control light stimulus.

4. Disconnect the Arduino before exit the program.


License:
------------

MIT License. See [LICENSE](https://github.com/Ayanaminn/PLS/blob/master/LICENSE) for details.


Contact:
------------

Should you have any questions about this application, please report an issue [here](https://github.com/Ayanaminn/PLS/issues), or send an [email](mailto:yutao.bai@student.rmit.edu.au).

If you have further interests about the research this program has been applied for, please [visit our website](https://neurotoxlab.com).

