int sensePin = A0;  
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

  delay(200);
}
