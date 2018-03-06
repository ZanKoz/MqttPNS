# MqttPNS #
## Branches ##
1. Master: Δοκιμασένη Εφαρμογή
2. Sxolia: Εφαρμογή με αναλυτικά σχόλια
## Προαπαιτούμενα ##
Για να γίνει debug της εφαρμογής στο visual studio πρέπει να εγκατασταθεί ο μεσίτης mosquitto και να μεταφερθει ο φάκελος του στην τοποθεσία bin/Debug. H ονομασία του φακέλου που περιέχει την εφαρμογη mosquitto πρεπει να ειναι "service". Αν εγκατα

## Περιγραφή ##
Η εφαρμογή Mikro PNS, ελέγχει την εύρυθμη λειτουργία ενός MQTT μεσίτη και εκτελεί, τις παρακάτω λειτουργίες:
* Εκτέλεση και τερματισμός του μεσίτη Mosquitto MQTT.
* Ρύθμισή κωδικού του μεσίτη που απαιτείται από τις συσκευές που θέλουν να συνδεθούν σε αυτόν.
* Καταγραφή  (συνδέσεων, αποσυνδέσεων) του μεσίτη.
* Καταγραφή ολων των μηνυμάτων  που ανταλλάσσονται μέσω του μεσίτη με την χρήση ενός πελάτη ο οποίος έχει εγγραφεί σε όλα τα θέματα (Θέμα εγγραφής = ‘#’).
* Δυνατότητα δημοσίευσης, οποιουδήποτε μηνύματος, σε οποιοδήποτε θέμα, με σκοπό την αποσφαλμάτωση των συνδεδεμένων συσκευών.
## Κώδικας Εφαργμογής ##
Φάκελος micro.autom.MqttBroker
## Φωτογραφίες ##
![alt text](https://github.com/ZanKoz/MqttPNS/blob/master/App%20Screenshots/Main%20Window.jpg)
![alt text](https://github.com/ZanKoz/MqttPNS/blob/master/App%20Screenshots/Energies.JPG)
![alt text](https://github.com/ZanKoz/MqttPNS/blob/master/App%20Screenshots/Mosq%20Broker%20Log.JPG)
![alt text](https://github.com/ZanKoz/MqttPNS/blob/master/App%20Screenshots/notif%20icon.JPG)
![alt text](https://github.com/ZanKoz/MqttPNS/blob/master/App%20Screenshots/Mikro%20PNS%20installation.JPG)

