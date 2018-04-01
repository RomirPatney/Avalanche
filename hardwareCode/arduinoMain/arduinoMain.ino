const int sensePin = A0;  
const int PIEZO_PIN = A1;

//temp
int sensorInput;    
double temp; 
bool inv=true;

int sensorPin = 0;
double alpha = 0.75;
int period = 1000;
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
    //Temperature sensor
    sensorInput = analogRead(A0); 
     
    if(inv)
    {
      sensorInput=1024-sensorInput;
      temp=sensorInput/20;
    }

    else
    {
      temp=sensorInput/20;
    }
  
    Serial.print("Current Temperature: ");
    Serial.println(temp);


   
    //Vibration Sensor
    int piezoADC = analogRead(PIEZO_PIN);
    float piezoV = piezoADC / 1023.0 * 5.0;
    Serial.print("Vibration: ");
    Serial.println(piezoV); // Print the voltage.



    //Heart Rate sensor
    static double oldValue = 0;
    static double oldChange = 0;
 
    int rawValue = analogRead (A2);
    double value = alpha * oldValue + (1 - alpha) * rawValue;
    value/=2;
 
   // Serial.print (rawValue);
   // Serial.print (",");
    Serial.print ("Heart Rate: ");
    Serial.println (value);
    oldValue = value;


/*
    //LEDs
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
*/

    digitalWrite(7,LOW);
      
    delay (period);

    digitalWrite(7,HIGH);

    Serial.println("");
}

