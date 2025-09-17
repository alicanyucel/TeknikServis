TeknikServis - Proje Dokümantasyonu

Bu proje, TeknikServis uygulamasýdýr. Lead developer: Ali CAN YUCEL tarafýndan geliþtirilmiþtir.

Kýsa Açýklama:
- .NET 8 ile geliþtirilmiþ bir Web API projesidir.
- Katmanlý mimari (Domain, Application, Infrastructure, WebAPI) kullanýr.

Baþlarken (Docker ile):
- Docker ve Docker Compose yüklü olmalýdýr.
- Aþaðýdaki komut ile API ve veritabaný konteynerlerini baþlatýn:
  docker-compose up -d --build
- API, varsayýlan yapýlandýrmada host üzerinde http://localhost:5000 adresinde dinler.

Konfigürasyon ve Veritabaný:
- docker-compose.yml dosyasýnda örnek bir SQL Server hizmeti bulunmaktadýr.
- Üretim/yerel ortam ayarlarýný TeknikServis.WebAPI/appsettings.json dosyasý ve ortam deðiþkenleri üzerinden yönetebilirsiniz.
- SA parolasý gibi hassas bilgileri gerçek bir ortamda gizli tutun (ör. Docker secrets veya çevresel deðiþkenler).

Proje Yapýsý (kýsa):
- TeknikServis.Domain: Domain modelleri ve repository arayüzleri.
- TeknikServis.Application: Ýþ mantýðý, MediatR istekleri ve servisler.
- TeknikServis.Infrastructure: Veri eriþimi, EF Core DbContext ve repository implementasyonlarý.
- TeknikServis.WebAPI: API katmaný, controller'lar ve giriþ noktasý.

Geliþtirme ve Katký:
- Projeye katký için fork ve pull request akýþý kullanýlabilir.

Ýletiþim:
- Lead developer: Ali CAN YUCEL
