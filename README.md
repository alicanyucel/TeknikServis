# TeknikServis — Kurumsal Teknik Servis Yönetimi (C# 12, .NET 8)

Bu proje; yüzde yüz “senior seviye” mimari, güvenilirlik ve operasyonel mükemmellik hedefiyle tasarlanmýþ, üretim (production) ortamýna hazýr bir Teknik Servis Yönetim sistemidir. Kod okunabilirliði, geniþletilebilirlik ve sürdürülebilirlik en üst düzeydedir.

## Neden Bu Proje “Senior Seviye”?
- Temiz mimari katmanlarý (Domain / Application / Infrastructure / WebAPI)
- CQRS + MediatR ile net sorumluluk ayrýmý, test edilebilirlik ve ölçeklenebilirlik
- Solid Repository + Unit of Work ile tutarlý veri katmaný ve atomik iþlemler
- Kimlik Doðrulama & Yetkilendirme: ASP.NET Identity + JWT + Rol tabanlý kontrol
- Üretim özellikleri: Rate limiting, global hata yönetimi, ProblemDetails, Swagger
- CI/CD, Test ve Kalite ölçümü: GitHub Actions + Test Coverage + Sonar entegrasyonu
- Docker & Docker Compose ile konteyner tabanlý daðýtým

## Mimari Genel Bakýþ
- Domain: Saf domain modelleri, value object’ler ve çekirdek kurallar
- Application: Use case’ler (Command/Query + Handler), Validasyon, Mapping
- Infrastructure: EF Core, DbContext, Repository/UoW, Identity, JwtProvider, DI
- WebAPI: Controller’lar, Middlewares, Swagger/OpenAPI, Yetkilendirme politikalarý

## Kullanýlan Teknolojiler
- .NET 8, C# 12
- Entity Framework Core (SQL Server)
- ASP.NET Identity, JWT Bearer
- MediatR (CQRS)
- AutoMapper
- FluentValidation (Behavior pipeline)
- TS.Result (Sonuç yönetimi)
- Scrutor (Otomatik servis tarama)
- Docker, Docker Compose

## Öne Çýkan Özellikler
- Soft Delete: Kayýtlar fiziksel deðil mantýksal olarak silinir (IsDeleted=true)
  - Örnek: `ServiceLineAction` için soft delete uygulanýr.
  - Denetimli: `UpdatedAt` ve `UpdatedBy` alanlarý güncellenir.
  - Ýdempotent: Zaten silinmiþse anlaþýlýr bir hata mesajý döner.
- Rol Tohumlama (Seeding): `Admin`, `User`, `Customer` rolleri otomatik oluþturulur.
- Ýlk Kullanýcý: `admin` kullanýcýsý otomatik açýlýr ve rolleri atanýr.
- JWT Role Claims: Token’a tüm roller eklenir; `[Authorize(Roles=...)]` güvenle çalýþýr.
- Rate Limiting: Sabit pencere (Fixed Window) ile kötüye kullaným önleme
- ProblemDetails + Global Exception Handler: Tutarlý hata sözleþmesi
- Swagger: Þema + Bearer token desteði ile API keþfi

## Çalýþtýrma
### Geliþtirme (Local)
1) Baðýmlýlýklarý yükle: `dotnet restore`
2) Veritabaný (gerekirse): `dotnet ef database update`
3) Uygulamayý çalýþtýr: `dotnet run --project ./TeknikServis/TeknikServis.WebAPI`
4) Swagger: http://localhost:5000/swagger veya yapýlandýrmanýza göre port

### Docker / Docker Compose
- Tüm sistemi konteyner olarak ayaða kaldýrýn:
```
docker-compose up -d --build
```
- Varsayýlan olarak API: http://localhost:5000

## Kimlik Doðrulama ve Roller
- Seed roller: `Admin`, `User`, `Customer`
- Ýlk kullanýcý: `admin / Mudbey123.` (EmailConfirmed=true)
- Token üretimi: Login ile JWT alýnýr; token içinde `role` claim’leri bulunur.
- Örnek yetki: `CustomersController` Create/Delete/Update -> Admin,Customer rolleri

## CI/CD — GitHub Actions
- `ci-cd.yml`: Build, test, kalite kapýlarý için temel boru hattý
- `coverage.yml`: Test coverage ölçümleri ve rozet entegrasyonu
- `sonar.yml`: SonarCloud/SonarQube kod kalitesi analizi

Gerekli Secret’lar (örnek):
- Sonar: `SONAR_TOKEN`, `SONAR_PROJECT_KEY`, `SONAR_ORG`
- Docker (isteðe baðlý): `DOCKERHUB_USERNAME`, `DOCKERHUB_TOKEN`

Pipeline davranýþý:
- PR ve main/master push’larýnda: build + test + kalite analizi tetiklenir
- Ýsteðe göre Docker image publish adýmlarý eklenebilir

## Testler
- Test Projesi: `TeknikServis.Test`
- Çalýþtýrma: `dotnet test -c Release`
- Coverage: GitHub Actions üzerinden otomatik toplanýr (coverage.yml)

## Geliþtirici Deneyimi
- Command/Query + Handler düzeni ile tek sorumluluk
- FluentValidation ile otomatik validasyon davranýþý
- AutoMapper ile minimal eþleme maliyeti
- Sonuç tipi (TS.Result) ile tutarlý API yanýtlarý

## Ýpuçlarý
- Okuma sorgularýnda soft delete için `!x.IsDeleted` filtresini unutmayýn
- `UpdatedBy` alanýný kimlik bilgisi ile doldurun (mevcutta örnek amaçlý “admin”)
- JWT ayarlarýnýzý `appsettings.json` içindeki `Jwt` bölümünden yapýn
- Postman koleksiyonu: `TeknikServis.WebAPI/TeknikServis.postman_collection.json`

## Yol Haritasý
- Global query filter ile soft delete’in otomatik filtrelenmesi
- Geliþmiþ gözlemlenebilirlik (distributed tracing, metrics)
- Docker image publish ve sürümleme (Tags) otomasyonu
- Performans profili/benchmark pipeline’ý

---
Bu proje; temiz mimari disiplini, modern .NET ekosistemi, CI/CD kültürü ve konteyner odaklý daðýtým yaklaþýmýyla “%100 Senior” bir ürün kalitesi hedefler. Kurumsal ölçekte güvenle devreye alýnabilir, ölçeklenebilir ve sürdürülebilirdir.
