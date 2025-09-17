TeknikServis - Proje Dok�mantasyonu

Bu proje, TeknikServis uygulamas�d�r. Lead developer: Ali CAN YUCEL taraf�ndan geli�tirilmi�tir.

K�sa A��klama:
- .NET 8 ile geli�tirilmi� bir Web API projesidir.
- Katmanl� mimari (Domain, Application, Infrastructure, WebAPI) kullan�r.

Ba�larken (Docker ile):
- Docker ve Docker Compose y�kl� olmal�d�r.
- A�a��daki komut ile API ve veritaban� konteynerlerini ba�lat�n:
  docker-compose up -d --build
- API, varsay�lan yap�land�rmada host �zerinde http://localhost:5000 adresinde dinler.

Konfig�rasyon ve Veritaban�:
- docker-compose.yml dosyas�nda �rnek bir SQL Server hizmeti bulunmaktad�r.
- �retim/yerel ortam ayarlar�n� TeknikServis.WebAPI/appsettings.json dosyas� ve ortam de�i�kenleri �zerinden y�netebilirsiniz.
- SA parolas� gibi hassas bilgileri ger�ek bir ortamda gizli tutun (�r. Docker secrets veya �evresel de�i�kenler).

Proje Yap�s� (k�sa):
- TeknikServis.Domain: Domain modelleri ve repository aray�zleri.
- TeknikServis.Application: �� mant���, MediatR istekleri ve servisler.
- TeknikServis.Infrastructure: Veri eri�imi, EF Core DbContext ve repository implementasyonlar�.
- TeknikServis.WebAPI: API katman�, controller'lar ve giri� noktas�.

Geli�tirme ve Katk�:
- Projeye katk� i�in fork ve pull request ak��� kullan�labilir.

�leti�im:
- Lead developer: Ali CAN YUCEL
