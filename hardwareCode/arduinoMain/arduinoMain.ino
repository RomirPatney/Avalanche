
int sensorPin = 0;
double alpha = 0.75;
int period = 200;
double change = 0.0;
double minval = 0.0;
bool a=false;

void setup ()
{
  Serial.begin (9600);
  pinMode(7,OUTPUT);
  pinMode(8,OUTPUT);
}
void loop ()
{
    static double oldValue = 0;
    static double oldChange = 0;
 
    int rawValue = analogRead (A2);
    double value = alpha * oldValue + (1 - alpha) * rawValue;
 
    Serial.print (rawValue);
    Serial.print (",");
    Serial.println (value);
    oldValue = value;

    if(a)
    {
      digitalWrite(8,HIGH);
      digitalWrite(7,LOW);
      a=false;
    }

    else
    {
      digitalWrite(7,HIGH);
      digitalWrite(8,LOW);
      a=true;
    }
    
    delay (period);

}

/*
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
*/
