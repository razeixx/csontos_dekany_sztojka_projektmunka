
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

- Személyek felvétele, módosítása, keresése és törlése.
- Személyes adatok ellenőrzése és tárolása.
- Régiók, megyék, járások és települések kezelése.
- Címek és lakcímváltozások nyilvántartása.
- Keresés, szűrés és rendezés biztosítása.
- Lakossági statisztikák készítése.
- Adatok mentése és betöltése TXT-fájlokból.
- Felhasználók és jogosultságok kezelése.
- Módosítások naplózása.
- Hibás adatok és jogosulatlan műveletek kezelése.

## Branch-stratégia

A munkavégzés megkönnyítése és az ütközések elkerülése érdekében senki nem dolgozik a fő ágon hanem minden új feladathoz egy külön, saját ágat hoz létre.


**`main`**: A stabil, éles ág. Közvetlenül ide senki nem commitol. Csak tesztelt, teljesen kész kód kerülhet ide a mérföldkövek végén.

**`develop` (Fejlesztői ág)**: A fejlesztői főág. Ide integráljuk a kész részfeladatokat tesztelésre, mielőtt a `main`-re kerülnének.

**`feature/...` (Funkcióágak)**: Minden új funkciót vagy osztályt egy saját, külön ágon fejlesztünk (pl. `feature/citizen-class`, `feature/console-menu`). Ha egy funkció elkészült és működik, Pull Request-et nyitunk a `develop` ág felé.


## Kezdeti osztályterv

**1. Hogyan épül fel a program**
A program négy részre (rétegre) van osztva, és minden rész csak a mellette lévővel beszél. Az adatokat
leíró osztályokat mindegyik rész használja.
• Felület: ezt látja a felhasználó. Csak az adatok beírását és kiírását kezeli, szabályokat nem.
• Szabályok és műveletek: itt dől el, mit szabad csinálni. Ide tartozik a személyek és címek
kezelése, a keresés, a statisztika és a fájlba mentés.
• Adatok elérése: ez menti, módosítja, keresi és törli az adatokat. A fölötte lévő rész nem tudja,
hogy fájlból vagy adatbázisból jön az adat, ezért a tárolást később ki lehet cserélni.
• Tárolás: maga a fájl (később akár adatbázis).

**2 Helyek: régió, megye, járás, település**
A helyek így épülnek egymásra: egy régióhoz több megye tartozik, egy megyéhez több járás, egy
járáshoz több település.
Régió (`Regio`): egy nagyobb terület, például Észak-Alföld. Van azonosítója és neve, és több megye
tartozik hozzá.
Megye (`Megye`): a 19 megye és Budapest. Van azonosítója és neve, és egy régióhoz tartozik. Több járása
és települése van.
Járás (`Jaras`): a megye és a település közötti egység. Egy megyéhez tartozik, és több települést fog
össze.
Település (`Telepules`): egy város vagy község. Van neve, irányítószáma és típusa (például főváros vagy
község), és egy járáshoz tartozik. A megye és a régió a járáson keresztül kiderül. Mivel a települések
előre fel vannak töltve, nem lehet elgépelt vagy nem létező települést megadni.
Cím (`Cim`): egy konkrét hely: település, közterület neve (például Kossuth), közterület fajtája (utca, tér,
út), házszám, emelet és ajtó. A cím nem tartozik senkihez, ezért több ember is lakhat ugyanazon a
címen, és nem kell ugyanazt a címet többször eltárolni.

**2.2 A személy és a lakcíme**
Személyazonosító (`SzemelyAzonosito`): egy személy egyedi azonosítószáma. Külön osztály, mert az
azonosítónak saját szabályai vannak (milyen alakú lehet, mikor érvényes), és ezeket így egy helyen lehet
ellenőrizni. Létrehozás után nem változik, és két azonosító akkor egyezik, ha ugyanaz az értékük. Az ilyen
osztályt érték-objektumnak hívják.

Személy (`Szemely`): a program legfontosabb osztálya. Tárolja a vezeték- és keresztnevet, a születési
nevet, az anyja nevét, a születés helyét és idejét, a nemet, a telefonszámot (ha van) és a halál dátumát
(ha az illető meghalt). Az életkort és azt, hogy él-e még, nem tároljuk külön: ezeket a dátumokból
számolja ki a program. Van egy jelölő is arra, hogy a személy törölt-e (a törlés csak megjelölés, lásd lent).
Lakcím (`Lakcim`): összeköti a személyt és a címet, és megjegyzi, mettől meddig lakott ott. Így
megmaradnak a régi címek is. A jelenlegi lakcím az, amelynek még nincs lezárva az ideje.
Két egyszerű választható lista is kell: a nem (Nem) és a szerepkör (Szerepkor: admin, ügyintéző, olvasó).

**2.3 Felhasználók és napló**
Felhasználó (`Felhasznalo`): aki a programot használja. Van felhasználóneve, jelszava (titkosítva tárolva)
és szerepköre. Az admin mindent csinálhat, az ügyintéző adatot rögzít és módosít, az olvasó csak nézheti
az adatokat.
Naplóbejegyzés (`AuditBejegyzes`): minden módosításról készül egy jegyzet: ki, mikor, mit csinált, és
melyik adaton. Ez az adatvédelem (GDPR) és az ellenőrizhetőség miatt kell.

**3. A szabályokat és műveleteket végző osztályok**
Ezek az osztályok végzik a tényleges munkát.
SzemelyKezelo: új személy felvétele, módosítása, haláleset rögzítése és keresés. Megnézi, hogy az
azonosító egyedi-e, ki vannak-e töltve a kötelező adatok, és helyes-e a dátum.

CimKezelo: bejelentés (új lakcím felvétele), kijelentés (lakcím lezárása) és költözés (a régi cím lezárása és
az új felvétele).
TelepulesKezelo: települések felvétele, keresése és listázása. Ellenőrzi, hogy a megadott település
tényleg létezik.

KeresesKezelo: keresés név vagy azonosító szerint, szűrés település, megye, régió, életkor és nem
szerint, és rendezés növekvő vagy csökkenő sorrendbe.
StatisztikaKezelo: a lakosok száma településenként, megyénként és régiónként, korfa (hány ember él az
egyes korosztályokban), átlagéletkor, a legfiatalabb és a legidősebb lakos, a férfiak és nők aránya, a
legnépesebb település és megye.

FajlKezelo: az adatok betöltése és kiírása TXT fájlokba. Kezeli, ha hiányzik vagy üres a fájl, ha hibás az
adat, vagy ha rossz a formátum.

AuditNaplozo: naplóbejegyzést ír minden módosítás után.
JogosultsagKezelo: a szerepkör alapján megmondja, hogy valaki megtehet-e egy műveletet.

**Fontos szabályok:**
• Egy gyerek nem lehet idősebb a szülőjénél.
• Elhunyt személyt nem lehet új címre bejelenteni.
• Költözésnél a régi lakcímet le kell zárni.

• Csak olyan település adható meg, amelyik szerepel a nyilvántartásban.

**4. Az adatokat mentő és olvasó osztályok**
SzemelyTar: a személyek mentése, módosítása, törltnek jelölése és keresése.
TelepulesTar: a régiók, megyék, járások és települések elérése.

CimTar: a címek és a lakcímek (a címek története).
FelhasznaloTar és AuditTar: a felhasználók és a naplóbejegyzések tárolása.
Mindegyikhez tartozik egy rövid leírás is (például ISzemelyTar), ami csak azt mondja meg, mit tud a tár,
azt nem, hogyan csinálja.
A szabályokat végző osztályok csak ezeket a leírásokat ismerik. Így a fájlos tárolás később adatbázisra
cserélhető anélkül, hogy a szabályokhoz hozzá kellene nyúlni.

**5. A menü**
A MenuKezelo osztály megmutatja a főmenüt, fogadja, mit választ a felhasználó, és elindítja a megfelelő
műveletet. A menüben van: személyek kezelése, címkezelés, települések kezelése, keresés, szűrés,
rendezés, statisztikák, mentés és betöltés, kilépés.

**6. Fontos döntések**
• Törlés csak megjelölés: a személyt nem töröljük ténylegesen, csak megjelöljük, hogy törölt. Így
az adatok később is visszakereshetők.
• Címtörténet: ha valaki elköltözik, új bejegyzés készül, a régit nem írjuk felül.
• A cím külön osztály: így nem kell ugyanazt a címet többször tárolni, és könnyű megszámolni,
hányan élnek egy településen.
• Az azonosító külön osztály: az érvényességi szabályok egy helyen vannak.
• Számolt adatok: az életkor és az „él-e még” nem tárolt adat, hanem a dátumokból számolódik.
• Rétegek: a tárolás cserélhető, és a szabályok nem függnek attól, hogyan néz ki a felület.

**7. Melyik osztály melyikhez kapcsolódik**
• Egy régióhoz több megye, egy megyéhez több járás, egy járáshoz több település tartozik.
• Egy településen több cím lehet.
• Egy személynek az idők során több lakcíme lehet, és egy címen több személy is lakhat.
• Minden személynek egy azonosítója van.
• Egy felhasználóhoz több naplóbejegyzés tartozik.












