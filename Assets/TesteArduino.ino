const int btnQtd = 4;
const int btnPins[] = {4,5,6,7};
const int btnKeys[] = {0,1,2,3};

const int switchPin = 8;
const int switchKey = 4;

const int potQtd = 2;
const int potPins[] = {0,1};
const int potKeys[] = {5,6};

void setup() {
  Serial.begin(9600);
  
  for(int i = 0; i < btnQtd;i++){
    pinMode(btnPins[i], INPUT_PULLUP);  
  }

  for(int i = 0; i < potQtd;i++){
    pinMode(potPins[i], INPUT);  
  }
  
  pinMode(switchPin, INPUT);
}

void loop() {
  // Buttons
  for(int i = 0; i < btnQtd;i++){
    WriteMessage(btnKeys[i], digitalRead(btnPins[i]));
  }

    // Switch
  WriteMessage(switchKey, digitalRead(switchPin));

  // Pots
  for(int i = 0; i < potQtd;i++){
    WriteMessage(potKeys[i], analogRead(potPins[i])/1023.0*255);
  }
}

void WriteMessage(int key, int value){
    Serial.write(key);
    Serial.write(value);
    Serial.flush();
    delay(20);
}
