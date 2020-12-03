
//dirrecion de los componentes
const int buzzerPin = 3;
const int redLedPin = 5;
const int greenLedPin = 6;

const int bufferSize = 32;// largo del mensaje en el buffer, 1 float
char recivedChar[bufferSize];
int incomingBytes[bufferSize];
float frequency  = 0; //donde se almacenara el mensaje pasado a float

int incomingByte =0;
boolean newData  = false;

void setup() {
 
  pinMode(greenLedPin,OUTPUT);
  pinMode(redLedPin,OUTPUT);
  Serial.begin(9600);//init puerto  COM5
  Serial.setTimeout(1);
}

void loop() {
    ReadBuffer();
    ShowData();
}

void ReadBuffer()
{
     char reciver;
     char endMarker = '\n';
     static  byte index =0;
     

  while(Serial.available() > 0 && newData == false){  
       reciver = Serial.read();  
      
       if(reciver != endMarker ){
          recivedChar[index] = reciver;
          index ++;
          
          if(index >= bufferSize)
          {
            index = bufferSize -1;
          }
        }else{                        
          recivedChar[index] = '\0';
          index = 0;          
          newData = true;
          delay(10); 
        }      
  }

}
void PlaySound(float _frequency){
   tone(buzzerPin,_frequency,250); //
   delay(251);

   noTone(buzzerPin);
}
void ShowData(){
  if(newData == true){ 
    frequency = atof(recivedChar);
    PlaySound(frequency);
   
    digitalWrite(redLedPin,LOW);
    digitalWrite(greenLedPin,HIGH);
    newData = false;   

  }else{
      digitalWrite(buzzerPin, LOW);
      digitalWrite(redLedPin,HIGH);
      digitalWrite(greenLedPin,LOW);    
  }
}
