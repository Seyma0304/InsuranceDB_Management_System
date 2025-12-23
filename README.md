# Insurance DB Management System

Modern ASP.NET Core 8.0 MVC web uygulaması - Sigorta yönetim sistemi

## Gereksinimler

- **.NET 8.0 SDK** veya üzeri
- **Visual Studio Code** + **C# Dev Kit** extension
- **SQL Server** (LocalDB, Express veya tam sürüm)

## Kurulum

### 1. .NET SDK Kontrolü
```bash
dotnet --version
# 8.0 veya üzeri görmelisiniz
```

### 2. Projeyi Klonlama
```bash
git clone https://github.com/Seyma0304/InsuranceDB_Management_System.git
cd InsuranceDB_Management_System/WebApplication
```

### 3. Bağımlılıkları Yükleme
```bash
dotnet restore
```

### 4. Veritabanı Bağlantısı
`appsettings.json` dosyasında connection string'i kendi SQL Server ayarlarınıza göre düzenleyin:

**Windows için:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=InsuranceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**macOS/Linux için:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=InsuranceDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
}
```

### 5. Projeyi Çalıştırma
```bash
dotnet run
```

Tarayıcıda: `https://localhost:5001` veya `http://localhost:5000` veya
'http://localhost:5000/Contract/SearchContracts'

## VS Code ile Geliştirme

### Gerekli Extensions
- **C# Dev Kit** (Microsoft)
- **C#** (Microsoft)

### Debug Yapılandırması
F5 tuşuna basarak debug modunda çalıştırabilirsiniz.



## Kullanılan Teknolojiler

- .NET 8.0 (LTS)
- ASP.NET Core MVC
- Microsoft.Data.SqlClient
- Bootstrap 5.2.3
- jQuery 3.7.0



### Build
```bash
dotnet build
```

### Temizleme
```bash
dotnet clean
```

### NuGet Paketi Ekleme
```bash
dotnet add package [PackageName]
```

### Hot Reload ile Çalıştırma
```bash
dotnet watch run
```







