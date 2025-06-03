# 📚 Hiscaries Library of Stories !

---

[Open Draw.io File](https://drive.google.com/file/d/1YVpVJS43djNFkMbAwFCGF1CzXhPL_sf-/view?usp=sharing)

---

## 🚀 Local Development Setup

### Prerequisites

You will need 

- Docker Engine
- DotNet CLI and SDK

to run the application locally.

### Backend

To set up the backend environment, simply run:

```powershell
SetUp-LocalEnv.ps1
```

or bash if you're on linux

```bash
chmod +x setup-localenv.sh

setup-localenv.sh
```

This will configure the backend using .NET Aspire.

```powershell
Add-Migration.ps1
```

This will add a new migration to the chosen service.

To run the backend application, please set HC.AppHost as your startup project and hit run!

#### Helpful!

List your user secrets:

```bash
chmod +x list-usersecrets.sh
list-usersecrets.sh
```

Build and run on linux (given you are within the /server directory):

```bash
dotnet restore

dotnet build

dotnet run --project ./HC.AppHost/HC.AppHost.csproj
```

### Frontend

Navigate to the frontend (client) directory, then run:

```bash
npm install
ng serve
```

---

🎉 **Thank you for visiting the repository!**

Feel free to contribute or raise any issues to improve the application!
