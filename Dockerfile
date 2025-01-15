# Sử dụng hình ảnh .NET SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy file dự án và khôi phục dependency
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ mã nguồn và build
COPY . ./
RUN dotnet publish -c Release -o /out

# Sử dụng hình ảnh .NET Runtime để chạy ứng dụng
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /out .

# Expose cổng HTTP và HTTPS
EXPOSE 80
EXPOSE 443

# Lệnh chạy ứng dụng
ENTRYPOINT ["dotnet", "CourseRegistrationSystem.dll"]
