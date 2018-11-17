# Založení projektu
Aplikace bude prvotně implementována s Sqlite napojením

* nový project typu "Console App (.NET Core)
* Nuget Microsoft.EntityFrameworkCore - základní balíček nezávislý na DB, obsahuje základní typy, funkčnosti, rozhraní
* Nuget Microsoft.EntityFrameworkCore.Relational - podpora pro "velké" relační databáze typu MSSQL, Oracle,... Pro sqlite není potřeba
* Nuget Microsoft.EntityFrameworkCore.InMemory - jde o nepersistentní uložiště, data jsou pouze v paměti, primárně určeno pro unit testy
* Nuget Microsoft.EntityFrameworkCore.Abstractions - různé abstrakce, atribut rozšičující EF, např. atribut Owned
* Nuget Microsoft.EntityFrameworkCore.Tools - podpora pro migrace, cmdlety,...
* Nuget Microsoft.EntityFrameworkCore.Tools.DotNet - podpora migrací přes příkazovou řádku (dotnet.exe ...)
* Nuget Microsoft.EntityFrameworkCore.Designe - umožňuje "scaffolding" = podpora pro automatické generování kódu podle určitých vzorů
* Nuget Microsoft.EntityFrameworkCore.Sqlite
* Nuget Microsoft.EntityFrameworkCore.Sqlite.Design

## Model
Jde o třídy entit reprezentující databázové struktury - objektový model databáze.

### Implictní konvence
* název třídy se mapuje na název tabulky
* název vlastnosti se mapuje na název pole tabulky
* pole mající název "Id" nebo sufix "Id" se nastavují jako primární klíče
* jednoduché cizí klíče se nastaví automaticky pokud odpovídají konvenci názvu `EntitaId`

## DBContext
Jedná se vždy o třídu dědící z bázové třídy "DBContext" a implementující konfiguraci databáze a napojení modelových entit na struktury v DB.

DBContext tímto obaluje veškerou komunikaci s datbází.

### OnConfiguring
Přepis metody za účelem konfigurace databázového připjení. Může zde být definice providera, nastavení různých parametrů,...

### OnModelCreating
Vytváří/mapuje modelové entity na DB struktury. Zde se definují pomocí FluentAPI např. pravidla typu primární klíč, cizí klíč a další constrainty. Tato pravidla je možné definovat také pomocí dataatributů, ale ty mají své omezení (nelze dělat např. složené klíče).

Napojení entit lze implementovat:
* přidáním vlastnosti typu DBSet<> na náš DBContext
* zavoláním metody "modelBuilder.Entity<>()" v "OnModelCreating"
* přidáním entity do již "napojené" entity jako vlastnosti

## Aktivace migrace
Aktivace migrací se v DB projeví vytvořením speciální tabulky s názvem "__EFMigrationsHistory", kde jsou ukládany informace o aplikovaných migracích.
### Package console
Provede zapnutí/inicializaci migrací<br/>
`Add-Migration InitialCreate`<br/>
Aplikuje připravené změny do DB<br/>
`Update-Database`<br/>
Dle změněného EF modelu připraví SQL aktualizaci DB, s názvem "NovaZmenaStruktur"<br/>
`Add-Migration NovaZmenaStruktur`
Aplikuje připravené změny do DB<br/>
`Update-Database

### Console
`dotnet ef migrations add InitialCreate`<br/>
`dotnet ef database update`<br/>
`dotnet ef migrations add NovaZmenaStruktur`<br/>
`dotnet ef database update`

### Postřehy
U migrací je vhodné vždy zkontrolovat vygenerovaný aktualizační soubor. Dále je lepší provádět migrace po menších částech, pro lepší řešení případných migračních problémů

#### Primární klíče
Jednoduché (single) klíče lze definovat pomocí data atributů. U složených (composite) klíčů je nutné použít vždy FluentAPI.

## Logování
Proti předchozímu EF6 je zde logování řešenou pomocí "providerů" - akce Database.Log zde již není dostupná.

Nutno přidat Nuget "Microsoft.Extensions.Logging.LoggerFactory" pro vytváření potřebných providerů a nastavit mu požadovaného providera. Může být jak vlastní implementace, tak nějaký existující, např. "Microsoft.Extensions.Logging.Debug.DebugLoggerProvider"<br/>
`static readonly Microsoft.Extensions.Logging.LoggerFactory LogFactory`<br/>
`  = new Microsoft.Extensions.Logging.LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });`

a v metodě "OnConfiguring" přihlásit k odběru:<br/>
`optionsBuilder.UseLoggerFactory(LogFactory);`

