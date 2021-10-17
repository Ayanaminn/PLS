# PMR control panel
Photomotor Response (PMR) control panel enables programmable photic stimulus control through interfacing with Arduino-based microcontroller.

![image](https://user-images.githubusercontent.com/49441654/137485205-9662aee2-a87d-471b-9ff2-5248a4b0aa1b.png)



**Please cite our manuscript:**

Coming soon....


Installation:
------------
### Requirements

* Windows operating system

* Arduino board. Arduino Mega2560 was validated, however any USB interfacing board that have digital I/O pins with PWM should work.


### Software

1. Download the latest release from [Releases](https://github.com/Ayanaminn/PMR-control-panel/releases)

2. Unzip file and open "PMR control panel" folder

3. Double click to run "PMR control panel.exe"

### Hardware

1. Download the latest release from [Releases](https://github.com/Ayanaminn/PMR-control-panel/releases)

2. Unzip file and open "PMR control panel Arduino script" folder

3. Using [Arduino IDE](https://www.arduino.cc/en/software) to open the "PMR_control_panel_Arduino_script.ino" and upload it to board

4. Wire LED circuit to Arduino pin 9 (default pin), if user prefer to use other pins, change pin number in the script accordingly and re-upload


Getting Started:
------------

1. Connect the Arduino with the computer

2. In the PMR control panel, select the Arduino port, then click "Connect"

3. Use manual or auto mode to control light stimulus

4. Disconnect the Arduino before exit the program


License:
------------

MIT License. See [LICENSE](https://github.com/Ayanaminn/PMR-control-panel/blob/master/LICENSE) for details.
