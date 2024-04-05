# Clinic - Aplikacja do zarządzania kliniką.

Author: [Dominik Breksa](https://github.com/ForNeus57)

### Instalacja

Aby uprościć proces instalacji w repozytorium znajduje się plik `docker-compose.yml`, który zawiera konfigurację `bazy danych` oraz aplikacji `ASP.NET CORE`.

Wymagane oprogramowanie:
- System operacyjny: Windows 11
- Wersja docker desktop: Docker Desktop 4.26.1

Kroki instalacji niezbędne do uruchomienia aplikacji:

1. Pobierz repozytorium z kodem źródłowym pop rzez komendę:

```bash
git clone <url>
```

2. Przejdź do katalogu z kodem źródłowym:

```bash
cd clinic
```

3. Uruchom aplikację za pomocą komendy:

```bash
docker.exe compose -f docker-compose.yml -p clinic up
```

4. Aplikacja powinna być dostępna pod adresem:

```txt
http://localhost:8080
```

W razie problemów z instalacją proszę o kontakt: `dominikbreksa@gmail.com`

### Opis

Funkcjonalności aplikacji:
- Zarządzanie pacjentami
  - Dodawanie pacjentów
  - Edycja pacjentów
  - Usuwanie pacjentów
  - Przeglądanie listy pacjentów
- Sortowanie pacjentów
- Paginacja tablicy pacjentów

### Technologie

- ASP.NET CORE
- .NET 8
- Entity Framework Core
- Docker
- PostgreSQL