
cd Vk.Data -> dotnet ef migrations add Initial -s ../VkApi/    -- new migration file generator
cd sln -> dotnet ef datab ase update --project  "./Vk.Data" --startup-project "./VkApi"   -- apply migrations files to database

/usr/local/share/dotnet/dotnet ef migrations add --project Vk.Data/Vk.Data.csproj --startup-project Vk.Api/Vk.Api.csproj --context Vk.Data.Context.VkDbContext --configuration Debug Initial --output-dir Migrations
/usr/local/share/dotnet/dotnet ef update --project Vk.Data/Vk.Data.csproj --startup-project Vk.Api/Vk.Api.csproj