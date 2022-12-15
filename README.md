# QuizzettinoDemo

Applicazione di esempio dell'interfacciamento di una applicazione Windows Forms con Quizzettino

## Descrizione
Una volta attivata, l'applicazione mostra un elenco delle porte seriali rilevate dal quale selezionare quella connessa al Quizzettino. Dopo aver fatto click sul pulsante "Connetti" verrà avviata la comunicazione, e per prima cosa verrà letta la configurazione (modalità AutoReset ed attivazione dei suoni), quindi inviato un comando di reset per spegnere qualsiasi eventuale LED dei concorrenti.

![image](https://user-images.githubusercontent.com/27277104/207894431-3f0a7092-db52-46c9-b217-1f34b6499a97.png)

Quando un concorrente preme un pulsante si attiverà anche il corrispondente segnalino colorato presente nell'applicazione, e spento quando si preme il pulsante "Reset" di Quizzettino o dell'applicazione. Anche premendo uno dei 6 pulsanti della demo si attiverà a su Quizzettino la corrispondente luce, ed ovviamente anche il segnalino colorato. Quindi il conduttore del gioco può utilizzare i due grossi pulsanti verde e rosso presenti su Quizzettino per indicare rispettivamente "risposta corretta" o "risposta errata", l'informazione verrà comunicata anche al PC, e resettato Quizzettino (si spegnerà la luce del concorrente ed il sistema sarà ora in attesa di un nuovo pulsante).

Le opzioni "AutoReset" e "Suoni" vengono impostate automaticamente all'apertura della porta, in base alla configurazione corrente di Quizzettino. Modificando una opzione, questa verrà immediatamente inviata a Quizzettino e sarà subito operativa e salvata nella EEPROM di Arduino.

## Libreria
Per facilitare l'integrazione di Quizzettino con eventuali programmi su PC, è disponibile la libreria "[QuizLib](https://github.com/dotto59/QuizLib)" al cui repository si rimanda per tutti i dettagli.
