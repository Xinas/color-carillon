/*
  arduino.ino

  The program utilize the sensor TCS230 to detect colors and write them
  on the serial port as three byte (one for each of the RGB color space values).
  All the readings of the sensor are done through the library MD_TCS230.


  The circuit:
  * TCS230: listed below are all the sensor's pin and where they should attached
  * GND attached to ground
  * VCC attached to +5V
  * S2 attached to pin 12
  * S3 attached to pin 13
  * OUT attached to pin 5

  * Note: The OUT port on the sensor MUST be attached to pin 5

  Created 29 Oct 2019
  By Lanzani Andrea, Ronconi Riccardo

  https://github.com/Xinas/color-carillon

*/

#include <MD_TCS230.h>
#include <FreqCount.h>

// Calibration constants
#define DARK_R 3879
#define DARK_G 2960
#define DARK_B 3838

#define WHITE_R 44954
#define WHITE_G 49706
#define WHITE_B 67180

// Pin definitions
#define S2_OUT 12
#define S3_OUT 13

MD_TCS230 CS(S2_OUT, S3_OUT);

void setup()
{
  Serial.begin(9600);
  CS.begin();

  // Calibration of the sensor
  sensorData sd;

  // Dark Calibration
  sd.value[0] = DARK_R;
  sd.value[1] = DARK_G;
  sd.value[2] = DARK_B;
  CS.setDarkCal(&sd);

  // White Calibration
  sd.value[0] = WHITE_R;
  sd.value[1] = WHITE_G;
  sd.value[2] = WHITE_B;
  CS.setWhiteCal(&sd);
}

void loop()
{
  colorData rgb;

  CS.read();
  while (!CS.available())
    ;

  CS.getRGB(&rgb);
  Serial.print("C");
  Serial.write(rgb.value[TCS230_RGB_R]);
  Serial.write(rgb.value[TCS230_RGB_G]);
  Serial.write(rgb.value[TCS230_RGB_B]);
}
