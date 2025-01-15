# Sử dụng .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Thiết lập thư mục làm việc
WORKDIR /src

# Copy file .csproj vào thư mục làm việc
COPY ["CourseRegistrationSystem.csproj", "./"]

# Restore dependencies
RUN dotnet restore "CourseRegistrationSystem.csproj"

# Copy toàn bộ mã nguồn vào container
COPY . .

# Build ứng dụng
RUN dotnet build "CourseRegistrationSystem.csproj" -c Release -o /app/build

# Publish ứng dụng ra thư mục /app/publish
RUN dotnet publish "CourseRegistrationSystem.csproj" -c Release -o /app/publish

# Sử dụng .NET Runtime để chạy ứng dụng (không cần build lại)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# Thiết lập thư mục làm việc trong container
WORKDIR /app

# Copy ứng dụng đã build vào thư mục làm việc
COPY --from=build /app/publish .

# Mở cổng 80 cho ứng dụng web
EXPOSE 80

# Chạy ứng dụng
ENTRYPOINT ["dotnet", "CourseRegistrationSystem.dll"]
