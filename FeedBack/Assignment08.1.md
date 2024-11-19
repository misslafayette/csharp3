# Feedback for assignment 08.1

Dashboard pěkně funguje :wink:

Svým projektem FrontEnd jsi ale rozbila projekt WebApi...

![alt text](<Screenshot 2024-11-19 210326.png>)

mělo by to být tak že složka src (neboli source) má v sobě 4 složky které v sobě obsahují příslušné projekty
- ToDoList.Domain
- ToDoList.Persistence
- ToDoList.WebApi
- ToDoList.Frontend

momentálně máš frontend schovaný ve složce ToDoList.Frontend který je ve složce src která je v ToDoList.WebApi ... a to způsobuje velké problémy. Poprosím opravit, pokud si s tím nebudeš vědět rady tak můžu pomoct :wink:

Problém se určitě stal tak že jsi měla otevřený integrovaný terminál ne na úrovni složky ToDoList, ale na úrovni ToDoList.WebApi (představ si že jsi spouštěla command ne v nějaké složce, ale její subsložce. To je celkem rozdíl)

Zkus se s tím poprat, nejsem si teď jistý jestli to nebude problém v nadcházející lekci.
