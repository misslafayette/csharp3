# Feedback for assignment 06.1

Kod víceméně funguje, ale

1. nešlo to zkompilovat - podle problému to vypadalo že to vzniklo aktualizací kódu z šablony, byly tam v Migrations složce duplikáty. Zduplikované soubory jsem odstranil.
2. pokud si spustíš naši web api, tak nefunguje - důvody jsem popsal v komentářích v kódu, poprosím opravit.

Teď trochu hodnocení :wink:

Obecně kód funguje (po opravení chyb) a nemám důvod úkol neuznat. Bohužel se děje poměrně často že dostávám nezkompilovatelný kód (má kompilační errory) nebo kód který nefunguje jak má (má runtime errory nebo logické errory). Neboj, to se nestává jenom u tebe, takže teď to říkám obecně.
Do skupinového chatu na Discord se pokusím co nejdříve nahrát nějaký soubor kde napíšu nějaké good practices jak programovat, tak až se stane tak poprosím si to v klidu přečíst :wink:

Dále trochu rozeberu můj komentář k Delete metodě v ToDoItemsRepository.
Jak jsem psal v komentářích, dobré je když se metody dané třídy chovaní konzistentně. Představ si že máš třídu která má větší množství metod a někdo po tobě tyto metody využívá. No a pro toho člověka (nebo pro tebe pokud jsi to např napsala dávno) je těžké se vyznat co má a nemá dělat když se každá metoda chová principielně jinak.
Update - nekontroluje že můžeme updatovat item
Delete - kontroluje že můžeme smazat item

a teď si představ dalších 20 metod kdy se to různě mění a střídá, občas to kontroluje, občas ne. Občas by to něco zalogovalo, občas ne. Občas by to odchycovalo nějaké exceptions, občas ne :) no byl by to chaos. Tak na to pozor. Cílem je naspat kód který je dobře udržitelný, srozumitelný a když se k tomu vrátíš za 3 měsíce, tak abys nemusela složitě bádat co jednotlivá metoda dělá nebo nedělá :wink:

Jinak beru jako velké plus jak se ti podařilo rychle dohnat zbytek skupiny a chválím za dobrou spolupráci se mnou - když si s něčím nevíme rady, tak od toho jsme lektoři a koučové abychom poradili.
