#include <MD_TCS230.h>
#include <FreqCount.h>

// Calibration
#define DARK_R 3879
#define DARK_G 2960
#define DARK_B 3838

#define WHITE_R 44954
#define WHITE_G 49706
#define WHITE_B 67180

// Pin definitions
#define  S2_OUT  12
#define  S3_OUT  13

MD_TCS230 CS(S2_OUT, S3_OUT);

void setup() {
  Serial.begin(9600);
  CS.begin();

  // Calibration
  sensorData sd;

  // Dark Cal
  sd.value[0] = DARK_R;
  sd.value[1] = DARK_G;
  sd.value[2] = DARK_B;
  CS.setDarkCal(&sd);

  // White Cal
  sd.value[0] = WHITE_R;
  sd.value[1] = WHITE_G;
  sd.value[2] = WHITE_B;
  CS.setWhiteCal(&sd);
}

void loop() {
  colorData rgb;
  
  CS.read();
  while (!CS.available());
  
  CS.getRGB(&rgb);
  Serial.print("C");
  Serial.write(rgb.value[TCS230_RGB_R]);
  Serial.write(rgb.value[TCS230_RGB_G]);
  Serial.write(rgb.value[TCS230_RGB_B]);
}
