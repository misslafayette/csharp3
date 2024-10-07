# Feedback ukolu 02.1

Web.API projekt jsi úspěšně rozjela, což je super, budeme na tom dále stavět v dašlích lekcích. Jen silně doporučím si složku ToDoListNew přesunout o úroveň výš (na úroveň složky ToDoList) aby v tom nebyl zmatek, ale nechávám to na tobě (ale opravdu silně doporučuju). Plus je teda další ve FizzBuzz.01 složce ale to už byl asi omyl při zakládání solution a projektu :)

V domácím úkolu máš ovšem jeden nedostatek, mluvím o posledním bodu
**Nakonec přidejte novou cestu `/nazdarSvete`, která vás pozdraví v češtině a otestujte si ji v prohlížeči.**

Cílem bylo přidat funkci, kdy pokud si spustím web API a do browseru zadám cestu (tedy pošlu GET request na naši api) localhost:*PortNumber*/nazdarSvete
tak nás to pozdraví česky.

Poprosím teda ještě upravit projekt ToDoList\NewToDoList\src\ToDoList.WebApi.

Ale neboj, poradím jak na to, je to jednořádková změna :)

Podíváme se spolu do Program.cs (ToDoList\NewToDoList\src\ToDoList.WebApi.Program.cs) a projedeme si jeden řádek kódu.

´´´
app.MapGet("/", () => "Hello world!");
´´´

app.MapGet - co tato metoda dělá: zjednodusene řečeno stanoví naši webové api jak reagovat na GET request pro určitou URL adresu

Copak teda udělá celý řádek?
´´´
app.MapGet("/", () => "Hello world!");
´´´

Pokud prijde GET request na adresu api (u naseho lokalniho testovani to je nase zname localhost:PortNumber)
cela cesta je vlastne http://localhost:PortNumber/
tak posle zpatky odpoved "Hello world!"
*/

Takže pokud chci aby mi mé webové API posílalo český pozdrav pokud pošlu GET request na cestu http://localhost:PortNumber/nazdarSvete, tak musím přidat další řádek

´´´
app.MapGet( cesta na kterou reaguju (bez http://localhost:PortNumber), () => Český pozdrav světu jako string);
´´´


Poprosím doopravit.
Kdyby jsi měla jakékoliv další otázky / připomínky klidně i k mým zpětným vazbám, neváhej se ozvat.
