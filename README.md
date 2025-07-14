# TrivillinRaffaele

database commands:

from the API project folder of the project:
```
dotnet ef migrations add <migration name> -p ..\TrivillinRaffaele.DataAccess -o Migrations -c ApplicationDbContext

dotnet ef database update
```