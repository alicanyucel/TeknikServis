TeknikServis - Proje Dokümantasyonu

Bu proje, TeknikServis uygulamasýdýr. Lead developer: Ali CAN YUCEL tarafýndan geliþtirilmiþtir.

Kýsa Açýklama:
- .NET 8 ile geliþtirilmiþ bir Web API projesidir.
- Katmanlý mimari (Domain, Application, Infrastructure, WebAPI) kullanýr.

Kullanýlan Teknolojiler ve Servisler:
- Programlama ve Framework: C# 12, .NET 8
- Veri Eriþimi: Entity Framework Core (EF Core) ile SQL Server
- Authentication & Authorization: ASP.NET Identity (AppUser / AppRole) ve JWT (JWT Bearer)
- Ýþ Akýþý: MediatR ile CQRS-benzeri yapý
- Doðrulama: FluentValidation
- Mapping: AutoMapper
- Logging: Serilog (console ve MSSQL sink'i ile günlükleme)
- Saðlýk Kontrolleri: ASP.NET Core Health Checks (SQL Server health check)
- Rate Limiting: .NET rate limiting (Fixed Window limiter) ve ek olarak proje içinde basit in-memory uygulama örneði bulunmaktadýr. Üretim için Redis veya daðýtýk cache tabanlý çözüm önerilir.
- Unit Testler: xUnit test framework'ü kullanýlmýþtýr. Testler TeknikServis.Test projesinde yer alýr; baðýmsýz birim testleri için mock/kopyalama ve DI üzerinden izole testler yazýlabilir.
- API Dokümantasyonu: Swagger (Swashbuckle)
- Diðer: Generic Repository pattern, Scrutor ile servis tarama, Docker & Docker Compose ile konteynerizasyon

Kod Kalite Metrikleri (SonarQube / SonarCloud):
- Proje için SonarCloud ya da self-hosted SonarQube entegrasyonu için örnek workflow eklendi (.github/workflows/sonar.yml).
- Kullaným (SonarCloud):
  1) SonarCloud veya SonarQube proje oluþturun.
  2) GitHub Secrets içine aþaðýdaki deðerleri ekleyin: SONAR_ORG, SONAR_TOKEN, SONAR_PROJECT_KEY (self-hosted SonarQube kullanýyorsanýz ilgili endpoint ve token ayarlarýný yapýn).
  3) main/master branch'e push veya pull request açýldýðýnda analiz tetiklenecektir.
- Badge ekleme örneði (SonarCloud):
  https://sonarcloud.io/api/project_badges/measure?project=YOUR_PROJECT_KEY&metric=alert_status

Test Coverage (Rozet):
- Codecov (örnek):
  https://img.shields.io/codecov/c/github/OWNER/REPO?style=flat  (OWNER/REPO kýsmýný deðiþtirin)
- Coveralls (örnek):
  https://img.shields.io/coveralls/github/OWNER/REPO?style=flat  (OWNER/REPO kýsmýný deðiþtirin)

Kurulum (kýsa):
- GitHub üzerinde repo varsa, GitHub Actions ile dotnet test --collect:"XPlat Code Coverage" çalýþtýrýp elde edilen coverage dosyasýný Codecov veya Coveralls'a gönderin.
- Örnek adýmlar:
  1) .github/workflows/coverage.yml oluþturun ve dotnet test + coverlet adýmlarýný ekleyin.
  2) Codecov/Coveralls token'ýnýzý GitHub Secrets'a ekleyin.
  3) README'deki badge URL'lerinde OWNER/REPO veya proje token bilgilerini güncelleyin.

Proje Çalýþtýrma (Docker ile):
- Docker ve Docker Compose yüklü olmalýdýr.
- Aþaðýdaki komut ile API ve veritabaný konteynerlerini baþlatýn:
  docker-compose up -d --build
- API, varsayýlan yapýlandýrmada host üzerinde http://localhost:5000 adresinde dinler.

Unit Testler:
- Projede "TeknikServis.Test" adýnda bir test projesi bulunmaktadýr.
- Testleri çalýþtýrmak için kök dizinden þu komutu kullanýn:
  dotnet test
- Test altyapýsý için xUnit kullanýlmýþtýr (örnek test sýnýfý: TeknikServis.Test/LoginCommandHandlerTest.cs).

Proje Deðerlendirmesi (Senior Ýmajý):
- Proje mimarisi katmanlý, SOLID ilkelerine uygun ve kurumsal uygulamalar için gerekli altyapý bileþenlerini içerir.
- Otomatik saðlýk kontrolleri, merkezi loglama (Serilog -> MSSQL), kimlik yönetimi, doðrulama, rate limiting ve Docker desteði gibi üretim odaklý özellikler bulunur.
- Bu sebeplerle proje, "Senior" seviyesinde bir profesyonel proje imajý sunar. (Kod kalitesi ve mimari yaklaþýmýnýn kurumsal gereksinimler doðrultusunda olgun olduðu varsayýmýyla.)

Geliþtirme ve Katký:
- Projeye katký için fork ve pull request akýþý kullanýlabilir.

Ýletiþim:
- Lead developer: Ali CAN YUCEL
