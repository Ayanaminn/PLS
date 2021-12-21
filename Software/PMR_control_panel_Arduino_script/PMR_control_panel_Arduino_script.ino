
byte Val; 
int ledpin = 9;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(ledpin,OUTPUT);
}

void loop() {
  if (Serial.available()) {
      Val = Serial.read();    
      Serial.println(Val);
      analogWrite(ledpin,int(Val));        
      }
  }




