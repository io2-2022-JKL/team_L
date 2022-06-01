## Docker
1. By stworzyć kontener bazy danych należy najpierw wejść w folder vsdb (obecny) i w jakiejkolwiek konsoli wywołać **docker build -t <nazwa> .** 
  (pamiętać o kropce na końcu!). Następnie w aplikacji desktopowej Dockera wejść w Images i stworzony obraz uruchomić (*run*). W opcjach podać port 1433 jako local host. 
  W Containers pojawi się nowy kontener. By upewnić się, że kontener bazy poprawnie powstał należy uruchomić i zalogować się w MS SQL Server 
  (hasło podane w Dockefile jako SA_PASSWORD).
2. Analogicznie przebiega tworzenie backendu - w folderze team_L (gdzie jest Dockefile) należy wywołać **docker build -t <nazwa> .**, a nastepnie 
  wybrać odpowiednie obraz z Images i go uruchomić. W opcjach możemy podać port, na którym chcemy żeby uruchamiał się backend.
  Gdy nowopowstały kontener jest *running* to możemy nacisnąc w aplikacji Dockera przycisk *open in browser*. Otworzy się wtedy przeglądarka z 
  adresem *localhost:[wybrany port]*. Dalej możemy podawać endpointy, które chcemy testować, np. *localhost:[port]/admin/doctors*. UWAGA, należy się upewnić czy
  w *connection strings* jest odkomentowany odpowiedni *default connection*.
