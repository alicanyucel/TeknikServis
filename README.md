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
- Rate Limiting: .NET rate limiting / basit in-memory uygulama (özelleþtirilebilir)
- API Dokümantasyonu: Swagger (Swashbuckle)
- Diðer: Generic Repository pattern, Scrutor ile servis tarama, Docker & Docker Compose ile konteynerizasyon

Proje Çalýþtýrma (Docker ile):
- Docker ve Docker Compose yüklü olmalýdýr.
- Aþaðýdaki komut ile API ve veritabaný konteynerlerini baþlatýn:
  docker-compose up -d --build
- API, varsayýlan yapýlandýrmada host üzerinde http://localhost:5000 adresinde dinler.

Proje Deðerlendirmesi (Senior Ýmajý):
- Proje mimarisi katmanlý, SOLID ilkelerine uygun ve kurumsal uygulamalar için gerekli altyapý bileþenlerini içerir.
- Otomatik saðlýk kontrolleri, merkezi loglama (Serilog -> MSSQL), kimlik yönetimi, doðrulama, rate limiting ve Docker desteði gibi üretim odaklý özellikler bulunur.
- Bu sebeplerle proje, "Senior" seviyesinde bir profesyonel proje imajý sunar. (Kod kalitesi ve mimari yaklaþýmýnýn kurumsal gereksinimler doðrultusunda olgun olduðu varsayýmýyla.)

Geliþtirme ve Katký:
- Projeye katký için fork ve pull request akýþý kullanýlabilir.

Ýletiþim:
- Lead developer: Ali CAN YUCEL
