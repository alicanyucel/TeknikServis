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
- Rate Limiting: .NET rate limiting (Fixed Window limiter) ve ek olarak proje i�inde basit in-memory uygulama �rne�i bulunmaktad�r. �retim i�in Redis veya da��t�k cache tabanl� ��z�m �nerilir.
- Unit Testler: xUnit test framework'� kullan�lm��t�r. Testler TeknikServis.Test projesinde yer al�r; ba��ms�z birim testleri i�in mock/kopyalama ve DI �zerinden izole testler yaz�labilir.
- API Dok�mantasyonu: Swagger (Swashbuckle)
- Di�er: Generic Repository pattern, Scrutor ile servis tarama, Docker & Docker Compose ile konteynerizasyon

Kod Kalite Metrikleri (SonarQube / SonarCloud):
- Proje i�in SonarCloud ya da self-hosted SonarQube entegrasyonu i�in �rnek workflow eklendi (.github/workflows/sonar.yml).
- Kullan�m (SonarCloud):
  1) SonarCloud veya SonarQube proje olu�turun.
  2) GitHub Secrets i�ine a�a��daki de�erleri ekleyin: SONAR_ORG, SONAR_TOKEN, SONAR_PROJECT_KEY (self-hosted SonarQube kullan�yorsan�z ilgili endpoint ve token ayarlar�n� yap�n).
  3) main/master branch'e push veya pull request a��ld���nda analiz tetiklenecektir.
- Badge ekleme �rne�i (SonarCloud):
  https://sonarcloud.io/api/project_badges/measure?project=YOUR_PROJECT_KEY&metric=alert_status

Test Coverage (Rozet):
- Codecov (�rnek):
  https://img.shields.io/codecov/c/github/OWNER/REPO?style=flat  (OWNER/REPO k�sm�n� de�i�tirin)
- Coveralls (�rnek):
  https://img.shields.io/coveralls/github/OWNER/REPO?style=flat  (OWNER/REPO k�sm�n� de�i�tirin)

Kurulum (k�sa):
- GitHub �zerinde repo varsa, GitHub Actions ile dotnet test --collect:"XPlat Code Coverage" �al��t�r�p elde edilen coverage dosyas�n� Codecov veya Coveralls'a g�nderin.
- �rnek ad�mlar:
  1) .github/workflows/coverage.yml olu�turun ve dotnet test + coverlet ad�mlar�n� ekleyin.
  2) Codecov/Coveralls token'�n�z� GitHub Secrets'a ekleyin.
  3) README'deki badge URL'lerinde OWNER/REPO veya proje token bilgilerini g�ncelleyin.

Proje �al��t�rma (Docker ile):
- Docker ve Docker Compose y�kl� olmal�d�r.
- A�a��daki komut ile API ve veritaban� konteynerlerini ba�lat�n:
  docker-compose up -d --build
- API, varsay�lan yap�land�rmada host �zerinde http://localhost:5000 adresinde dinler.

Unit Testler:
- Projede "TeknikServis.Test" ad�nda bir test projesi bulunmaktad�r.
- Testleri �al��t�rmak i�in k�k dizinden �u komutu kullan�n:
  dotnet test
- Test altyap�s� i�in xUnit kullan�lm��t�r (�rnek test s�n�f�: TeknikServis.Test/LoginCommandHandlerTest.cs).

Proje De�erlendirmesi (Senior �maj�):
- Proje mimarisi katmanl�, SOLID ilkelerine uygun ve kurumsal uygulamalar i�in gerekli altyap� bile�enlerini i�erir.
- Otomatik sa�l�k kontrolleri, merkezi loglama (Serilog -> MSSQL), kimlik y�netimi, do�rulama, rate limiting ve Docker deste�i gibi �retim odakl� �zellikler bulunur.
- Bu sebeplerle proje, "Senior" seviyesinde bir profesyonel proje imaj� sunar. (Kod kalitesi ve mimari yakla��m�n�n kurumsal gereksinimler do�rultusunda olgun oldu�u varsay�m�yla.)

Geli�tirme ve Katk�:
- Projeye katk� i�in fork ve pull request ak��� kullan�labilir.

�leti�im:
- Lead developer: Ali CAN YUCEL
