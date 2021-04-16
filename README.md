# Blogger

GENÇLERİN BLOGU

Bu uygulama, kullanıcıların farklı kategorilerde, farklı paylaşımlar yapmalarına imkan sağlamak amacıyla geliştirdiğim bir blog uygulamasıdır.
Uygulamanın çalışır hali için https://www.genclerinblogu.com adresinden sitemi ziyaret edebilirsiniz.

------------------------------------------------------------------------------------
  
Kullanılan Teknolojiler ve Yöntemler

•Asp.Net Core MVC -> Sunum Katmanı

•	MSSQL -> Veritabanı

•	ClassLibrary (.Net Core) -> Katmanlı Mimari

•	Entity Framework  -> ORM Aracı

•	LINQ -> Veri Sorgulama

•	Html,Css,Javascript,Bootstrap -> Görsel Tasarım 

•	CkEditor4 -> Editor, resim ekleme, kod eklentisi ekleme

•	Uygulama Code First stratejisiyle geliştirilmiştir.

------------------------------------------------------------------------------------

Uygulama Özellikleri

•	Üyelik ve Kayıt Sistemi(Email Doğrulama,Şifremi Unuttum)

•	Paylaşımlar -> Ekle, Oku, Sil, Güncelle, Sayfala, Ara

•	Kategoriler -> Ekle,Oku, Sil, Güncelle, Filtrele

•	Yorumlar -> Ekle, Sil

•	Cevaplar -> Ekle, Sil

•	Kısıtlamalar -> Ekle, Sil

•	Roller -> Ekle, Sil

•	Kullanıcılar -> Ekle, Oku, Sil, Güncelle, Ara


------------------------------------------------------------------------------------

Güvenlik

•	Kullanıcılara ait parolalar oldukları gibi saklanmazlar. Hash algoritmasıyla şifrelenerek saklanırlar. Kullanıcı parolasını girdiğinde parola  tekrar şifrelenir ve şifrelenmiş hali veritabanındaki haliyle karşılaştırılır. Admin dahil kimse parolanızı göremez.

•	Kayıt olurken email doğrulama istenir. Email doğrulaması yapılmadığı takdirde kayıt olan kullanıcılar giriş yapamazlar. Giriş yapmayan kullanıcılar paylaşımlara yorum yapamazlar ve yorumlara cevap veremezler.

•	Kullanıcılar veritabanına rol ilişkileri kurularak kaydedilir. Yetkisiz kullanıcılar admin paneline giriş yapamazlar. Ayrıca admin panelinde admin ve author rollerinin farklı yetkileri vardır.

