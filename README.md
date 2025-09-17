TeknikServis - Proje Dok�mantasyonu

Bu proje, TeknikServis uygulamas�d�r. Lead developer: Ali CAN YUCEL taraf�ndan geli�tirilmi�tir.

K�sa A��klama:
- .NET 8 ile geli�tirilmi� bir Web API projesidir.
- Katmanl� mimari (Domain, Application, Infrastructure, WebAPI) kullan�r.

Kullan�lan Teknolojiler ve Servisler:
- Programlama ve Framework: C# 12, .NET 8
- Veri Eri�imi: Entity Framework Core (EF Core) ile SQL Server
- Authentication & Authorization: ASP.NET Identity (AppUser / AppRole) ve JWT (JWT Bearer)
- �� Ak���: MediatR ile CQRS-benzeri yap�
- Do�rulama: FluentValidation
- Mapping: AutoMapper
- Logging: Serilog (console ve MSSQL sink'i ile g�nl�kleme)
- Sa�l�k Kontrolleri: ASP.NET Core Health Checks (SQL Server health check)
- Rate Limiting: .NET rate limiting / basit in-memory uygulama (�zelle�tirilebilir)
- API Dok�mantasyonu: Swagger (Swashbuckle)
- Di�er: Generic Repository pattern, Scrutor ile servis tarama, Docker & Docker Compose ile konteynerizasyon

Proje �al��t�rma (Docker ile):
- Docker ve Docker Compose y�kl� olmal�d�r.
- A�a��daki komut ile API ve veritaban� konteynerlerini ba�lat�n:
  docker-compose up -d --build
- API, varsay�lan yap�land�rmada host �zerinde http://localhost:5000 adresinde dinler.

Proje De�erlendirmesi (Senior �maj�):
- Proje mimarisi katmanl�, SOLID ilkelerine uygun ve kurumsal uygulamalar i�in gerekli altyap� bile�enlerini i�erir.
- Otomatik sa�l�k kontrolleri, merkezi loglama (Serilog -> MSSQL), kimlik y�netimi, do�rulama, rate limiting ve Docker deste�i gibi �retim odakl� �zellikler bulunur.
- Bu sebeplerle proje, "Senior" seviyesinde bir profesyonel proje imaj� sunar. (Kod kalitesi ve mimari yakla��m�n�n kurumsal gereksinimler do�rultusunda olgun oldu�u varsay�m�yla.)

Geli�tirme ve Katk�:
- Projeye katk� i�in fork ve pull request ak��� kullan�labilir.

�leti�im:
- Lead developer: Ali CAN YUCEL
