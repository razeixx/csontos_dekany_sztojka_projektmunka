# csontos_dekany_sztojka_projektmunka


## Projektleírás

Egy sulis projekt, amit C# nyelven készítünk el. A program egy egyszerűbb, konzolos alkalmazás lesz, amivel Magyarország lakosainak az adatait lehet kezelni és tárolni. 

A szoftver lényege, hogy egy könnyen használható, menüs rendszert adjon az ügyintézőknek. A programban lehet majd új embereket regisztrálni a személyes adataikkal 
(név, lakcím, személyi igazolvány szám, stb.), lehet keresni a lakosok között, és ha valaki elköltözik, át lehet írni a lakcímét. Az adatokat egy külön fájlba is el fogjuk menteni, hogy a program bezárása után se vesszenek el.

**Fejlesztői környezet:** Visual Studio, C# (.NET 8.0)



## Feladatmegosztás (Csapattagok és szerepkörök)


**Csontos Fanni** - GitHub menedzser - repó létrehozása, branch stratégia kidolgozása és a README.md karbantartása.

**Sztojka Gergő** - Rendszertervező - követelmény lista és kezdeti osztályterv megtervezése.

**Dékány Milán** - Vezető fejlesztő - C# konzolos projekt alapvázának létrehozása VS-ban és Git-be feltöltés.


## Követelmény lista



## Branch-stratégia

A munkavégzés megkönnyítése és az ütközések elkerülése érdekében senki nem dolgozik a fő ágon hanem minden új feladathoz egy külön, saját ágat hoz létre.


**`main`**: A stabil, éles ág. Közvetlenül ide senki nem commitol. Csak tesztelt, teljesen kész kód kerülhet ide a mérföldkövek végén.

**`develop` (Fejlesztői ág)**: A fejlesztői főág. Ide integráljuk a kész részfeladatokat tesztelésre, mielőtt a `main`-re kerülnének.

**`feature/...` (Funkcióágak)**: Minden új funkciót vagy osztályt egy saját, külön ágon fejlesztünk (pl. `feature/citizen-class`, `feature/console-menu`). Ha egy funkció elkészült és működik, Pull Request-et nyitunk a `develop` ág felé.


## Kezdeti osztályterv











