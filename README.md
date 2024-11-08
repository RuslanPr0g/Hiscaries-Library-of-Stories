# 📚 Hiscaries Library of Stories

---

## 🛠️ Configuration Settings

Configure your settings in your chosen config file as follows:

```json
{
  "Kestrel:Certificates:Development:Password": "***",
  "JwtSettings:Key": "***",
  "JwtSettings:Issuer": "***",
  "JwtSettings:Audience": "***",
  "SaltSettings:StoredSalt": "***",
  "ConnectionStrings:PostgresEF": "Server=postgresdb;Port=5432;User Id=postgres;Password=***;Database=hiscarydbef;Include Error Detail=true;"
}
```

In your `.env` file (to be located next to `docker-compose.yml`), include the following:

```
POSTGRES_PASSWORD=***
```

---

🎉 **Thank you for visiting the repository!**  
Feel free to contribute or raise any issues to improve the application!
