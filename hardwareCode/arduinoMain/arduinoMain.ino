const int sensePin = A0;  
const int PIEZO_PIN = A1;
int sensorInput;    
double temp; 
       



void setup() {
  Serial.begin(9600); 
}


void loop() {
  
  sensorInput = analogRead(A0);    
  temp = (double)sensorInput / 1024;   
  temp = temp * 5;                 
  temp = temp - 0.5;               
  temp = temp * 100;               

  Serial.print("Current Temperature: ");
  Serial.println(sensorInput);
  Serial.println(temp);
  Serial.println("End");


  int piezoADC = analogRead(PIEZO_PIN);
  float piezoV = piezoADC / 1023.0 * 5.0;
  Serial.println("Vibration: ");
  Serial.println(piezoV); // Print the voltage.

 
  delay(200);
}

